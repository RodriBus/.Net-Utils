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
    }
}
