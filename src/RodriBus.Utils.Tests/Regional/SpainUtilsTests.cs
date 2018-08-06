using RodriBus.Utils.Regional;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace RodriBus.Utils.Tests.Regional
{
    public static class SpainUtilsTests
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

        [Theory]
        [InlineData("0034918458879")]
        [InlineData("34918458879")]
        [InlineData("+34918458879")]
        [InlineData("918458879")]
        [InlineData("0034 91 845 88 79")]
        [InlineData("34 91 845 88 79")]
        [InlineData("+34 91 845 88 79")]
        [InlineData("91 845 88 79")]
        [InlineData("0034-91-845-88-79")]
        [InlineData("34-91-845-88-79")]
        [InlineData("+34-91-845-88-79")]
        [InlineData("91-845-88-79")]
        [InlineData("0034.91.845.88.79")]
        [InlineData("34.91.845.88.79")]
        [InlineData("+34.91.845.88.79")]
        [InlineData("91.845.88.79")]


        [InlineData("0034689551122")]
        [InlineData("34689551122")]
        [InlineData("+34689551122")]
        [InlineData("689551122")]

        [InlineData("0034789551122")]
        [InlineData("34789551122")]
        [InlineData("+34789551122")]
        [InlineData("789551122")]
        public static void ShouldValidatePhoneNumbers(string validPhone)
        {
            SpainUtils.IsValidPhone(validPhone).Should().BeTrue();
        }

        [Theory]
        [InlineData("003491845887")]
        [InlineData("3491845887")]
        [InlineData("+3491845887")]
        [InlineData("91845887")]
        [InlineData("034918458879")]
        [InlineData("3918458879")]
        [InlineData("+4918458879")]
        [InlineData("0035918458879")]
        [InlineData("35918458879")]
        [InlineData("+35918458879")]
        [InlineData("818458879")]
        [InlineData("003491r8458879")]
        [InlineData("349184588,79")]
        [InlineData("0034589551122")]
        [InlineData("34589551122")]
        [InlineData("+34589551122")]
        [InlineData("589551122")]
        public static void ShouldNotValidatePhoneNumbers(string validPhone)
        {
            SpainUtils.IsValidPhone(validPhone).Should().BeFalse();
        }
    }
}
