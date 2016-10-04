using System.Windows.Forms;

namespace GeforceAutoUpdate
{
	class Program
	{
		static void Main(string[] args)
		{
			if (!GameReadyDriver.isInstalled)
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
