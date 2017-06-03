using System;

namespace CrashAnalyzer {
	class Program {
		static void Main(string[] args) {
			var arguments = new Arguments(args);
			if ( arguments.IsValid ) {
				CrashAnalyzer.Start(arguments);
			} else {
				Console.WriteLine("Usage:");
				Console.WriteLine("-dump DUMP_PATH // path to crash dump file");
				Console.WriteLine("-apk APK_PATH // path to apk");
				Console.WriteLine("-objdump OBJ_DUMP_PATH // path to Android NDK objdump utility (ex. arm-linux-androideabi-objdump)");
			}
			Console.ReadKey();
		}
	}
}
