using System;
using System.IO;
using System.Collections.Generic;

namespace CrashAnalyzer {
	public class Output {

		const string LibLineSeparator = "* * * * * * * * * *";

		string _path = null;
		List<string> _lines = new List<string>();

		public Output(string crashFilePath) {
			_path = ConvertPath(crashFilePath);
		}

		string ConvertPath(string crashFilePath) {
			return crashFilePath + ".summary.txt";
		}

		public void AddLine(CrashDumpLine line) {
			string content = $"{line.Number}\t{line.Address}\t{line.Path}";
			if ( !string.IsNullOrEmpty(line.Desc) ) {
				content += $"\t{line.Desc}";
			}
			_lines.Add(content);
		}

		public void AddEntry(LibEntry entry) {
			AddLine(entry.CrashLine);
			_lines.Add(LibLineSeparator);
			_lines.Add($"{entry.LibLine}\nfrom\n{entry.FuncHeader}");
			_lines.Add(LibLineSeparator);
		}

		public void Save() {
			try {
				File.WriteAllLines(_path, _lines.ToArray());
				Console.WriteLine($"Output saved to '{_path}'");
			} catch ( Exception e ) {
				Console.WriteLine($"Can't save output to '{_path}': '{e.Message}'");
			}
		}
	}
}
