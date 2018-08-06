using RodriBus.Utils.Regional;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace RodriBus.Utils.Tests.Regional
{
    public class CccTests
    {
        [Theory]
        [InlineData("2005-0665-69-3151246628")]
        [InlineData("0011-3127-29-2656681581")]
        [InlineData("14798133808414754291")]
        public static void ShouldValidateCcc(string validCcc)
        {
            var ccc = new Ccc(validCcc);
            ccc.IsValid.Should().BeTrue();
        }


        [Theory]
        [InlineData("2005-0665-69-3151246627")]
        [InlineData("0011-3127-29-2656681586")]
        [InlineData("14798133808414754295")]
        public static void ShouldNotValidateInvalidCcc(string invalidCcc)
        {
            var ccc = new Ccc(invalidCcc);
            ccc.IsValid.Should().BeFalse();
        }


        [Theory]
        //mixed separators
        [InlineData("2005-0665 69-3151246628")]
        [InlineData("0011-3127_29-2656681581")]
        [InlineData("1479-8133_80 8414754291")]
        //incorrect length
        [InlineData("200506656931512466")]
        [InlineData("001131272926566815810")]
        [InlineData("1479813380841475429")]
        public static void ShouldThrowWithIncorrectAccount(string invalidCcc)
        {
            Action action = () => new Ccc(invalidCcc);
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData("2005-0665-69-3151246628", ' ')]
        [InlineData("0011 3127 29 2656681581", '_')]
        [InlineData("1479_8133_80_8414754291", '-')]
        public static void ShouldThrowWithIncorrectSeparator(string validCcc, char separator)
        {
            Action action = () => new Ccc(validCcc, separator);
            action.Should().Throw<ArgumentException>();
        }
    }
}
