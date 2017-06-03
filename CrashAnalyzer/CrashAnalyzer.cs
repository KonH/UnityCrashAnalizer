using System;

namespace CrashAnalyzer {
	public class CrashAnalyzer {

		public static void Start(Arguments args) {
			var apkDir = UnzipApkFile(args.ApkPath);

			var dump = new CrashDump(args.DumpPath);
			dump.Resolve();

			var collector = new LibCollector(apkDir, dump.Lines);
			collector.FindApkLibPathes();

			foreach ( var libPath in collector.ApkLibPathes ) {
				SoDumper dumper = new SoDumper(args.ObjDumpPath, libPath);
				dumper.Dump();
			}
		}

		public static string UnzipApkFile(string path) {
			var unpacker = new ZipUnpacker(path);
			unpacker.Unpack();
			return unpacker.DirectoryPath;
		}
	}
}