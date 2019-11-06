using NUnit.Framework;
using System.Collections.Generic;

namespace PathToTree2.Tests.GitSpecificNS
{
	[TestFixture]
	public class GitSpecific
	{
		[Test]
		public static void Simple()
		{
			var o = new Options();
			o.TrySetFromSwitch("s");

			var paths = new List<Path> {
				new Path("M  asdfsdf.txt", o),
				new Path("R  dfgdfg.txt -> ssdfds/dfgdfg.txt", o),
				new Path("A  \"ssdfds/ssdfds 2/dfgdfg.txt\"", o),
				new Path("A  \"ssdfds/ssdfds 2/fdsgfdgdfg.txt\"", o),
				new Path("A  \"ssdfds/ssdfds 2/ssdfds 3/dfgdfg.txt\"", o),
				new Path("A  \"ssdfds/ssdfds 2/ssdfds 3/fdsgfdgdfg.txt\"", o),
			};
			var root = new Node.Folder.Root(paths);
			var result = new NodePrinter(root, o);

			var correct = @" R  dfgdfg.txt -> ssdfds/dfgdfg.txt
 M_ asdfsdf.txt
 ┬ ssdfds
 └─┬ ssdfds 2
   │  A_ dfgdfg.txt
   │  A_ fdsgfdgdfg.txt
   └── ssdfds 3
        A_ dfgdfg.txt
        A_ fdsgfdgdfg.txt
".Replace("\r\n", "\n");

			Assert.AreEqual(correct, result.ToString());
		}
	}
}
