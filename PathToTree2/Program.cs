using System;
using System.Collections.Generic;

namespace PathToTree2
{
	class Program
	{
		static void Main(string[] args)
		{
			try {
				var options = CommandLineParser.GetOptions(args);

				List<Path> paths = new List<Path>();

				string s;
				while ((s = Console.ReadLine()) != null) {
					if (s.Trim() == "") break;

					paths.Add(new Path(s, options));
				}

				var root = new Node.Folder.Root(paths);
				var result = new NodePrinter(root, options);
				Console.WriteLine(result);
			}
			catch (CommandLineException ex) {
				Console.WriteLine();
				Console.WriteLine("Command argument error: " + ex.Message);
				Console.WriteLine();
				Console.WriteLine(CommandLineParser.HelpSwitches());
			}
			catch (HelpRequested) {
				Console.WriteLine();
				Console.WriteLine(CommandLineParser.HelpHeader());
				Console.WriteLine(CommandLineParser.HelpSwitches());
			}
			catch (InputException ex) {
				Console.WriteLine();
				Console.WriteLine("Input error: " + ex.Message);
			}
		}
	}

}
