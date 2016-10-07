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
				InfoBox.AppendText("Starting installation of Game Ready Driver version " + LatestVersion + "\n\n");
				InfoBox.Text += GetDownloadLink() + "\n\n";

				DownloadOK = true;
				ExtractOK = true;
				InstallOK = true;
			}

			public async Task Download()
			{
				InfoBox.AppendText("Downloading...");
				client = new WebClient();
				await client.DownloadFileTaskAsync(new Uri(GetDownloadLink()), downloadPath + "update.exe");
			}

			public async Task Download(ProgressBar progressBar)
			{
				InfoBox.AppendText("Downloading...");
				client = new WebClient();
				this.progressBar = progressBar;
				client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
				client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
				await client.DownloadFileTaskAsync(new Uri(GetDownloadLink()), downloadPath + "update.exe");
			}

			public async Task Extract(bool silent)
			{
				InfoBox.AppendText("Getting unpack location...");
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

				InfoBox.AppendText("Unpacking...");
				extract.Start();
				await Task.Run(() => extract.WaitForExit());

				if (extract.ExitCode == 0)
				{
					InfoBox.Text += "OK\n\nRemoving unwanted components:\n";
					if (Directory.Exists(extractPath + "Display.Update\\"))
					{
						Directory.Delete(extractPath + "Display.Update\\", true);
						InfoBox.AppendText(" - Removing Display.Updaten\n");
					}
					if (Directory.Exists(extractPath + "GFExperience\\"))
					{
						Directory.Delete(extractPath + "GFExperience\\", true);
						InfoBox.AppendText(" - Removing GFExperience\n");
					}
					if (Directory.Exists(extractPath + "GFExperience.NvStreamSrv\\"))
					{
						Directory.Delete(extractPath + "GFExperience.NvStreamSrv\\", true);
						InfoBox.AppendText(" - Removing GFExperience.NvStreamSrv\n");
					}
					if (Directory.Exists(extractPath + "GfExperienceService\\"))
					{
						Directory.Delete(extractPath + "GfExperienceService\\", true);
						InfoBox.AppendText(" - Removing GfExperienceService\n");
					}
					if (Directory.Exists(extractPath + "HDAudio\\"))
					{
						Directory.Delete(extractPath + "HDAudio\\", true);
						InfoBox.AppendText(" - Removing HDAudio\n");
					}
					if (Directory.Exists(extractPath + "NV3DVision\\"))
					{
						Directory.Delete(extractPath + "NV3DVision\\", true);
						InfoBox.AppendText(" - Removing NV3DVision\n");
					}
					if (Directory.Exists(extractPath + "NV3DVisionUSB.Driver\\"))
					{
						Directory.Delete(extractPath + "NV3DVisionUSB.Driver\\", true);
						InfoBox.AppendText(" - Removing NV3DVisionUSB.Driver\n");
					}
					if (Directory.Exists(extractPath + "PhysX\\"))
					{
						Directory.Delete(extractPath + "PhysX\\", true);
						InfoBox.AppendText(" - Removing PhysX\n");
					}
					if (Directory.Exists(extractPath + "ShadowPlay\\"))
					{
						Directory.Delete(extractPath + "ShadowPlay\\", true);
						InfoBox.AppendText(" - Removing ShadowPlay\n");
					}
					if (Directory.Exists(extractPath + "Update.Core\\"))
					{
						Directory.Delete(extractPath + "Update.Core\\", true);
						InfoBox.AppendText(" - Removing Update.Core\n\n");
					}
				}
				else
				{
					ExtractOK = false;
					InfoBox.AppendText("Error!");
				}
				extract.Dispose();
			}

			public async Task Install(bool silent)
			{
				Process install = new Process();
				InfoBox.AppendText("Installing...");
				install.StartInfo.FileName = extractPath + "setup.exe";
				if (silent)
				{
					install.StartInfo.Arguments = "/n /s /noeula /nofinish";
				}
				else
				{
					install.StartInfo.Arguments = "/n /passive /noeula /nofinish";
				}
				install.Start();
				await Task.Run(() => install.WaitForExit());

				if (install.ExitCode != 0)
				{
					InstallOK = false;
					InfoBox.AppendText("Error!\n\n");
				}
				else
				{
					InfoBox.AppendText("OK\n\n");
				}
			}

			public void Abort()
			{
				InfoBox.AppendText("Aborting operation:\n");
				if (client != null)
				{
					client.CancelAsync();
					InfoBox.AppendText(" - Stopping download client.\n\n");
				}
				if (extract != null && !extract.HasExited)
				{
					extract.Kill();
					InfoBox.AppendText(" - Stopping extraction process.\n\n");
				}
				Thread.Sleep(2000);
				CleanUp();
			}

			public void CleanUp()
			{
				if (Directory.Exists(downloadPath))
				{
					Directory.Delete(downloadPath, true);
					InfoBox.AppendText("Deleting download files.\n\n");

				}
				if (Directory.Exists(extractPath))
				{
					if (pastInstallation)
					{
						Directory.Delete(extractPath.Substring(0, 31), true); // deletes installation files only for installed version
					}
					else
					{
						Directory.Delete(extractPath.Substring(0, 10), true); // deletes entire NVIDIA directory in root
					}
					InfoBox.AppendText("Deleting unpacked instalation files.\n\n");
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
					InfoBox.AppendText("OK\n\n");
				}
				else
				{
					DownloadOK = false;
				}
				client.Dispose();
			}
		}
	}
}
