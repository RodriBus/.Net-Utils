using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Rodribus.Utils;
using Xunit;

namespace RodriBus.Utils.Tests
{
    public static class TimeUtilsTests
    {
        [Fact]
        public static void ShouldTransformDateTimeToUnixTimestamp()
        {
            var date = new DateTime(2018, 08, 06, 0, 0, 0, DateTimeKind.Utc);
            var expected = 1533513600;
            var actual = TimeUtils.DateTimeToUnixTimestamp(date);

            actual.Should().Be(expected);
        }

        [Fact]
        public static void ShouldTransformUnixTimeStampToDateTime()
        {
            var timestamp = 1533513600;
            var expected = new DateTime(2018, 08, 06, 0, 0, 0, DateTimeKind.Utc);
            var actual = TimeUtils.UnixTimeStampToDateTime(timestamp);

            actual.Should().Be(expected);
        }

        [Fact]
        public static void ShouldCalculateAgeFromBirthDay()
        {
            var birthday = new DateTime(1990, 05, 01);
            var reference = new DateTime(2000, 06, 01);
            var expected = 10;
            var actual = TimeUtils.GetAge(reference, birthday);

            actual.Should().Be(expected);
        }

        [Fact]
        public static void ShouldCalculateExactAgeFromBirthDay()
        {
            var birthday = new DateTime(1990, 05, 01);
            var reference = new DateTime(2005, 05, 01);
            var expected = 15;
            var actual = TimeUtils.GetAge(reference, birthday);

            actual.Should().Be(expected);
        }
    }
}
