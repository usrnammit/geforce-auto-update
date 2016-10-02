using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeforceAutoUpdate
{
	public partial class DownloadAndInstall : Form
	{
		public DownloadAndInstall()
		{
			InitializeComponent();
		}

		private void DownloadAndInstall_Load(object sender, EventArgs e)
		{

		}

		public void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			progressBar.Value = e.ProgressPercentage;
		}
	}
}
