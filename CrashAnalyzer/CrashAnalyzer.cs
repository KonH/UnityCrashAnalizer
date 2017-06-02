using System;

namespace CrashAnalyzer {
    public class CrashAnalyzer {
        
		public static void Start(string apkPath) {
			var apkDir = UnzipApkFile(apkPath);
		}

		public static string UnzipApkFile(string path) {
			var unpacker = new ZipUnpacker(path);
            unpacker.Unpack();
            return unpacker.DirectoryPath;
		}
    }
}