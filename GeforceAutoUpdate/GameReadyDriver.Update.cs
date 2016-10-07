using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace GeforceAutoUpdate
{
	static partial class GameReadyDriver
	{
		public class Update
		{
			private WebClient client;
			private ProgressBar progressBar;
			private Process extract;

			private string downloadPath;
			private string extractPath;



			public Update()
			{
				client = null;
				downloadPath = Path.GetTempPath() + "GeForceAutoUpdate\\";
				Directory.CreateDirectory(downloadPath);
			}

			public async Task Download()
			{
				client = new WebClient();
				await client.DownloadFileTaskAsync(new Uri(GetDownloadLink()), downloadPath + "update.exe");

			}

			public async Task Download(ProgressBar progressBar)
			{
				client = new WebClient();
				this.progressBar = progressBar;
				client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
				await client.DownloadFileTaskAsync(new Uri(GetDownloadLink()), downloadPath + "update.exe");
			}

			public void Extract(bool silent)
			{
				extract = new Process();
				extract.StartInfo.FileName = downloadPath + "update.exe";
				extract.StartInfo.Arguments = "-sfxconfig " + downloadPath + "config.txt";
				extract.Start();
				extract.WaitForExit();

				string[] config = File.ReadAllLines(downloadPath + "config.txt");
				extractPath = config[1].Substring(27).Replace(@"\\", @"\");

				if (silent)
				{
					extract.StartInfo.Arguments = "-gm2 -nr -y";
				}
				else
				{
					extract.StartInfo.Arguments = "-nr -y";
				}
				extract.Start();
				extract.WaitForExit();
				if (extract.ExitCode == 0)
				{
					// Directory.Delete(extractPath + "Display.Update\\", true);
					// Directory.Delete(extractPath + "GFExperience\\", true);
					// Directory.Delete(extractPath + "GFExperience.NvStreamSrv\\", true);
					// Directory.Delete(extractPath + "GfExperienceService\\", true);
					// Directory.Delete(extractPath + "HDAudio\\", true);
					// Directory.Delete(extractPath + "NV3DVision\\", true);
					// Directory.Delete(extractPath + "NV3DVisionUSB.Driver\\", true);
					// Directory.Delete(extractPath + "PhysX\\", true);
					// Directory.Delete(extractPath + "ShadowPlay\\", true);
					// Directory.Delete(extractPath + "Update.Core\\", true);
				}
			}

			// /n prevents PC from rebooting
			// replacing /passive with /s will result in fully silent install
			public bool Install()
			{
				Process install = new Process();
				install.StartInfo.FileName = "setup.exe";
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


			private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
			{
				progressBar.Maximum = (int)e.TotalBytesToReceive / 100;
				progressBar.Value = (int)e.BytesReceived / 100;
			}
		}
	}
}
