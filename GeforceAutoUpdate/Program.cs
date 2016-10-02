using System;

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
				Console.WriteLine("Update {0} for GeForce Game Ready Driver is avaible.", GameReadyDriver.LatestVersion);
			}
			else
			{
				Console.WriteLine("GeForce Game Ready driver is up-to-date.");
			}
		}
	}
}
