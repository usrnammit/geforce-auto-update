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
			AutomaticButton.Enabled = false;
			ManualButton.Enabled = false;

			UpdateInfo.Height = 296;
			progressBar.Show();

			GameReadyDriver.Update update = new GameReadyDriver.Update();

			update.Download();
			while (update.Downloading)
			{
				Application.DoEvents();
			}

			update.Install();
			while (update.Installing)
			{
				Application.DoEvents();
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
