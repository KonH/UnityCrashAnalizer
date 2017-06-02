using System;

namespace CrashAnalyzer {
	class Program {
		static void Main(string[] args) {
			var arguments = new Arguments(args);
			if ( arguments.IsValid ) {
				CrashAnalyzer.Start(arguments.ApkPath);
			} else {
				Console.WriteLine("Usage:");
				Console.WriteLine("-apk APK_PATH");
			}
		}
	}
}
