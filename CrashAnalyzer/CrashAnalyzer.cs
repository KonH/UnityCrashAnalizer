using System;

namespace CrashAnalyzer {
	public class CrashAnalyzer {

		public static void Start(Arguments args) {
			var apkDir = UnzipApkFile(args.ApkPath);
			var dump = new CrashDump(args.DumpPath);
			dump.Resolve();
		}

		public static string UnzipApkFile(string path) {
			var unpacker = new ZipUnpacker(path);
			unpacker.Unpack();
			return unpacker.DirectoryPath;
		}
	}
}