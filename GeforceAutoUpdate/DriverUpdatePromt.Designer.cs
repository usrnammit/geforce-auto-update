﻿namespace GeforceAutoUpdate
{
	partial class DriverUpdatePromt
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DriverUpdatePromt));
			this.Automatic = new System.Windows.Forms.Button();
			this.Manual = new System.Windows.Forms.Button();
			this.Cancel = new System.Windows.Forms.Button();
			this.UpdateInfo = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// Automatic
			// 
			this.Automatic.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.Automatic.Enabled = false;
			this.Automatic.Location = new System.Drawing.Point(12, 343);
			this.Automatic.Name = "Automatic";
			this.Automatic.Size = new System.Drawing.Size(91, 38);
			this.Automatic.TabIndex = 0;
			this.Automatic.Text = "Automatic";
			this.Automatic.UseVisualStyleBackColor = true;
			this.Automatic.Click += new System.EventHandler(this.AutomaticButtonClicked);
			// 
			// Manual
			// 
			this.Manual.Location = new System.Drawing.Point(163, 343);
			this.Manual.Name = "Manual";
			this.Manual.Size = new System.Drawing.Size(91, 38);
			this.Manual.TabIndex = 1;
			this.Manual.Text = "Manual";
			this.Manual.UseVisualStyleBackColor = true;
			this.Manual.Click += new System.EventHandler(this.ManualButtonClicked);
			// 
			// Cancel
			// 
			this.Cancel.Location = new System.Drawing.Point(316, 346);
			this.Cancel.Name = "Cancel";
			this.Cancel.Size = new System.Drawing.Size(91, 38);
			this.Cancel.TabIndex = 2;
			this.Cancel.Text = "Cancel";
			this.Cancel.UseVisualStyleBackColor = true;
			this.Cancel.Click += new System.EventHandler(this.CancelButtonClicked);
			// 
			// UpdateInfo
			// 
			this.UpdateInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.UpdateInfo.Location = new System.Drawing.Point(12, 12);
			this.UpdateInfo.Name = "UpdateInfo";
			this.UpdateInfo.ReadOnly = true;
			this.UpdateInfo.Size = new System.Drawing.Size(395, 325);
			this.UpdateInfo.TabIndex = 3;
			this.UpdateInfo.Text = "";
			// 
			// DriverUpdatePromt
			// 
			this.AccessibleName = "DriverUpdatePrompt";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(419, 396);
			this.Controls.Add(this.UpdateInfo);
			this.Controls.Add(this.Cancel);
			this.Controls.Add(this.Manual);
			this.Controls.Add(this.Automatic);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "DriverUpdatePromt";
			this.Text = "GeForce Auto Updater";
			this.Load += new System.EventHandler(this.DriverUpdatePromt_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button Automatic;
		private System.Windows.Forms.Button Manual;
		private System.Windows.Forms.Button Cancel;
		private System.Windows.Forms.RichTextBox UpdateInfo;
	}
}