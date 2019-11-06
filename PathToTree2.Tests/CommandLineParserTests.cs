using NUnit.Framework;

namespace PathToTree2.Tests.CommandLineParserTestsNS
{
	[TestFixture]
	public class CommandLineParserTests
	{
		[Test]
		public static void Help1()
		{
			Assert.Throws<HelpRequested>(() => {
				CommandLineParser.GetOption("-h");
			});
		}

		[Test]
		public static void Help2()
		{
			Assert.Throws<HelpRequested>(() => {
				CommandLineParser.GetOption("/?");
			});
		}

		[Test]
		public static void Help3()
		{
			Assert.Throws<HelpRequested>(() => {
				CommandLineParser.GetOption("-H");
			});
		}

		[Test]
		public static void InvalidSwitchContent()
		{
			Assert.Throws<CommandLineException>(() => {
				CommandLineParser.GetOption("-š");
			});
		}

		[Test]
		public static void InvalidSwitchStart()
		{
			Assert.Throws<CommandLineException>(() => {
				CommandLineParser.GetOption("~a");
			});
		}

		[Test]
		public static void ValidSwitch()
		{
			Options o = CommandLineParser.GetOption("/c");
			Assert.IsTrue(o.useColoration);
		}
	}
}
