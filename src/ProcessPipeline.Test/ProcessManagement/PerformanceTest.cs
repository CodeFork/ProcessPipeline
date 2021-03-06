﻿// Copyright 2018 @asmichi (on github). Licensed under the MIT License. See LICENCE in the project root for details.

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

#pragma warning disable RCS1090 // Call 'ConfigureAwait(false)'.

namespace Asmichi.Utilities.ProcessManagement
{
    public class PerformanceTest
    {
        [Fact]
        public void ChildProcessWaitForAsyncIsTrulyAsynchronous()
        {
            var si = new ChildProcessStartInfo(TestUtil.TestChildPath, "Sleep", "1000")
            {
                StdOutputRedirection = OutputRedirection.NullDevice,
                StdErrorRedirection = OutputRedirection.NullDevice,
            };

            using (var sut = ChildProcess.Start(si))
            {
                WaitForAsyncIsTrulyAsynchronous(sut);
            }
        }

        [Fact]
        public void ProcessPipelineWaitForAsyncIsTrulyAsynchronous()
        {
            var si = new ProcessPipelineStartInfo()
            {
                StdOutputRedirection = OutputRedirection.NullDevice,
                StdErrorRedirection = OutputRedirection.NullDevice,
            };
            si.Add(TestUtil.TestChildPath, "Sleep", "1000");
            si.Add(TestUtil.TestChildPath, "Sleep", "1000");

            using (var sut = ProcessPipeline.Start(si))
            {
                WaitForAsyncIsTrulyAsynchronous(sut);
            }
        }

        private static void WaitForAsyncIsTrulyAsynchronous(IChildProcess sut)
        {
            var sw = Stopwatch.StartNew();
            // Because WaitForExitAsync is truly asynchronous and does not block a thread-pool thread,
            // we can create WaitForExitAsync tasks without consuming thread-pool threads.
            // In other words, if WaitForExitAsync would consume a thread-pool thread, the works queued by Task.Run would be blocked.
            var waitTasks =
                Enumerable.Range(0, Environment.ProcessorCount * 8)
                .Select(_ => sut.WaitForExitAsync(1000))
                .ToArray();
            Assert.True(waitTasks.All(x => !x.IsCompleted));

            var emptyTasks =
                Enumerable.Range(0, Environment.ProcessorCount * 8)
                .Select(_ => Task.Run(() => { }))
                .ToArray();
            Task.WaitAll(emptyTasks);

            Assert.True(sw.ElapsedMilliseconds < 100);
        }
    }
}
