using L5Sharp.Console;

while (Console.ReadLine() != "exit")
{
    var test = new TestClass();
    await test.RunExample();
}