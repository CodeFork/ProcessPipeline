﻿// Copyright 2018 @asmichi (on github). Licensed under the MIT License. See LICENCE in the project root for details.

using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace Asmichi.Utilities.ProcessManagement
{
    internal class ChildProcessWaitHandle : WaitHandle
    {
        public ChildProcessWaitHandle(SafeWaitHandle waitHandle)
        {
            this.SetSafeWaitHandle(waitHandle);
        }
    }
}
