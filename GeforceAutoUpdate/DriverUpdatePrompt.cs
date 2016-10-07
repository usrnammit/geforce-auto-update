using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeforceAutoUpdate
{
	public partial class DriverUpdatePrompt : Form
	{
		public DriverUpdatePrompt()
		{
			InitializeComponent();
		}

		private void DriverUpdatePromt_Load(object sender, EventArgs e)
		{
			UpdateInfo.Text = GameReadyDriver.GetUpdateDetails();
		}

		private void AutomaticButtonClicked(object sender, EventArgs e)
		{
			// restart as admin with -install argument
			Process restart = new Process();
			restart.StartInfo.FileName = Process.GetCurrentProcess().MainModule.FileName;
			restart.StartInfo.UseShellExecute = true;
			restart.StartInfo.Arguments = "-install";
			restart.StartInfo.Verb = "runas";
			try
			{
				restart.Start();
				this.Close();
			}
			catch (Win32Exception)
			{
				UpdateInfo.Text += "Administrator privileges are required for automatic installation!\n";
			}
		}

		private void ManualButtonClicked(object sender, EventArgs e)
		{
			Process.Start(GameReadyDriver.GetDownloadLink());
			this.Close();
		}

		private void CancelButtonClicked(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
