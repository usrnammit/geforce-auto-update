using System;

namespace GeforceAutoUpdate
{
	class Program
	{
		static void Main(string[] args)
		{
			if (GameReadyDriver.UpdateNeeded)
			{
				// TODO: promt for manual / automatic update
				// retrieve OS information (OsInfo library)
				// add method to GameRadydriver.cs for getting update link
			}
			else
			{
				Console.WriteLine("GeForce Game Ready driver is up-to-date.");
			}
		}
	}
}
