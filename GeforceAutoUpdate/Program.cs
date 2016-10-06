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
			else
			{
				if (!GameReadyDriver.IsInstalled)
				{
					MessageBox.Show("Unable to retrieve local version of Game Ready Driver.");
				}
				else if (GameReadyDriver.UpdateNeeded)
				{
					DriverUpdatePrompt prompt = new DriverUpdatePrompt();
					Application.Run(prompt);
				}
			}
		}
	}
}
