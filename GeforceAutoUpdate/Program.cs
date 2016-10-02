using System;
using System.Diagnostics;

namespace GeforceAutoUpdate
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Installed version: " + GameReadyDriver.LocalVersion);
			Console.WriteLine("Latest version: " + GameReadyDriver.LatestVersion);
			Console.WriteLine("\n");

			if (GameReadyDriver.UpdateNeeded)
			{
				Console.WriteLine("Update {0} for GeForce Game Ready Driver is avaible. Would you like to download it? (y/n)", GameReadyDriver.LatestVersion);
				string s = Console.ReadLine();
				if (s == "y")
				{
					Process.Start(GameReadyDriver.GetDownloadLink());
				}
			}
			else
			{
				Console.WriteLine("GeForce Game Ready driver is up-to-date.");
			}
		}
	}
}
