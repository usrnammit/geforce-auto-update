using System;
using Microsoft.Win32;
using System.Globalization;

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
			version = "100.00"; // TODO: remove this
			return version;
		}

		private static string RetrieveLatestVersion()
		{
			// TODO: logic for retrieving latest version
			string version = "372.90";
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
					string downloadLink = "http://us.download.nvidia.com/Windows/" + LocalVersion + "/" + LocalVersion + "-desktop-win10-64bit-international-whql.exe";
					return downloadLink;
				}
				else
				{
					string downloadLink = "http://us.download.nvidia.com/Windows/" + LocalVersion + "/" + LocalVersion + "-desktop-win8-win7-64bit-international-whql.exe";
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
	}
}
