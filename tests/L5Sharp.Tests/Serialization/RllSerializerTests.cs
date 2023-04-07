using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Serialization;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Serialization
{
    [TestFixture]
    public class RllSerializerTests
    {
        private RllSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new RllSerializer();
        }

        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Serialize_ValidType_ShouldNotBeNull()
        {
            var rungs = new List<Rung>
            {
                new() { Text = new NeutralText("XIC(Test);") },
                new() { Text = new NeutralText("TON(TestTimer,0,0);") },
                new() { Text = new NeutralText("OTU(Test);") },
                new() { Text = new NeutralText("OTL(Test);") },
            };

            var component = new Rll(rungs);

            var xml = _serializer.Serialize(component);

            xml.Should().NotBeNull();
        }

        [Test]
        public Task Serialize_ValidType_ShouldBeApproved()
        {
            var rungs = new List<Rung>
            {
                new() { Text = new NeutralText("XIC(Test);") },
                new() { Text = new NeutralText("TON(TestTimer,0,0);") },
                new() { Text = new NeutralText("OTU(Test);") },
                new() { Text = new NeutralText("OTL(Test);") },
            };

            var component = new Rll(rungs);

            var xml = _serializer.Serialize(component);

            return Verify(xml.ToString());
        }

        [Test]
        public void Deserialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Deserialize(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Deserialize_ValidElement_ShouldNotBeNull()
        {
            var xml = XElement.Parse(GetRllData());

            var component = _serializer.Deserialize(xml);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_ValidBoolElement_ShouldHaveExpectedProperties()
        {
            var xml = XElement.Parse(GetRllData());

            var component = _serializer.Deserialize(xml);

            component.Should().HaveCount(6);
            component.Count.Should().Be(6);

            component[0].Text.ToString().Should().Be("TON(TestTimer,?,?);");
            component[1].Text.ToString().Should().Be("MOV(16#20,SimpleSint);");
            component[2].Text.ToString().Should().Be("aoi_Test(aoiTestInstance,TestSimpleTag,SimpleInt,RealArray);");
            component[3].Text.ToString().Should()
                .Be("[XIC(SimpleBool) ,XIC(SimpleBool) ][OTE(SimpleBool) ,OTU(SimpleBool) ];");
            component[4].Text.ToString().Should().Be("OTE(TestComplexTag.SimpleMember.BoolMember);");
            component[5].Text.ToString().Should().Be("MOV(SimpleSint,AsciiTag);");
        }

        private static string GetRllData()
        {
            return @"<RLLContent>
                <Rung Number=""0"" Type=""N"">
                <Text>
                <![CDATA[TON(TestTimer,?,?);]]>
            </Text>
                </Rung>
                <Rung Number=""1"" Type=""N"">
                <Text>
                <![CDATA[MOV(16#20,SimpleSint);]]>
            </Text>
                </Rung>
                <Rung Number=""2"" Type=""N"">
                <Text>
                <![CDATA[aoi_Test(aoiTestInstance,TestSimpleTag,SimpleInt,RealArray);]]>
            </Text>
                </Rung>
                <Rung Number=""3"" Type=""N"">
                <Text>
                <![CDATA[[XIC(SimpleBool) ,XIC(SimpleBool) ][OTE(SimpleBool) ,OTU(SimpleBool) ];]]>
            </Text>
                </Rung>
                <Rung Number=""4"" Type=""N"">
                <Text>
                <![CDATA[OTE(TestComplexTag.SimpleMember.BoolMember);]]>
            </Text>
                </Rung>
                <Rung Number=""5"" Type=""N"">
                <Text>
                <![CDATA[MOV(SimpleSint,AsciiTag);]]>
            </Text>
                </Rung>
                </RLLContent>";
        }
    }
}