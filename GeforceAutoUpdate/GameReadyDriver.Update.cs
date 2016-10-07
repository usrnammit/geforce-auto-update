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
			private RichTextBox InfoBox;
			private ProgressBar progressBar;
			private WebClient client;
			private Process extract;

			private string downloadPath;
			private string extractPath;

			private bool pastInstallation;

			public bool DownloadOK;
			public bool ExtractOK;
			public bool InstallOK;

			public Update(RichTextBox InfoBox)
			{
				downloadPath = Path.GetTempPath() + "GeForceAutoUpdate\\";
				Directory.CreateDirectory(downloadPath);

				if (Directory.Exists(Path.GetPathRoot(Environment.SystemDirectory) + "NVIDIA\\"))
				{
					pastInstallation = false;
				}
				else
				{
					pastInstallation = true;
				}

				this.InfoBox = InfoBox;
				InfoBox.Text = "Starting installation of Game Ready Driver version " + LatestVersion + "\n\n";
				InfoBox.Text += GetDownloadLink() + "\n\n";

				DownloadOK = true;
				ExtractOK = true;
				InstallOK = true;
			}

			public async Task Download()
			{
				InfoBox.Text += "Downloading...";
				client = new WebClient();
				await client.DownloadFileTaskAsync(new Uri(GetDownloadLink()), downloadPath + "update.exe");
			}

			public async Task Download(ProgressBar progressBar)
			{
				InfoBox.Text += "Downloading...";
				client = new WebClient();
				this.progressBar = progressBar;
				client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
				client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
				await client.DownloadFileTaskAsync(new Uri(GetDownloadLink()), downloadPath + "update.exe");
			}

			public async Task Extract(bool silent)
			{
				InfoBox.Text += "Getting unpack location...";
				extract = new Process();
				extract.StartInfo.FileName = downloadPath + "update.exe";
				extract.StartInfo.Arguments = "-sfxconfig " + downloadPath + "config.txt";
				extract.Start();
				extract.WaitForExit();
				if (extract.ExitCode == 0)
				{
					string[] config = File.ReadAllLines(downloadPath + "config.txt");
					extractPath = Path.GetPathRoot(Environment.SystemDirectory) + config[1].Substring(28).Replace(@"\\", @"\").Replace("\"", @"\");
					InfoBox.Text += "OK\n" + extractPath + "\n\n";
				}
				else
				{
					ExtractOK = false;
					return;
				}

				if (silent)
				{
					extract.StartInfo.Arguments = "-gm2 -nr -y";
				}
				else
				{
					extract.StartInfo.Arguments = "-nr -y";
				}

				InfoBox.Text += "Unpacking...";
				extract.Start();
				await Task.Run(() => extract.WaitForExit());

				if (extract.ExitCode == 0)
				{
					InfoBox.Text += "OK\n\nRemoving unwanted components:\n";
					if (Directory.Exists(extractPath + "Display.Update\\"))
					{
						Directory.Delete(extractPath + "Display.Update\\", true);
						InfoBox.Text += " - Removing Display.Updaten\n";
					}
					if (Directory.Exists(extractPath + "GFExperience\\"))
					{
						Directory.Delete(extractPath + "GFExperience\\", true);
						InfoBox.Text += " - Removing GFExperience\n";
					}
					if (Directory.Exists(extractPath + "GFExperience.NvStreamSrv\\"))
					{
						Directory.Delete(extractPath + "GFExperience.NvStreamSrv\\", true);
						InfoBox.Text += " - Removing GFExperience.NvStreamSrv\n";
					}
					if (Directory.Exists(extractPath + "GfExperienceService\\"))
					{
						Directory.Delete(extractPath + "GfExperienceService\\", true);
						InfoBox.Text += " - Removing GfExperienceService\n";
					}
					if (Directory.Exists(extractPath + "HDAudio\\"))
					{
						Directory.Delete(extractPath + "HDAudio\\", true);
						InfoBox.Text += " - Removing HDAudio\n";
					}
					if (Directory.Exists(extractPath + "NV3DVision\\"))
					{
						Directory.Delete(extractPath + "NV3DVision\\", true);
						InfoBox.Text += " - Removing NV3DVision\n";
					}
					if (Directory.Exists(extractPath + "NV3DVisionUSB.Driver\\"))
					{
						Directory.Delete(extractPath + "NV3DVisionUSB.Driver\\", true);
						InfoBox.Text += " - Removing NV3DVisionUSB.Driver\n";
					}
					if (Directory.Exists(extractPath + "PhysX\\"))
					{
						Directory.Delete(extractPath + "PhysX\\", true);
						InfoBox.Text += " - Removing PhysX\n";
					}
					if (Directory.Exists(extractPath + "ShadowPlay\\"))
					{
						Directory.Delete(extractPath + "ShadowPlay\\", true);
						InfoBox.Text += " - Removing ShadowPlay\n";
					}
					if (Directory.Exists(extractPath + "Update.Core\\"))
					{
						Directory.Delete(extractPath + "Update.Core\\", true);
						InfoBox.Text += " - Removing Update.Core\n";
					}
				}
				else
				{
					ExtractOK = false;
					return;
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

			private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
			{
				if (e.Error == null)
				{
					InfoBox.Text += "OK\n\n";
				}
				else
				{
					DownloadOK = false;
				}

			}
		}
	}
}
