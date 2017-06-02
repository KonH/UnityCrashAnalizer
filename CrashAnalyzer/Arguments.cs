using System;

namespace CrashAnalyzer {
    public class Arguments {
        public bool IsValid { get; private set; }
		public string ApkPath { get; private set; }

		public Arguments(string[] args) {
			for ( int i = 0; i < args.Length; i++ ) {
				var arg = args[i];
				switch (arg) {
					case "-apk":
						ApkPath = NextToken(args, i);
						break;
				}
			}
			IsValid = !string.IsNullOrEmpty(ApkPath);
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