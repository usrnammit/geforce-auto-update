using System.Windows.Forms;
using System.Security.Principal;

namespace GeforceAutoUpdate
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length == 1 && args[0] == "-install" && IsElevated())
			{
				DriverUpdateInstaller installer = new DriverUpdateInstaller();
				Application.Run(installer);
			}
			else if (GameReadyDriver.UpdateNeeded)
			{
				DriverUpdatePrompt prompt = new DriverUpdatePrompt();
				Application.Run(prompt);
			}
		}

		static bool IsElevated()
		{
			WindowsIdentity identity = WindowsIdentity.GetCurrent();
			WindowsPrincipal principal = new WindowsPrincipal(identity);
			bool elevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
			return elevated;
		}
	}
}

