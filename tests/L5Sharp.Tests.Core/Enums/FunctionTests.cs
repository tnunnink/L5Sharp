using FluentAssertions;

namespace L5Sharp.Tests.Core.Enums;

[TestFixture]
public class FunctionTests
{
    [Test]
    public void Abs_WhenCalled_ShouldNotBeNull()
    {
        Function.Abs.Should().NotBeNull();
        Function.Abs.Name.Should().Be("Abs");
        Function.Abs.Value.Should().Be("ABS");
    }

    [Test]
    public void Acos_WhenCalled_ShouldNotBeNull()
    {
        Function.Acos.Should().NotBeNull();
        Function.Acos.Name.Should().Be("Acos");
        Function.Acos.Value.Should().Be("ACOS");
    }

    [Test]
    public void Asin_WhenCalled_ShouldNotBeNull()
    {
        Function.Asin.Should().NotBeNull();
        Function.Asin.Name.Should().Be("Asin");
        Function.Asin.Value.Should().Be("ASIN");
    }

    [Test]
    public void Atan_WhenCalled_ShouldNotBeNull()
    {
        Function.Atan.Should().NotBeNull();
        Function.Atan.Name.Should().Be("Atan");
        Function.Atan.Value.Should().Be("ATAN");
    }

    [Test]
    public void Cos_WhenCalled_ShouldNotBeNull()
    {
        Function.Cos.Should().NotBeNull();
        Function.Cos.Name.Should().Be("Cos");
        Function.Cos.Value.Should().Be("COS");
    }

    [Test]
    public void Deg_WhenCalled_ShouldNotBeNull()
    {
        Function.Deg.Should().NotBeNull();
        Function.Deg.Name.Should().Be("Deg");
        Function.Deg.Value.Should().Be("DEG");
    }

    [Test]
    public void Ln_WhenCalled_ShouldNotBeNull()
    {
        Function.Ln.Should().NotBeNull();
        Function.Ln.Name.Should().Be("Ln");
        Function.Ln.Value.Should().Be("LN");
    }

    [Test]
    public void Log_WhenCalled_ShouldNotBeNull()
    {
        Function.Log.Should().NotBeNull();
        Function.Log.Name.Should().Be("Log");
        Function.Log.Value.Should().Be("LOG");
    }

    [Test]
    public void Rad_WhenCalled_ShouldNotBeNull()
    {
        Function.Rad.Should().NotBeNull();
        Function.Rad.Name.Should().Be("Rad");
        Function.Rad.Value.Should().Be("RAD");
    }

    [Test]
    public void Sin_WhenCalled_ShouldNotBeNull()
    {
        Function.Sin.Should().NotBeNull();
        Function.Sin.Name.Should().Be("Sin");
        Function.Sin.Value.Should().Be("SIN");
    }

    [Test]
    public void Sqrt_WhenCalled_ShouldNotBeNull()
    {
        Function.Sqrt.Should().NotBeNull();
        Function.Sqrt.Name.Should().Be("Sqrt");
        Function.Sqrt.Value.Should().Be("SQRT");
    }

    [Test]
    public void Tan_WhenCalled_ShouldNotBeNull()
    {
        Function.Tan.Should().NotBeNull();
        Function.Tan.Name.Should().Be("Tan");
        Function.Tan.Value.Should().Be("TAN");
    }

    [Test]
    public void Trunc_WhenCalled_ShouldNotBeNull()
    {
        Function.Trunc.Should().NotBeNull();
        Function.Trunc.Name.Should().Be("Trunc");
        Function.Trunc.Value.Should().Be("TRUNC");
    }

    [Test]
    public void All_ShouldHaveExpectedCount()
    {
        Function.All().Should().HaveCount(13);
    }
}
