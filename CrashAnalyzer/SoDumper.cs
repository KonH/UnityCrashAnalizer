using System;
using System.IO;
using System.Diagnostics;

namespace CrashAnalyzer {
	public class SoDumper {

		public string DumpPath { get; private set; }

		string _objDumpPath = null;
		string _libPath = null;

		public SoDumper(string objDumpPath, string libPath) {
			_objDumpPath = objDumpPath;
			_libPath = libPath;
		}

		public void Dump() {
			try {
				var outputPath = _libPath + ".txt";
				if ( File.Exists(outputPath) ) {
					Console.WriteLine($"Dump file {outputPath} already exist.");
				} else {
					Console.WriteLine($"Start dump file: '{_libPath}' using '{_objDumpPath}'");
					var args = $"-S {_libPath} -C";
					var startInfo = new ProcessStartInfo() {
						FileName = _objDumpPath,
						Arguments = args,
						UseShellExecute = false,
						RedirectStandardOutput = true,
						CreateNoWindow = true
					};
					var proc = Process.Start(startInfo);
					proc.Start();
					var output = proc.StandardOutput.ReadToEnd();
					File.WriteAllText(outputPath, output);
					Console.WriteLine($"Dump file for '{_libPath}' is saved to '{outputPath}'");
				}
				DumpPath = outputPath;
			} catch ( Exception e ) {
				Console.WriteLine($"Failed to dump file: '{_libPath}': '{e.Message}'");
			} 
		}
	}
}
