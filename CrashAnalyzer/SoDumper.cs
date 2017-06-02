using System;
using System.IO;
using System.Diagnostics;

namespace CrashAnalyzer {
	public class SoDumper {

		string _path = null;

		public SoDumper(string path) {
			_path = path;
		}

		public void Dump() {
			try {
				Console.WriteLine($"Start dump file: '{_path}'");
				var toolPath = "/Users/konh/Library/Developer/Xamarin/android-ndk/android-ndk-r10e/toolchains/arm-linux-androideabi-4.8/prebuilt/darwin-x86_64/bin/arm-linux-androideabi-objdump";
				var args = $"-S {_path} -C";
				var startInfo = new ProcessStartInfo() {
					FileName = toolPath,
					Arguments = args,
					UseShellExecute = false,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				};
				var proc = Process.Start(startInfo);
				proc.Start();
				var output = proc.StandardOutput.ReadToEnd();
				var outputPath = _path + ".txt";
				File.WriteAllText(outputPath, output);
				Console.WriteLine($"Dump file for '{_path}' is saved to '{outputPath}'");
			} catch ( Exception e ) {
				Console.WriteLine($"Failed to dump file: '{_path}': '{e.Message}'");
			} 
		}
	}
}
