using System;

namespace CrashAnalyzer {

	public class CrashDumpLine {
		public string Number { get; private set; }
		public string Address { get; private set; }
		public string Path { get; private set; }
		public string Desc { get; private set; }

		public CrashDumpLine(string number, string address, string path, string desc) {
			Number = number;
			Address = address;
			Path = path;
			Desc = desc;
		}
	}
}
