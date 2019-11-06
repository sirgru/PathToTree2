using System.Collections.Generic;

namespace PathToTree2
{
	public class Node
	{
		public class Folder
		{
			public readonly int depth;
			public readonly string name;
			public readonly List<File> fileChildren = new List<File>();
			public readonly List<Folder> folderChildren = new List<Folder>();

			public bool isLastChild { get; private set; }

			private List<Path> _pathsForFolder = new List<Path>();
			private List<Path> _pathsForFiles = new List<Path>();
			private List<Path> _pathsForFileOrFolder = new List<Path>();
			private List<Path> _pathsNoMoreFolders = new List<Path>();

			private MultiDictionary<string, Path> _folderToPaths = new MultiDictionary<string, Path>();

			public class Root
			{
				public readonly List<Path> exemptPaths = new List<Path>();
				List<Path> _paths = new List<Path>();
				Folder _root;

				public Root(List<Path> paths)
				{
					foreach (var path in paths) {
						if (!path.isExempt) _paths.Add(path);
						else exemptPaths.Add(path);
					}
					_root = new Folder(_paths, "", 0);
				}

				public Folder value => _root;
			}

			private Folder(List<Path> paths, string name, int depth)
			{
				this.depth = depth;
				this.name = name;
				_pathsForFolder = paths;

				BuildSureFolders();

				BuildMaybeFolders();

				CreateFileChildren();

				CreateFolderChildren();
			}

			private void BuildSureFolders()
			{
				if (_folderToPaths.Count != 0) throw new Termination("Uncleaned State.");
				foreach (var path in _pathsForFolder) {
					string folder = path.GetFolderAtDepth(depth);

					if (folder == null) {
						_pathsNoMoreFolders.Add(path);
						continue;
					}

					_folderToPaths.Add(folder, path);
				}

				if (_pathsForFileOrFolder.Count != 0) throw new Termination("Uncleaned State.");
				foreach (var path in _pathsNoMoreFolders) {
					_pathsForFolder.Remove(path);
					_pathsForFileOrFolder.Add(path);
				}
			}

			private void BuildMaybeFolders()
			{
				// If any of "maybe folders" is a folder according to some other path
				// then we treat it as a folder, otherwise it will be a file.
				foreach (var path in _pathsForFileOrFolder) {
					if (!_folderToPaths.ContainsKey(path.fileOrFolder)) {
						_pathsForFiles.Add(path);
					}
				}
			}

			private void CreateFileChildren()
			{
				foreach (var path in _pathsForFiles) {
					fileChildren.Add(new File(path.fileOrFolder, path.statusFlags));
				}
			}

			private void CreateFolderChildren()
			{
				Folder last = null;
				foreach (var item in _folderToPaths) {
					last = new Folder(item.Value, item.Key, depth + 1);
					folderChildren.Add(last);
				}
				if (last != null) last.isLastChild = true;
			}

			public bool hasChildFolder {
				get => folderChildren.Count > 0;
			}
		}

		public class File
		{
			public readonly string name;
			public readonly string statusFlags;

			internal File(string name, string statusFlags)
			{
				this.name = name;
				this.statusFlags = statusFlags;
			}

			public string nameWithStatus {
				get => statusFlags == "" ? name : statusFlags + " " + name;
			}
		}
	}
}
