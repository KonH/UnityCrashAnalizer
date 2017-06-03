using System;
using System.IO;

namespace CrashAnalyzer {
	public class LibAnalyzer {

		string _path = null;
		string[] _contents = null;

		public LibAnalyzer(string path) {
			_path = path;
		}

		public bool Load() {
			Console.WriteLine($"Start load lib dump from '{_path}'");
			try {
				_contents = File.ReadAllLines(_path);
				Console.WriteLine($"Lib dump loaded from '{_path}' ({_contents.Length} lines)");
				return true;
			} catch( Exception e ) {
				Console.WriteLine($"Failed to load lib dump from '{_path}': '{e.Message}'");
			}
			return false;
		}

		string NormalizeAddress(string address) {
			return address.TrimStart(new char[] { '0' });
		}

		int FindLineWithAddress(string address) {
			for ( int i = 0; i < _contents.Length; i++ ) {
				if ( _contents[i].Contains(address) ) {
					return i;
				}
 			}
			return -1;
		}

		bool IsFuncHeader(string line) {
			return line.TrimEnd().EndsWith(">:", StringComparison.OrdinalIgnoreCase);
		}

		int FindFuncHeaderIndex(int callIndex) {
			for (int i = callIndex; i >= 0; i-- ) {
				var line = _contents[i];
				if ( IsFuncHeader(line) ) {
					return i;
				}
			}
			return -1;
		}

		public bool Analyze(CrashDumpLine line, Output output) {
			Console.WriteLine($"Start analyze line '{line.Number}'");
			var normalizedAddress = NormalizeAddress(line.Address);
			Console.WriteLine($"Address: '{line.Address}' => '{normalizedAddress}'");
			var libLineIndex = FindLineWithAddress(normalizedAddress);
			if ( libLineIndex >= 0 ) {
				var libLine = _contents[libLineIndex];
				var funcHeaderIndex = FindFuncHeaderIndex(libLineIndex);
				if ( funcHeaderIndex >= 0 ) {
					var funcHeader = _contents[funcHeaderIndex];
					Console.WriteLine($"Line analyzed: '{line.Number}'");
					var libEntry = new LibEntry(line, libLine, funcHeader);
					output.AddEntry(libEntry);
					return true;
				}
			}
			Console.WriteLine($"Line didn't analyzed: '{line.Number}'");
			return false;
		}
	}
}
