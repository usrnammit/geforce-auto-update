using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeforceAutoUpdate
{
	public partial class DriverUpdatePromt : Form
	{
		public DriverUpdatePromt()
		{
			InitializeComponent();
		}

		private void DriverUpdatePromt_Load(object sender, EventArgs e)
		{
			UpdateInfo.Text = GameReadyDriver.GetUpdateDetails();
		}

		private void AutomaticButtonClicked(object sender, EventArgs e)
		{
			GameReadyDriver.PerformUpdate();
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
