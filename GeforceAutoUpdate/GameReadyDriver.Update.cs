using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace GeforceAutoUpdate
{
	static partial class GameReadyDriver
	{
		public class Update
		{
			private WebClient client;
			private string location;
			private bool usingProgressBar;
			private ProgressBar progressBar;

			public bool Downloading;
			public bool Installing;
			// TODO: rewrite as properties

			public Update()
			{
				client = new WebClient();
				location = Path.GetTempPath() + "GeForceAutoUpdate\\";
				Directory.CreateDirectory(location);

				usingProgressBar = false;
				progressBar = null;

				Downloading = false;
				Installing = false;
			}

			public Update(ProgressBar progressBar)
			{
				usingProgressBar = true;
				this.progressBar = progressBar;
			}

			public void Download()
			{
				Downloading = true;
				if (usingProgressBar)
				{
					client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
					client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
				}
				client.DownloadFileAsync(new Uri(GetDownloadLink()), location);
			}

			public void Install()
			{
				// TODO: install behaivior
			}

			public void Cancel()
			{
				// TODO: abort handling
			}

			private void CleanUp()
			{
				// TODO: delete install files
			}

			private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
			{
				progressBar.Maximum = (int)e.TotalBytesToReceive / 100;
				progressBar.Value = (int)e.BytesReceived / 100;
			}

			private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
			{
				// TODO: finished download handling + extraction
			}
		}
	}
}
