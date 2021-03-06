﻿// Copyright 2018 @asmichi (on github). Licensed under the MIT License. See LICENCE in the project root for details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Asmichi.Utilities.Utilities
{
    public class CommandLineUtilTest
    {
        [Fact]
        public void MakeCommandLineQuotesArguments()
        {
            // NOTE: in the following asserts, we substitute ' for " to ease escaping.

            // no need for quoting
            Assert("cmd 1 2 3", "cmd", "1", "2", "3");
            Assert(@"c\m\d\ \1\2\3\", @"c\m\d\", @"\1\2\3\");

            // spaces, tabs
            Assert("'c m d' '1 2' a", "c m d", "1 2", "a");
            Assert("'c\tm\td' '1\t2' a", "c\tm\td", "1\t2", "a");
            Assert("'c m d' ' 1 2 ' a", "c m d", " 1 2 ", "a");
            Assert("'c\tm\td' '\t1\t2\t' a", "c\tm\td", "\t1\t2\t", "a");

            // quotes
            Assert(@"'\'cmd\'' '\'1\''", "'cmd'", "'1'");

            // backslashes in a quoted part (no need for escape)
            Assert(@"'c m\d' '1 2\3'", @"c m\d", @"1 2\3");

            // backslashes followed by a double quote
            Assert(@"'cmd\\\'' '123\\\''", @"cmd\'", @"123\'");
            Assert(@"'cmd\\\\\'' '123\\\\\''", @"cmd\\'", @"123\\'");

            void Assert(string expected, string fileName, params string[] args)
            {
                string Replace(string s) => s.Replace('\'', '"');

                Xunit.Assert.Equal(
                    Replace(expected),
                    CommandLineUtil.MakeCommandLine(Replace(fileName), args.Select(Replace).ToArray()).ToString());
            }
        }
    }
}
