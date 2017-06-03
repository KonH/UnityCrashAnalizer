using System.Collections.Generic;

namespace CrashAnalyzer {
	public class CrashAnalyzer {

		public static void Start(Arguments args) {
			var apkDir = UnzipApkFile(args.ApkPath);

			var dump = new CrashDump(args.DumpPath);
			dump.Resolve();

			var collector = new LibCollector(apkDir, dump.Lines);
			collector.FindApkLibPathes();

			var libDumps = FindLibDumps(args.ObjDumpPath, collector.ApkLibPathes);
			AnalyzeDumpLines(args.DumpPath, dump.Lines, libDumps);
		}

		static string UnzipApkFile(string path) {
			var unpacker = new ZipUnpacker(path);
			unpacker.Unpack();
			return unpacker.DirectoryPath;
		}

		static Dictionary<string, string> FindLibDumps(string objDumpPath, Dictionary<string, string> apkLibPathes) {
			var libDumps = new Dictionary<string, string>();
			foreach (var libPath in apkLibPathes) {
				SoDumper dumper = new SoDumper(objDumpPath, libPath.Value);
				dumper.Dump();
				if (!string.IsNullOrEmpty(dumper.DumpPath)) {
					libDumps.Add(libPath.Key, dumper.DumpPath);
				}
			}
			return libDumps;
		}

		static void AnalyzeDumpLines(string dumpPath, List<CrashDumpLine> lines, Dictionary<string, string> libDumps) {
			var analyzers = InitAnalyzers(libDumps);
			var output = new Output(dumpPath);
			foreach (var line in lines) {
				var analyzer = FindAnalyzer(line.Path, analyzers);
				if ( analyzer != null ) {
					if ( analyzer.Analyze(line, output) ) {
						continue;
					}
				}
				output.AddLine(line);
			}
			output.Save();
		}

		static Dictionary<string, LibAnalyzer> InitAnalyzers(Dictionary<string, string> dumpPathes) {
			var analyzers = new Dictionary<string, LibAnalyzer>();
			foreach ( var pathPair in dumpPathes ) {
				var analyzer = new LibAnalyzer(pathPair.Value);
				if ( analyzer.Load() ) {
					analyzers.Add(pathPair.Key, analyzer);
				}
			}
			return analyzers;
		}

		static LibAnalyzer FindAnalyzer(string libPath, Dictionary<string, LibAnalyzer> analyzers) {
			LibAnalyzer analyzer = null;
			analyzers.TryGetValue(libPath, out analyzer);
			return analyzer;
		} 
	}
}