using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeforceAutoUpdate
{
	public partial class DriverUpdateInstaller : Form
	{
		public DriverUpdateInstaller()
		{
			InitializeComponent();
			this.Show();
			Install();
		}

		GameReadyDriver.Update update;

		private async void Install()
		{
			update = new GameReadyDriver.Update(InfoBox);

			await update.Download(progressBar);
			if (!update.DownloadOK)
			{
				MessageBox.Show("Something went wrong during the download.");
				update.CleanUp();
				Environment.Exit(1);
			}

			silentCheckBox.Enabled = false;
			await update.Extract(silentCheckBox.Checked);
			if (!update.ExtractOK)
			{
				MessageBox.Show("Something went wrong during extraction.");
				update.CleanUp();
				Environment.Exit(1);
			}

			MyCancelButton.Enabled = false;
			await update.Install(silentCheckBox.Checked);
			if (!update.InstallOK)
			{
				MessageBox.Show("Something went wrong during installation.");
				update.CleanUp();
				Environment.Exit(1);
			}

			InfoBox.Text += "Installation was successful. The program will exit shortly.\n";
			this.Close();			
		}

		private void CancelButtonClicked(object sender, EventArgs e)
		{
			this.Close();
		}

		private void DriverUpdateInstaller_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (update != null)
			{
				update.Abort();
			}
			update.CleanUp();
		}
	}
}
