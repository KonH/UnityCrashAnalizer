using System;
using System.IO;
using System.Collections.Generic;

namespace CrashAnalyzer {
	class CrashDump {

		public List<CrashDumpLine> Lines { get; private set; }

		string _path = null;

		public CrashDump(string path) {
			_path = path;
		}

		public void Resolve() {
			try {
				Console.WriteLine($"Start resolve crash dump from '{_path}'");
				Lines = new List<CrashDumpLine>();
				var lines = File.ReadAllLines(_path);
				var backtraceStarted = false;
				foreach (var line in lines) {
					if ( backtraceStarted ) {
						if ( !TryAddLine(line) ) {
							break;
						}
					} else if ( line.Contains("backtrace:") ){
						backtraceStarted = true;
					}
				}
				Console.WriteLine($"Resolved crash dump lines: {Lines.Count}");
			} catch (Exception e) {
				Console.WriteLine($"Can't resolve dump file from '{_path}': '{e.Message}'");
			}
		}

		// ... #00 pc 00018c10  /system/lib/libc.so (strlen+71)
		bool TryAddLine(string line) {
			string number = null;
			string address = null;
			string path = null;
			string desc = null;
			for ( int i = 0; i < line.Length; i++ ) {
				var c = line[i];
				if ( number == null ) {
					if ( c == '#' ) {
						number = TakeToWhiteSpace(line, i, out i);
						TakeToWhiteSpace(line, i, out i); // ps
					} else {
						continue;
					}
				}
				if ( !char.IsWhiteSpace(c) ) {
					if ( address == null ) {
						address = TakeToWhiteSpace(line, i, out i);
					} else if ( path == null ) {
						path = TakeToWhiteSpace(line, i, out i);
					} else if ( desc == null ) {
						desc = TakeToWhiteSpace(line, i, out i);
					}
				}
			}
			if (!string.IsNullOrEmpty(number) && !string.IsNullOrEmpty(address) && !string.IsNullOrEmpty(path)) {
				var crashLine = new CrashDumpLine(number, address, path, desc);
				Lines.Add(crashLine);
				Console.WriteLine($"'{crashLine.Number}', '{crashLine.Address}', '{crashLine.Path}', '{crashLine.Desc}'");
				return true;
			}
			return false;
		}

		string TakeToWhiteSpace(string line, int startIndex, out int lastIndex) {
			string result = null;
			int i = startIndex;
			for (; i < line.Length; i++ ) {
				var c = line[i];
				if ( (result != null) && char.IsWhiteSpace(c) ) {
					break;
				}
				result += c;
			}
			lastIndex = i;
			return result;
		}
	}
}
