using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace GeforceAutoUpdate
{
	class Program
	{
		static void Main(string[] args)
		{
			if (GameReadyDriver.UpdateNeeded)
			{
				DriverUpdatePromt promt = new DriverUpdatePromt();
				Application.Run(promt);
			}
		}
	}
}
