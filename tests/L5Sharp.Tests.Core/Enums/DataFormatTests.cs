﻿using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Tests.Core.Enums
{
    [TestFixture]
    public class DataFormatTests
    {
        [Test]
        public void New_Decorated_ShouldNotBeNullAndHaveExpectedValue()
        {
            var format = DataFormat.Decorated;

            format.Should().NotBeNull();
            format.Name.Should().Be("Decorated");
        }


        [Test]
        public void New_String_ShouldNotBeNullAndHaveExpectedValue()
        {
            var format = DataFormat.String;

            format.Should().NotBeNull();
            format.Name.Should().Be("String");
        }
        
        
        [Test]
        public void New_Alarm_ShouldNotBeNullAndHaveExpectedValue()
        {
            var format = DataFormat.Alarm;

            format.Should().NotBeNull();
            format.Name.Should().Be("Alarm");
        }
    }
}