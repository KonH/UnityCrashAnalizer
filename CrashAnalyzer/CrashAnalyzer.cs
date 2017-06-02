using System;

namespace CrashAnalyzer {
    public class CrashAnalyzer {
        
		public static void Start(string apkPath) {
			Console.WriteLine("CrashAnalyzer.Start");
			var apkDir = UnzipApkFile(apkPath);
		}

		public static string UnzipApkFile(string path) {
			var unpacker = new ZipUnpacker(path);
			if ( unpacker.Unpack() ) {
				Console.WriteLine($"APK extracted to '{unpacker.DirectoryPath}'");
				return unpacker.DirectoryPath;
			}
			return null;
		}
    }
}