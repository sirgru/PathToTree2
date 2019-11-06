using System.Collections.Generic;

namespace PathToTree2
{
	public class Path
	{
		public readonly string statusFlags = "";
		public readonly string fullPath;
		public readonly SC_Type type;
		public readonly List<string> folders = new List<string>();
		public readonly string fileOrFolder = "";
		public readonly bool isExempt;

		public enum SC_Type
		{
			Git, Hg
		}

		public Path(string input, Options o)
		{
			if (o.hasStatusFlags) {
				if (input.Length < 3) throw new InputException($"Switch for status flags is set, but path '{input}' does not contain a flag.");

				if (input[1] == ' ' && input[2] != ' ') {
					type = SC_Type.Hg;
				}
				else if (input[2] == ' ' && input.Length > 3 && input[3] != ' ') {
					type = SC_Type.Git;
				}
				else throw new InputException($"Could not parse flags for path '{input}'.");

				if (type == SC_Type.Git) {
					if (input.Contains(" -> ")) {
						// Git uses -> to denote moves.
						// We leave these paths as they are
						fullPath = input;
						isExempt = true;
						return;
					}

					statusFlags = input.Substring(0, 2).Replace(' ', '_');
					fullPath = input.Substring(3);
					// Git puts qutations marks if paths have spaces
					if (fullPath.StartsWith('"') && fullPath.EndsWith('"')) fullPath = fullPath.Substring(1, fullPath.Length - 2);
				}
				else {
					statusFlags = input.Substring(0, 1);
					fullPath = input.Substring(2);
				}
			}
			else {
				fullPath = input;
			}

			fullPath = fullPath.Replace('\\', '/');
			while (fullPath.Contains("//")) fullPath = fullPath.Replace("//", "/");

			var split = fullPath.Split('/');
			for (int i = 0; i < split.Length - 1; i++) {
				folders.Add(split[i]);
			}
			fileOrFolder = split[split.Length - 1];
		}

		public string GetFolderAtDepth(int depth)
		{
			if (folders.Count > depth) return folders[depth];
			else return null;
		}
	}
}
