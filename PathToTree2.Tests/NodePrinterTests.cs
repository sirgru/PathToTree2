using NUnit.Framework;
using System.Collections.Generic;

namespace PathToTree2.Tests.NodePrinterTestsNS
{
	[TestFixture]
	public class NodePrinterTests
	{
		[Test]
		public static void Simple()
		{
			var o = new Options();
			var paths = new List<Path>();
			paths.Add(new Path(@"file1", o));
			paths.Add(new Path(@"folder/file2", o));

			var root = new Node.Folder.Root(paths);
			var result = new NodePrinter(root, o);

			var correct = @" file1
 + folder
    file2
".Replace("\r\n", "\n");

			Assert.AreEqual(correct, result.ToString());
		}

		[Test]
		public static void Lite()
		{
			var o = new Options();
			o.TrySetFromSwitch("s");
			o.TrySetFromSwitch("i");

			var paths = new List<Path>();
			paths.Add(new Path(@"M F1.txt", o));
			paths.Add(new Path(@"M Folder\F2.txt", o));
			paths.Add(new Path(@"! F3.txt", o));
			paths.Add(new Path(@"? Folder\F33.txt", o));
			paths.Add(new Path(@"? Folder\Folder 1\F2.txt", o));
			paths.Add(new Path(@"? Folder\Folder 1\F33.txt", o));
			paths.Add(new Path(@"? Folder\Folder 1\Folder 2\F2.txt", o));
			paths.Add(new Path(@"? Folder\Folder 1\Folder 2\F33.txt", o));

			var root = new Node.Folder.Root(paths);
			var result = new NodePrinter(root, o);

			var correct = @" M F1.txt
 ! F3.txt
 > Folder
   M F2.txt
   ? F33.txt
   > Folder 1
     ? F2.txt
     ? F33.txt
     > Folder 2
       ? F2.txt
       ? F33.txt
".Replace("\r\n", "\n");

			Assert.AreEqual(correct, result.ToString());
		}

		[Test]
		public static void Simple2()
		{
			var o = new Options();
			var paths = new List<Path> {
				new Path("folder_1/folder_1_1/file_1.txt", o),
				new Path("folder_1/folder_1_1/file_2.txt", o),
				new Path("folder_1/folder_1_2/file_2.txt", o),
				new Path("folder_2/folder_2_1/file_3.txt", o),
				new Path("folder_2/folder_2_1/file_4.txt", o),
				new Path("folder_1/folder_1_2/file_5.txt", o),
				new Path("folder_3/folder_3_1", o),
				new Path("folder_3/folder_3_1/file_6.txt", o),
			};
			var root = new Node.Folder.Root(paths);
			var result = new NodePrinter(root, o);

			var correct = @" ┬ folder_1
 ├── folder_1_1
 │    file_1.txt
 │    file_2.txt
 └── folder_1_2
      file_2.txt
      file_5.txt
 ┬ folder_2
 └── folder_2_1
      file_3.txt
      file_4.txt
 ┬ folder_3
 └── folder_3_1
      file_6.txt
".Replace("\r\n", "\n");

			Assert.AreEqual(correct, result.ToString());
		}

		[Test]
		public static void Complex()
		{
			var o = new Options();
			var paths = new List<Path>();
			paths.Add(new Path(@"C:\WINDOWS\AppPatch\MUI\040C.file", o));
			paths.Add(new Path(@"C:\foo.file", o));
			paths.Add(new Path(@"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\MUI", o));
			paths.Add(new Path(@"C:\WINDOWS\addins.file", o));
			paths.Add(new Path(@"C:\WINDOWS\AppPatch\MUI", o));
			paths.Add(new Path(@"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\MUI\0409.file", o));
			paths.Add(new Path(@"C:\WINDOWS\Microsoft.NET\Framework2\v2.0.50727\MUI\0409.file", o));

			var root = new Node.Folder.Root(paths);
			var result = new NodePrinter(root, o);

			var correct = @" ┬ C:
 │  foo.file
 └─┬ WINDOWS
   │  addins.file
   ├─┬ AppPatch
   │ └── MUI
   │      040C.file
   └─┬ Microsoft.NET
     ├─┬ Framework
     │ └─┬ v2.0.50727
     │   └── MUI
     │        0409.file
     └─┬ Framework2
       └─┬ v2.0.50727
         └── MUI
              0409.file
".Replace("\r\n", "\n");

			Assert.AreEqual(correct, result.ToString());
		}
	}
}
