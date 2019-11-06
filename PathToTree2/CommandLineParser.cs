using System.Collections.Generic;

namespace PathToTree2
{
	public class Options
	{
		public bool hasStatusFlags { get; private set; }
		public bool useColoration { get; private set; }
		public bool useInvisibleTree { get; private set; }
		public bool printLineSeparators { get; private set; }

		public bool TrySetFromSwitch(string sw)
		{
			switch (sw) {
			case "s": hasStatusFlags = true; return true;
			case "c": useColoration = true; return true;
			case "i": useInvisibleTree = true; return true;
			case "l": printLineSeparators = true; return true;
			default: return false;
			}
		}
	}

	public static class CommandLineParser
	{
		public static Options GetOption(string input)
		{
			return GetOptions(new string[] { input });
		}

		public static Options GetOptions(string[] inputSwitches)
		{
			List<string> switches = new List<string>();
			Options result = new Options();

			foreach (var inputSwitch in inputSwitches) {
				if (!StartsCorrectly(inputSwitch)) throw new CommandLineException($"Invalid switch: '{inputSwitch}'. Switches must start with either '/' or '-'.");

				string iswContent = inputSwitch.Substring(1);
				if (iswContent == "?" || iswContent.ToUpperInvariant() == "H") throw new HelpRequested();

				bool success = result.TrySetFromSwitch(iswContent);
				if (!success) throw new CommandLineException($"Invalid switch content: '{iswContent}'");
			}

			return result;
		}

		private static bool StartsCorrectly(string isw)
		{
			return isw.StartsWith('/') || isw.StartsWith('-');
		}

		internal static string HelpHeader()
		{
			return
			"********** Path 2 Tree Help **********\n" +
			"Program transforms a stream of file paths piped in, one file path per line, into a tree structure.\n" +
			"The intended use case for the program is transforming output from hg and git status commands to a tree structure.\n" +
			"When using git status, option -s for 'short' must be used.\n" +
			"Paths can be in either Windows-style or UNIX style (backslash '\\' vs slash '/').\n" +
			"Output paths are in UNIX style (using '/').\n" +
			"Command line options can be specified either Windows or UNIX style (slash '/' vs dash '-').\n";
		}

		internal static string HelpSwitches()
		{
			return
			"\t-s Input contains status flags from hg/git status commands.\n" +
			"\t-c Color the output, which on Windows you must pipe to cmdcolor.exe," +
            "\t   found here: https://github.com/jeremejevs/cmdcolor\n" +
			"\t-i Print tree with 'invisible branches', for 'light' view.\n" +
			"\t-l Print a line separator before and after the tree.\n";
		}
	}
}
