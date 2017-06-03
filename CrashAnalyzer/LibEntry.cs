using System;

namespace CrashAnalyzer {

	public class LibEntry {

		public CrashDumpLine CrashLine { get; private set; } 
		public string LibLine { get; private set; }

		public LibEntry(CrashDumpLine crashLine, string libLine) {
			CrashLine = crashLine;
			LibLine = libLine;
		}
	}
}
