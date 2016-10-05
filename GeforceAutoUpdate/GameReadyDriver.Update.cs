using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;

namespace GeforceAutoUpdate
{
	static partial class GameReadyDriver
	{
		public class Update
		{
			private WebClient client;
			private ProgressBar progressBar;
			private Process extract;
			private string location;
			private bool downloading;
			private bool downloadOK;


			public Update()
			{
				client = new WebClient();
				location = Path.GetTempPath() + "GeForceAutoUpdate\\";
				Directory.CreateDirectory(location);
				downloading = false;
				downloadOK = false;
			}

			public void Download()
			{
				downloading = true;
				client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
				client.DownloadFileAsync(new Uri(GetDownloadLink()), location + "update.exe");

				while (downloading)
				{
					Application.DoEvents();
				}
			}

			public bool Download(ProgressBar progressBar)
			{
				downloading = true;
				this.progressBar = progressBar;
				client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
				client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
				client.DownloadFileAsync(new Uri(GetDownloadLink()), location + "update.exe");

				while (downloading)
				{
					Application.DoEvents();
				}

				if (downloadOK)
				{
					return true;
				}
				else
				{
					return false;
				}
			}

			public bool Extract()
			{
				extract = new Process();
				extract.StartInfo.FileName = location + "update.exe";
				extract.StartInfo.Arguments = "-nr -y";
				extract.Start();
				extract.WaitForExit();
				if (extract.ExitCode == 0)
				{
					Directory.Delete(extractPath + "Display.Update\\", true);
					Directory.Delete(extractPath + "GFExperience\\", true);
					Directory.Delete(extractPath + "GFExperience.NvStreamSrv\\", true);
					Directory.Delete(extractPath + "GfExperienceService\\", true);
					Directory.Delete(extractPath + "HDAudio\\", true);
					Directory.Delete(extractPath + "NV3DVision\\", true);
					Directory.Delete(extractPath + "NV3DVisionUSB.Driver\\", true);
					Directory.Delete(extractPath + "PhysX\\", true);
					Directory.Delete(extractPath + "ShadowPlay\\", true);
					Directory.Delete(extractPath + "Update.Core\\", true);

					return true;
				}
				else
				{
					return false;
				}
			}

			public bool Install()
			{
				Process install = new Process();
				install.StartInfo.FileName = extractPath + "setup.exe";
				install.StartInfo.Arguments = "/n /passive /noeula /nofinish";
				install.Start();
				install.WaitForExit();
				if (install.ExitCode == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}

			public void Abort()
			{
				if (client != null)
				{
					client.CancelAsync();
					client.Dispose();
				}
				if (extract != null)
				{
					extract.Kill();
					extract.Dispose();
				}
				CleanUp();
			}

			public void CleanUp()
			{
				if (Directory.Exists(location))
				{
					Directory.Delete(location, true);
				}
				if (Directory.Exists("C:\\NVIDIAL"))
				{
					Directory.Delete("C:\\NVIDIA", true);
				}
			}

			private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
			{
				progressBar.Maximum = (int)e.TotalBytesToReceive / 100;
				progressBar.Value = (int)e.BytesReceived / 100;
			}

			private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
			{
				if (e.Error == null)
				{
					downloadOK = true;
				}
				downloading = false;
			}
		}
	}
}
