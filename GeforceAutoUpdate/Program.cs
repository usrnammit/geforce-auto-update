using System.Diagnostics;
using System.Windows.Forms;

namespace GeforceAutoUpdate
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length == 1 && args[0] == "-install")
			{
				// check if admin
				// install


			}
			else if (GameReadyDriver.UpdateNeeded)
			{
				DriverUpdatePrompt prompt = new DriverUpdatePrompt();
				Application.Run(prompt);
			}
		}
	}
}
