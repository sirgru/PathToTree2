namespace PathToTree2
{
	public partial class NodePrinter
	{
		internal class Coloration
		{
			private bool _useColoration;

			private static readonly string _resetColoration = "\\033[97m";

			public Coloration(bool useColoration)
			{
				_useColoration = useColoration;
			}

			static string GetColorationFromStatusFlags(string flags)
			{
				string hgStatusColoration = "";
				char flag;

				// Choose the best status flag for git and hg
				if (flags[0] == ' ' && flags.Length == 2) flag = flags[1];
				else flag = flags[0];

				// HG:
				// M = modified
				// A = added
				// R = removed
				// C = clean
				// ! = missing(deleted by non-hg command, but still tracked)
				// ? = not tracked
				// I = ignored
				//   = origin of the previous file(with --copies)

				// GIT:
				//   = unmodified
				// M = modified
				// A = added
				// D = deleted
				// R = renamed
				// C = copied
				// U = updated but unmerged
				// ?? = untracked path

				switch (flag) {
				case 'M': hgStatusColoration = "\\033[94m"; break;
				case 'A': hgStatusColoration = "\\033[35m"; break;
				case 'D': hgStatusColoration = "\\033[31m"; break;
				case 'R': hgStatusColoration = "\\033[31m"; break;
				case 'C': hgStatusColoration = "\\033[92m"; break;
				case '!': hgStatusColoration = "\\033[91m"; break;
				case '?': hgStatusColoration = "\\033[93m"; break;
				case 'I': hgStatusColoration = "\\033[90m"; break;
				case ' ': hgStatusColoration = "\\033[97m"; break;
				default: hgStatusColoration = "\\033[94m"; break;
				}
				return hgStatusColoration;
			}

			public string Over(Node.File file)
			{
				if (_useColoration) return GetColorationFromStatusFlags(file.statusFlags) + file.nameWithStatus + _resetColoration;
				else return file.nameWithStatus;
			}

		}
	}
}
