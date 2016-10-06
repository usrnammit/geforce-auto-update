namespace GeforceAutoUpdate
{
	partial class DriverUpdateInstaller
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DriverUpdateInstaller));
			this.MyCancelButton = new System.Windows.Forms.Button();
			this.InfoBox = new System.Windows.Forms.RichTextBox();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.silentChecked = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// MyCancelButton
			// 
			this.MyCancelButton.Location = new System.Drawing.Point(317, 346);
			this.MyCancelButton.Name = "MyCancelButton";
			this.MyCancelButton.Size = new System.Drawing.Size(90, 38);
			this.MyCancelButton.TabIndex = 3;
			this.MyCancelButton.Text = "Cancel";
			this.MyCancelButton.UseVisualStyleBackColor = true;
			// 
			// InfoBox
			// 
			this.InfoBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.InfoBox.Location = new System.Drawing.Point(12, 12);
			this.InfoBox.Name = "InfoBox";
			this.InfoBox.ReadOnly = true;
			this.InfoBox.Size = new System.Drawing.Size(395, 292);
			this.InfoBox.TabIndex = 4;
			this.InfoBox.Text = "";
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(12, 310);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(395, 30);
			this.progressBar.TabIndex = 5;
			// 
			// silentChecked
			// 
			this.silentChecked.AutoSize = true;
			this.silentChecked.Location = new System.Drawing.Point(19, 358);
			this.silentChecked.Name = "silentChecked";
			this.silentChecked.Size = new System.Drawing.Size(166, 17);
			this.silentChecked.TabIndex = 6;
			this.silentChecked.Text = "Don\'t show progress windows";
			this.silentChecked.UseVisualStyleBackColor = true;
			// 
			// DriverUpdateInstaller
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(419, 396);
			this.Controls.Add(this.silentChecked);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.InfoBox);
			this.Controls.Add(this.MyCancelButton);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "DriverUpdateInstaller";
			this.Text = "Driver Update Installer";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button MyCancelButton;
		private System.Windows.Forms.RichTextBox InfoBox;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.CheckBox silentChecked;
	}
}