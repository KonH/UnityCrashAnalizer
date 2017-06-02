using System;
using System.IO;
using System.IO.Compression;

namespace CrashAnalyzer {
    public class ZipUnpacker {
        
		public string DirectoryPath { get; private set; }

		string _path = null;

		public ZipUnpacker(string path) {
			_path = path;
		}

		public bool Unpack() {
			try {
				var extension = ".zip";
				var dstDirPath = _path.Substring(0, _path.Length - extension.Length);
				if ( Directory.Exists(dstDirPath) ) {
					Directory.Delete(dstDirPath, true);
				}
				ZipFile.ExtractToDirectory(_path, dstDirPath);
				if ( Directory.Exists(dstDirPath) ) {
					DirectoryPath = dstDirPath;
					return true;
				}
			} catch(Exception e) {
				Console.WriteLine($"Can't extract APK file from '{_path}': {e.Message}");
			}
			return false;
		}
    }
}