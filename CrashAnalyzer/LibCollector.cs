using System;
using System.IO;
using System.Collections.Generic;

namespace CrashAnalyzer {
	public class LibCollector {

		public Dictionary<string, string> ApkLibPathes { get; private set; } 

		string _bundleName = null;
		string _apkDirPath = null;
		List<CrashDumpLine> _lines = null;

		public LibCollector(string bundleName, string apkDirPath, List<CrashDumpLine> lines) {
			_bundleName = bundleName;
			_apkDirPath = apkDirPath;
			_lines = lines;
		}

		bool IsApkLib(string path) {
			return (path != null) && path.Contains(_bundleName) && !path.EndsWith(".dex", StringComparison.OrdinalIgnoreCase);
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
			ApkLibPathes = new Dictionary<string, string>();
			foreach (var line in _lines) {
				var path = line.Path;
				if( ApkLibPathes.ContainsKey(path) ) {
					continue;
				}
				if ( IsApkLib(line.Path) ) {
					var convertedPath = ConvertToApkPath(path);
					if ( !string.IsNullOrEmpty(convertedPath) ) {
						ApkLibPathes.Add(path, convertedPath);
					}
				}
			}
			Console.WriteLine($"Found {ApkLibPathes.Count} libs in apk.");
		}
	}
}
