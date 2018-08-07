using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;
using RodriBus.Utils.Global;
using static RodriBus.Utils.Global.Iban;

namespace RodriBus.Utils.Tests.Global
{
    public static class IbanTests
    {
        [Theory]
        [InlineData("BE18001654923565")]
        [InlineData("GB82WEST12345698765432")]
        [InlineData("GB82-WEST-1234-5698-7654-32")]
        [InlineData("FR76 3000 6000 0112 3456 7890 189")]
        [InlineData("DE91 1000 0000 0123 4567 89")]
        [InlineData("GR96 0810 0010 0000 0123 4567 890")]
        [InlineData("RO09 BCYP 0000 0012 3456 7890")]
        [InlineData("SA44 2000 0001 2345 6789 1234")]
        [InlineData("ES79 2100 0813 6101 2345 6789")]
        [InlineData("CH56 0483 5012 3456 7800 9")]
        [InlineData("GB98 MIDL 0700 9312 3456 78")]
        public static void ShouldValidateIban(string validIban)
        {
            var instance = new Iban(validIban);
            instance.ValidationStatus.Should().Be(ValidationResult.IsValid);
        }


        [Theory]
        [InlineData("BE1800165492356")]
        [InlineData("GB82WEST123456987654320")]
        [InlineData("BE180016549235656")]
        [InlineData("BE18001654923566")]
        [InlineData("XX82WEST12345698765432")]
        public static void ShouldNotValidateIban(string invalidIban)
        {
            var instance = new Iban(invalidIban);
            instance.ValidationStatus.Should().NotBe(ValidationResult.IsValid);
        }
    }
}
