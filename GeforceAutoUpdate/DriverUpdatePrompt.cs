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

			UpdateInfo.Text += "======================\n\nDonwloading update...";
			if (!update.Download(progressBar))
			{
				MessageBox.Show("Something went wrong with the download.");
				update.Abort();
				this.Close();
			}
			UpdateInfo.Text += "OK!\nExtracting files...";
			if (!update.Extract())
			{
				MessageBox.Show("Something went wrong with the archive extraction.");
				update.Abort();
				this.Close();
			}
			UpdateInfo.Text += "OK!\nStarting nVidia Driver Installer.";
			MyCancelButton.Enabled = false;
			if (!update.Install())
			{
				MessageBox.Show("Something went wrong during the installation.");
			}
			UpdateInfo.Text += "\n\nGeForce Game Ready Driver was successfully updated.\nDeleting installation files and exiting.";
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
