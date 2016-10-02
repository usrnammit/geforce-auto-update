using System;
using Microsoft.Win32;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.IO;

namespace GeforceAutoUpdate
{
	static class GameReadyDriver
	{
		public static readonly string LocalVersion;
		public static readonly string LatestVersion;
		public static readonly bool UpdateNeeded;

		static GameReadyDriver()
		{
			LocalVersion = RetrieveLocalVersion();
			LatestVersion = RetrieveLatestVersion();

			if (Double.Parse(LatestVersion, CultureInfo.InvariantCulture) > Double.Parse(LocalVersion, CultureInfo.InvariantCulture))
			{
				UpdateNeeded = true;
			}
			else
			{
				UpdateNeeded = false;
			}
		}

		private static string RetrieveLocalVersion()
		{
			RegistryKey localKey = null;
			if (Environment.Is64BitOperatingSystem)
			{
				localKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
			}
			else
			{
				localKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32);
			}

			localKey = localKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}_Display.Driver");
			string version = localKey.GetValue("DisplayVersion").ToString();
			version = "100.00"; // TODO: remove this (wrong local version for testing purposes)
			return version;
		}

		private static string RetrieveLatestVersion()
		{
			WebClient client = new WebClient();
			string chocoPackagePage = client.DownloadString("https://chocolatey.org/packages/geforce-game-ready-driver-win10");

			string pattern = @"Geforce Game Ready Driver for Windows 10 \d\d\d.\d\d";
			Regex r = new Regex(pattern);
			Match m = r.Match(chocoPackagePage);

			string version = m.Value.Substring(41);
			return version;
		}

		public static string GetDownloadLink()
		{
			if (Environment.Is64BitOperatingSystem)
			{
				RegistryKey localKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
				localKey = localKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
				string windowsVersion = localKey.GetValue("ProductName") as String;

				if (windowsVersion.StartsWith("Windows 10"))
				{
					string downloadLink = "http://us.download.nvidia.com/Windows/" + LatestVersion + "/" + LatestVersion + "-desktop-win10-64bit-international-whql.exe";
					return downloadLink;
				}
				else
				{
					string downloadLink = "http://us.download.nvidia.com/Windows/" + LatestVersion + "/" + LatestVersion + "-desktop-win8-win7-64bit-international-whql.exe";
					return downloadLink;
				}
			}
			else
			{
				RegistryKey localKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32);
				localKey = localKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
				string windowsVersion = localKey.GetValue("ProductName") as String;

				if (windowsVersion.StartsWith("Windows 10"))
				{
					string downloadLink = "http://us.download.nvidia.com/Windows/" + LocalVersion + "/" + LocalVersion + "-desktop-win10-32bit-international-whql.exe";
					return downloadLink;
				}
				else
				{
					string downloadLink = "http://us.download.nvidia.com/Windows/" + LocalVersion + "/" + LocalVersion + "-desktop-win8-win7-32bit-international-whql.exe";
					return downloadLink;
				}
			}
		}

		public static string GetUpdateDetails() // TODO: add changelog, link to nvidia site, license, disclaimer, ...
		{
			string updateDetails = "New version of GeForce Game Ready Drive is aviable.\n\n" +
									"Installed version: " + GameReadyDriver.LocalVersion + "\n" +
									"Latest version: " + GameReadyDriver.LatestVersion + "\n\n\n" +
									"Automatic: Downloads the update and performs silent install in the background.\nNot implemented yet\n\n" +
									"Manual: Opens direct link to the .exe in your default browser.\nPlease check that OS version and CPU architecture matches.\n\n";
			return updateDetails;
		}

		public static bool AutoUpdate()
		{
			DownloadAndInstall installer = new DownloadAndInstall();
			installer.Show();

			Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\GeforceAutoUpdate\\");
			string location = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\GeforceAutoUpdate\\";

			WebClient client = new WebClient();
			client.DownloadProgressChanged += installer.DownloadProgressChanged;
			client.DownloadFileAsync(new Uri(GetDownloadLink()), location);

			return false;
		}
	}
}
