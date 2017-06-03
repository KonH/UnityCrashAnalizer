using System;

namespace CrashAnalyzer {
	public class Arguments {
		public bool IsValid { get; private set; }
		public string ApkPath { get; private set; }
		public string DumpPath { get; private set; }
		public string ObjDumpPath { get; private set; }
		public string BundleName { get; private set; }

		public Arguments(string[] args) {
			for ( int i = 0; i < args.Length; i++ ) {
				var arg = args[i];
				switch (arg) {
					case "-apk":
						ApkPath = NextToken(args, i);
						break;

					case "-dump":
						DumpPath = NextToken(args, i);
						break;

					case "-objdump":
						ObjDumpPath = NextToken(args, i);
						break;

					case "-bundle":
						BundleName = NextToken(args, i);
						break;
				}
			}
			IsValid = 
				!string.IsNullOrEmpty(ApkPath) &&
				!string.IsNullOrEmpty(DumpPath) && 
				!string.IsNullOrEmpty(ObjDumpPath) &&
				!string.IsNullOrEmpty(BundleName);
		}

		string NextToken(string[] args, int prevIndex) {
			var index = prevIndex + 1;
			if ( index < args.Length ) {
				return args[index];
			}
			return null;
		}
	}
}