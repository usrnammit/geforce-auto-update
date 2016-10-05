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
		GameReadyDriver.Update update;

		public DriverUpdatePrompt()
		{
			InitializeComponent();
			update = null;
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

			update = new GameReadyDriver.Update();
			update.Download(progressBar);
			update.Extract();
			update.Install();
			update.CleanUp();

			this.Close();
		}

		private void ManualButtonClicked(object sender, EventArgs e)
		{
			Process.Start(GameReadyDriver.GetDownloadLink());
			this.Close();
		}

		private void CancelButtonClicked(object sender, EventArgs e)
		{
			if (update != null)
			{
				update.Abort();
			}
			this.Close();
		}
	}
}
