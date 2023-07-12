using FluentAssertions;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Tests.Types;

public class ArrayTypeGenericTests
{
    [Test]
    public void GetIndex_OneDimensional_ShouldReturnExpected()
    {
        var array = new ArrayType<DINT>(new DINT[] { 1, 2, 3, 4 });

        var type = array[0];

        type.Should().BeOfType<DINT>();
        type.Should().Be(1);
    }

    [Test]
    public void SetIndex()
    {
        var array = new ArrayType<DINT>(new DINT[] { 1, 2, 3, 4 });

        array[0] = new DINT(2);
    }

    [Test]
    public void GetIndex_TwoDimensional_ShouldReturnExpected()
    {
        var array = new ArrayType<DINT>(new DINT[,] { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } });

        var type = array[1, 1];

        type.Should().BeOfType<DINT>();
        type.Should().Be(4);
    }
}