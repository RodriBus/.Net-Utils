using RodriBus.Utils.Regional;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace RodriBus.Utils.Tests.Regional
{
    public class SpainUtilsTests
    {
        [Theory]
        [InlineData("34801337Z")]
        [InlineData("45517999X")]
        [InlineData("04909603T")]
        [InlineData("14323708K")]
        public static void ShouldValidateNif(string validId)
        {
            SpainUtils.IsValidIdNumber(SpainIdNumber.NIF, validId).Should().BeTrue();
        }

        [Theory]
        [InlineData("14801337Z")]
        [InlineData("25517999X")]
        [InlineData("304909603T")]
        [InlineData("4423708K")]
        public static void ShouldNotValidateInvalidNif(string invalidId)
        {
            SpainUtils.IsValidIdNumber(SpainIdNumber.NIF, invalidId).Should().BeFalse();
        }

        [Theory]
        [InlineData("Y6950597B")]
        [InlineData("X4676338R")]
        [InlineData("Z1555507E")]
        public static void ShouldValidateNie(string validId)
        {
            SpainUtils.IsValidIdNumber(SpainIdNumber.NIE, validId).Should().BeTrue();
        }

        [Theory]
        [InlineData("Z6950597B")]
        [InlineData("X467638R")]
        [InlineData("Z15559507E")]
        public static void ShouldNotValidateInvalidNie(string invalidId)
        {
            SpainUtils.IsValidIdNumber(SpainIdNumber.NIE, invalidId).Should().BeFalse();
        }
    }
}
