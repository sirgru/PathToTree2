using System;

namespace PathToTree2
{
	public partial class NodePrinter
	{
		public string result { get; private set; }

		private Options _options;
		private Coloration _color;

		private static string _fileIndent = " ";

		public NodePrinter(Node.Folder.Root root, Options o)
		{
			_options = o;
			_color = new Coloration(o.useColoration);

			AddSeparators(o);

			PrintExemptPaths(root);

			if (o.useInvisibleTree) {
				CreateInvisibleTree(root.value);
			}
			else {
				CreateNodeRoot(root.value);
			}

			AddSeparators(o);
		}

		private void PrintExemptPaths(Node.Folder.Root root)
		{
			foreach (var path in root.exemptPaths) {
				result += " " + path.fullPath + "\n";
			}
		}

		private void AddSeparators(Options o)
		{
			if (o.printLineSeparators) result += "──────────────────────────────────\n";
		}

		// Splitting the cases on root, 1st child, others
		// is in this case better than perfect recursion:
		// cleaner logic, less checks.
		private void CreateNodeRoot(Node.Folder node)
		{
			foreach (var file in node.fileChildren) {
				result += _fileIndent + _color.Over(file) + "\n";
			}
			foreach (var folder in node.folderChildren) {
				var connectionDownOptional = folder.hasChildFolder ? "┬ " : "+ ";
				result += " " + connectionDownOptional + folder.name + "\n";

				var fileCarryover = folder.hasChildFolder ? "│ " : "  ";
				foreach (var file in folder.fileChildren) {
					result += " " + fileCarryover + _fileIndent + _color.Over(file) + "\n";
				}

				foreach (var inner in folder.folderChildren) {
					CeateForNode(inner, " ");
				}
			}
		}

		private void CeateForNode(Node.Folder node, string carryoverPrefix)
		{
			string connectionUp = node.isLastChild ? "└─" : "├─";
			string connectionDown = node.hasChildFolder ? "┬ " : "─ ";
			result += carryoverPrefix + connectionUp + connectionDown + node.name + "\n";

			var fileCarryover = node.hasChildFolder ? "│ " : "  ";
			var newCarryover = node.isLastChild ? "  " : "│ ";
			foreach (var file in node.fileChildren) {
				result += carryoverPrefix + newCarryover + fileCarryover + _fileIndent + _color.Over(file) + "\n";
			}

			foreach (var folder in node.folderChildren) {
				CeateForNode(folder, carryoverPrefix + newCarryover);
			}
		}

		private void CreateInvisibleTree(Node.Folder node)
		{
			string folderPreix = (node.depth > 0)? "> " : "";
			string spacing = new String(' ', Math.Max(0, node.depth * 2 - 1));
			if (node.depth > 0) result += spacing + folderPreix + node.name + "\n";

			foreach (var file in node.fileChildren) {
				var contextSpacing = node.depth == 0 ? " " : "  ";
				result += spacing + contextSpacing + _color.Over(file) + "\n";
			}
			foreach (var folder in node.folderChildren) {
				CreateInvisibleTree(folder);
			}
		}

		public override string ToString()
		{
			return result;
		}
	}
}
