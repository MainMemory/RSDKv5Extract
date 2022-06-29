using RSDKv5;
using System;
using System.Collections.Generic;
using System.IO;

namespace RSDKv5Extract
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length < 1)
			{
				Console.WriteLine("\tRSDKv5Extract Data.rsdk [FileList.txt] [OutputFolder]");
				return;
			}
			System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
			stopwatch.Start();
			string filelist = args.Length > 1 ? args[1] : Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "rsdk_files_list.txt");
			string outpath = args.Length > 2 ? args[2] : Path.ChangeExtension(args[0], null);
			List<string> filenames = null;
			if (File.Exists(filelist))
				filenames = new List<string>(File.ReadAllLines(filelist));
			Console.WriteLine("Extracting files...");
			DataPack dataPack = new DataPack(args[0], filenames);
			foreach (var file in dataPack.files)
			{
				string path = Path.Combine(outpath, file.name.name);
				Directory.CreateDirectory(Path.GetDirectoryName(path));
				File.WriteAllBytes(path, file.data);
			}
			File.WriteAllText(Path.Combine(outpath, "RSDKVer.txt"), ((char)dataPack.version).ToString());
			stopwatch.Stop();
			Console.WriteLine("Extracted {0} files in {1} seconds.", dataPack.files.Count, stopwatch.ElapsedMilliseconds / 1000m);
		}
	}
}
