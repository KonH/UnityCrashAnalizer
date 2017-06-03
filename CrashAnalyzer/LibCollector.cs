using System;
using System.IO;
using System.Collections.Generic;

namespace CrashAnalyzer {
	public class LibCollector {

		public List<string> ApkLibPathes { get; private set; } 

		List<CrashDumpLine> _lines = null;
		string _apkDirPath = null;

		public LibCollector(string apkDirPath, List<CrashDumpLine> lines) {
			_apkDirPath = apkDirPath;
			_lines = lines;
		}

		bool IsApkLib(string path) {
			return (path != null) && path.Contains("/app/");
		}

		string ConvertToApkPath(string path) {
			if ( path.Contains("/lib/arm/") ) {
				var libName = FindLibName(path);
				if ( !string.IsNullOrEmpty(libName) ) {
					return Path.Combine(_apkDirPath, "lib", "armeabi-v7a", libName);
				}
			}
			return null;
		}

		string FindLibName(string path) {
			var parts = path.Split('/');
			if ( parts.Length > 0 ) {
				return parts[parts.Length - 1];
			}
			return null;
		}

		public void FindApkLibPathes() {
			Console.WriteLine("Start analyze apk libs.");
			ApkLibPathes = new List<string>();
			foreach (var line in _lines) {
				if (IsApkLib(line.Path)) {
					var convertedPath = ConvertToApkPath(line.Path);
					if (!string.IsNullOrEmpty(convertedPath) && !ApkLibPathes.Contains(convertedPath)) {
						ApkLibPathes.Add(convertedPath);
					}
				}
			}
			Console.WriteLine($"Found {ApkLibPathes.Count} libs in apk.");
		}
	}
}
