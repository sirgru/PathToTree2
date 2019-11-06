using NUnit.Framework;
using System.Collections.Generic;

namespace PathToTree2.Tests.NodeTestsNS
{
	[TestFixture]
	public class NodeTests
	{
		[Test]
		public static void Simple()
		{
			Options o = new Options();

			List<Path> paths = new List<Path> {
				new Path("folder_1/folder_1_1/file_1.txt", o),
				new Path("folder_1/folder_1_2/file_2.txt", o),
				new Path("folder_2/folder_2_1/file_3.txt", o),
				new Path("folder_2/folder_2_1/file_4.txt", o),
				new Path("folder_1/folder_1_2/file_5.txt", o),
				new Path("folder_3/folder_3_1", o),
				new Path("folder_3/folder_3_1/file_6.txt", o),
			};

			var root = new Node.Folder.Root(paths).value;

			Assert.AreEqual("", root.name);
			Assert.AreEqual(3, root.folderChildren.Count);
			Assert.AreEqual(0, root.fileChildren.Count);

			var folder_1 = root.folderChildren[0];
			Assert.AreEqual("folder_1", folder_1.name);
			Assert.AreEqual(2, folder_1.folderChildren.Count);
			Assert.AreEqual(0, folder_1.fileChildren.Count);

			var folder_2 = root.folderChildren[1];
			Assert.AreEqual("folder_2", folder_2.name);
			Assert.AreEqual(1, folder_2.folderChildren.Count);
			Assert.AreEqual(0, folder_2.fileChildren.Count);

			var folder_3 = root.folderChildren[2];
			Assert.AreEqual("folder_3", folder_3.name);
			Assert.AreEqual(1, folder_3.folderChildren.Count);
			Assert.AreEqual(0, folder_3.fileChildren.Count);

			var folder_1_1 = folder_1.folderChildren[0];
			Assert.AreEqual("folder_1_1", folder_1_1.name);
			Assert.AreEqual(0, folder_1_1.folderChildren.Count);
			Assert.AreEqual(1, folder_1_1.fileChildren.Count);

			var file_1 = folder_1_1.fileChildren[0];
			Assert.AreEqual("file_1.txt", file_1.name);

			var folder_1_2 = folder_1.folderChildren[1];
			Assert.AreEqual("folder_1_2", folder_1_2.name);
			Assert.AreEqual(0, folder_1_2.folderChildren.Count);
			Assert.AreEqual(2, folder_1_2.fileChildren.Count);

			var file_2 = folder_1_2.fileChildren[0];
			Assert.AreEqual("file_2.txt", file_2.name);

			var file_5 = folder_1_2.fileChildren[1];
			Assert.AreEqual("file_5.txt", file_5.name);

			var folder_2_1 = folder_2.folderChildren[0];
			Assert.AreEqual("folder_2_1", folder_2_1.name);
			Assert.AreEqual(0, folder_2_1.folderChildren.Count);
			Assert.AreEqual(2, folder_2_1.fileChildren.Count);

			var file_3 = folder_2_1.fileChildren[0];
			Assert.AreEqual("file_3.txt", file_3.name);

			var file_4 = folder_2_1.fileChildren[1];
			Assert.AreEqual("file_4.txt", file_4.name);

			var folder_3_1 = folder_3.folderChildren[0];
			Assert.AreEqual("folder_3_1", folder_3_1.name);
			Assert.AreEqual(0, folder_3_1.folderChildren.Count);
			Assert.AreEqual(1, folder_3_1.fileChildren.Count);

			var file_6 = folder_3_1.fileChildren[0];
			Assert.AreEqual("file_6.txt", file_6.name);
		}
	}
}
