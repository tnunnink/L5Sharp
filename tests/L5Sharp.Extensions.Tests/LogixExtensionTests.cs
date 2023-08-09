using FluentAssertions;
using L5Sharp.Common;
using L5Sharp.Components;
using L5Sharp.Samples;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Extensions.Tests;

[TestFixture]
public class LogixExtensionTests
{
    [Test]
    public void ToArrayType_OneDimension_ShouldBeOfCorrectType()
    {
        var array = new DINT[] { 1, 2, 3, 4 };

        var type = array.ToArrayType();

        type.Should().BeOfType<ArrayType<DINT>>();
    }

    [Test]
    public void ToArrayType_OperatorOverload_ShouldBeOfCorrectType_()
    {
        LogixType type = new DINT[] { 1, 2, 3, 4 };
        
        type.Should().BeOfType<ArrayType<DINT>>();
    }
    
    [Test]
    public void Tags_WhenCalled_GetsAllTagsEverywhereIncludingModuleAndAoi()
    {
        var content = LogixContent.Load(Known.Test);
        
        var tags = content.Tags().ToList();

        tags.Should().NotBeEmpty();
    }

    [Test]
    public void FindByTagName_ValidTagNameWithDefaultComparer_ShouldHaveExpectedCount()
    {
        var content = LogixContent.Load(Known.Test);

        var results = content.Tags().Find("LocalComplex.CounterMember.PRE", TagNameComparer.Qualified).ToList();

        results.Should().HaveCount(1);
    }
    
    [Test]
    public void Names_WhenCalled_ContainsExpectedNames()
    {
        var tag = new Tag { Name = "Test", Value = new TIMER() };

        var names = tag.TagNames().ToList();

        names.Should().HaveCount(70);
        names.Should().Contain("Test");
        names.Should().Contain("Test.PRE");
        names.Should().Contain("Test.PRE.0");
        names.Should().Contain("Test.PRE.31");
        names.Should().Contain("Test.ACC");
        names.Should().Contain("Test.ACC.0");
        names.Should().Contain("Test.ACC.31");
        names.Should().Contain("Test.DN");
        names.Should().Contain("Test.TT");
        names.Should().Contain("Test.EN");
    }

    [Test]
    public Task AddModule_ValidModule_ShouldBeVerified()
    {
        var content = LogixContent.Load(Known.Test);

        var local = content.Modules.Local();
        
        local?.Add(new Module{ Name = "Test", CatalogNumber = "WhoCares", Revision = 1.3});

        var xml = content.Modules.Serialize().ToString();

        return Verify(xml);
    }
}