using L5Sharp.Components;

namespace L5Sharp.Tests.Components;

[TestFixture]
public class RoutineTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var routine = new Routine();
        
        routine.Content?.Add(new Rung());
    }
}