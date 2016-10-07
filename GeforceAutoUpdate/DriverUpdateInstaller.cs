﻿using System;
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

		private async void Install()
		{
			GameReadyDriver.Update update = new GameReadyDriver.Update();
			await update.Download(progressBar);
			update.Extract(silentCheckBox.Checked);
			// update.Install();
			// update.CleanUp();
			this.Close();
		}
	}
}