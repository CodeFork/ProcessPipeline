﻿// Copyright 2018 @asmichi (on github). Licensed under the MIT License. See LICENCE in the project root for details.

using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Asmichi.Utilities.Utilities
{
    public class EnvironmentBlockUtilTest
    {
        public static readonly object[][] TestMakeEnvironmentBlockWin32TestCases = new object[][]
        {
            new object[2] { "A=a\0\0", new[] { ("A", "a") } },
            new object[2] { "A=a\0BB=bb\0\0", new[] { ("A", "a"), ("BB", "bb") } },
        };

        [Theory]
        [MemberData(nameof(TestMakeEnvironmentBlockWin32TestCases))]
        public void TestMakeEnvironmentBlockWin32(string expected, (string name, string value)[] input)
        {
            Assert.Equal(
                expected.ToCharArray(),
                EnvironmentBlockUtil.MakeEnvironmentBlockWin32(input));
        }
    }
}
