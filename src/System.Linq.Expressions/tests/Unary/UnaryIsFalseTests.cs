// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Reflection;
using Xunit;

namespace System.Linq.Expressions.Tests
{
    public static class UnaryIsFalseTests
    {
        #region Test methods

        [Theory, ClassData(typeof(CompilationTypes))] //[WorkItem(3196, "https://github.com/dotnet/corefx/issues/3196")]
        public static void CheckUnaryIsFalseBoolTest(bool useInterpreter)
        {
            bool[] values = new bool[] { false, true };
            for (int i = 0; i < values.Length; i++)
            {
                VerifyIsFalseBool(values[i], useInterpreter);
            }
        }

        [Fact]
        public static void ToStringTest()
        {
            var e = Expression.IsFalse(Expression.Parameter(typeof(bool), "x"));
            Assert.Equal("IsFalse(x)", e.ToString());
        }

        #endregion

        #region Test verifiers

        private static void VerifyIsFalseBool(bool value, bool useInterpreter)
        {
            Expression<Func<bool>> e =
                Expression.Lambda<Func<bool>>(
                    Expression.IsFalse(Expression.Constant(value, typeof(bool))),
                    Enumerable.Empty<ParameterExpression>());
            Func<bool> f = e.Compile(useInterpreter);
            Assert.Equal((bool)(value == false), f());
        }


        #endregion
    }
}
