using System;

namespace CrashAnalyzer {
	class Program {
		static void Main(string[] args) {
			var arguments = new Arguments(args);
			if ( arguments.IsValid ) {
				CrashAnalyzer.Start(arguments);
			} else {
				Console.WriteLine("Usage:");
				Console.WriteLine("-dump DUMP_PATH");
				Console.WriteLine("-apk APK_PATH");
			}
		}
	}
}
