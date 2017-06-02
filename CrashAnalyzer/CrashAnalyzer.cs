using System;

namespace CrashAnalyzer {
	public class CrashAnalyzer {

		public static void Start(Arguments args) {
			var apkDir = UnzipApkFile(args.ApkPath);
			var dump = new CrashDump(args.DumpPath);
			dump.Resolve();

			SoDumper dumper = new SoDumper("/Users/konh/Projects/Unity3D/UnityCrashAnalizer/Builds/build_mono/lib/armeabi-v7a/libmono.so");
			dumper.Dump();
		}

		public static string UnzipApkFile(string path) {
			var unpacker = new ZipUnpacker(path);
			unpacker.Unpack();
			return unpacker.DirectoryPath;
		}
	}
}