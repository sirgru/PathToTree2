using NUnit.Framework;
using System.Collections.Generic;

namespace PathToTree2.Tests.PathTestsNS
{
	[TestFixture]
	public class PathTests
	{
		[Test]
		public static void MaintainsFlagsHg()
		{
			Options o = new Options();
			o.TrySetFromSwitch("s");
			var path = new Path("M folder/file.txt", o);

			Assert.IsTrue(path.type == Path.SC_Type.Hg);
			Assert.AreEqual("M", path.statusFlags);
			Assert.AreEqual("folder/file.txt", path.fullPath);
		}

		[Test]
		public static void MaintainsFlagsGit1()
		{
			Options o = new Options();
			o.TrySetFromSwitch("s");
			var path = new Path(" M folder/file.txt", o);

			Assert.IsTrue(path.type == Path.SC_Type.Git);
			Assert.AreEqual("_M", path.statusFlags);
			Assert.AreEqual("folder/file.txt", path.fullPath);
		}

		[Test]
		public static void MaintainsFlagsGit2()
		{
			Options o = new Options();
			o.TrySetFromSwitch("s");
			var path = new Path("M  folder/file.txt", o);

			Assert.IsTrue(path.type == Path.SC_Type.Git);
			Assert.AreEqual("M_", path.statusFlags);
			Assert.AreEqual("folder/file.txt", path.fullPath);
		}

		[Test]
		public static void EliminateDoubleSlashes()
		{
			Options o = new Options();
			var path = new Path("folder/////file.txt", o);

			Assert.AreEqual("", path.statusFlags);
			Assert.AreEqual("folder/file.txt", path.fullPath);
		}

		[Test]
		public static void EliminateDoubleBackSlashes()
		{
			Options o = new Options();
			var path = new Path("folder\\\\file.txt", o);

			Assert.AreEqual("", path.statusFlags);
			Assert.AreEqual("folder/file.txt", path.fullPath);
		}

		[Test]
		public static void CorrectFoldersBuildup()
		{
			Options o = new Options();
			var path = new Path("root/folder/file.txt", o);

			Assert.AreEqual("", path.statusFlags);
			Assert.AreEqual("root/folder/file.txt", path.fullPath);
			Assert.AreEqual("file.txt", path.fileOrFolder);
			Assert.That(new List<string> { "root", "folder" }, Is.EquivalentTo(path.folders));
		}
	}
}
