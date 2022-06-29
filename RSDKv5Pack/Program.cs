using RSDKv5;
using System;
using System.IO;

namespace RSDKv5Pack
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length < 1)
			{
				Console.WriteLine("\tRSDKv5Pack DataFolder [Data.rsdk]");
				return;
			}
			System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
			stopwatch.Start();
			DataPack dataPack = new DataPack();
			string rsdkverpath = Path.Combine(args[0], "RSDKVer.txt");
			if (File.Exists(rsdkverpath))
				dataPack.version = (byte)File.ReadAllText(rsdkverpath)[0];
			foreach (var file in Directory.EnumerateFiles(args[0], "*", SearchOption.AllDirectories))
				if (Path.GetFileName(file) != "RSDKVer.txt")
					dataPack.files.Add(new DataPack.File()
					{
						name = new NameIdentifier(file.Substring(args[0].Length + 1)),
						data = File.ReadAllBytes(file)
					});
			dataPack.Write(args.Length > 1 ? args[1] : args[0] + ".rsdk");
			stopwatch.Stop();
			Console.WriteLine("Wrote {0} files in {1} seconds.", dataPack.files.Count, stopwatch.ElapsedMilliseconds / 1000m);
		}
	}
}
