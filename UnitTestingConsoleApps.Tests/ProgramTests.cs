using Moq;
using System.Text;
using System.IO;

namespace UnitTestingConsoleApps.Tests;

public class ProgramTests
{
  // buffer for all the output that the program generates
  StringBuilder _ConsoleOutput;
  Mock<TextReader> _ConsoleInput;

  [SetUp]
  public void Setup()
  {
    _ConsoleOutput = new StringBuilder();
    var consoleOutputWriter = new StringWriter(_ConsoleOutput);
    _ConsoleInput = new Mock<TextReader>();
    Console.SetOut(consoleOutputWriter);
    Console.SetIn(_ConsoleInput.Object);
  }
  private string[] RunMainAndGetConsoleOutput()
  {
    Program.Main(default);
    return _ConsoleOutput.ToString().Split("\r\n");
  }

  private MockSequence SetupUserResponses(params string[] userResponses)
  {
    var sequence = new MockSequence();
    foreach (var response in userResponses)
      _ConsoleInput.InSequence(sequence).Setup(x => x.ReadLine()).Returns(response);
    return sequence;
  }

  [Test]
  public void Main_AskForUserName_WhenExecuted()
  {
    SetupUserResponses("John", "10", "20");
    var expectedPromt = "What is your name?";

    var outputLines = RunMainAndGetConsoleOutput();
    Assert.That(outputLines[0], Is.EqualTo(expectedPromt));
  }
  [Test]
  public void Main_GreetsUser_WhenNameIsSupplied()
  {
    SetupUserResponses("John", "10", "20");
    var expectedGreeting = "Hello John";

    var outputLines = RunMainAndGetConsoleOutput();

    Assert.That(outputLines[1], Is.EqualTo(expectedGreeting));

  }
  [Test]
  public void Main_PromptsUserAgain_WhenNameIsEmpty()
  {
    SetupUserResponses("", "James", "10", "20");
    var expectedPrompt = "What is your name?";

    var outputLines = RunMainAndGetConsoleOutput();

    Assert.That(outputLines[1], Is.EqualTo(expectedPrompt));

  }
}