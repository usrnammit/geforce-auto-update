namespace GeforceAutoUpdate
{
	partial class DriverUpdatePrompt
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DriverUpdatePrompt));
			this.AutomaticButton = new System.Windows.Forms.Button();
			this.ManualButton = new System.Windows.Forms.Button();
			this.MyCancelButton = new System.Windows.Forms.Button();
			this.UpdateInfo = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// AutomaticButton
			// 
			this.AutomaticButton.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.AutomaticButton.Image = ((System.Drawing.Image)(resources.GetObject("AutomaticButton.Image")));
			this.AutomaticButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.AutomaticButton.Location = new System.Drawing.Point(12, 346);
			this.AutomaticButton.Name = "AutomaticButton";
			this.AutomaticButton.Size = new System.Drawing.Size(90, 38);
			this.AutomaticButton.TabIndex = 0;
			this.AutomaticButton.Text = "Automatic";
			this.AutomaticButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.AutomaticButton.UseVisualStyleBackColor = true;
			this.AutomaticButton.Click += new System.EventHandler(this.AutomaticButtonClicked);
			// 
			// ManualButton
			// 
			this.ManualButton.Location = new System.Drawing.Point(165, 346);
			this.ManualButton.Name = "ManualButton";
			this.ManualButton.Size = new System.Drawing.Size(90, 38);
			this.ManualButton.TabIndex = 1;
			this.ManualButton.Text = "Manual";
			this.ManualButton.UseVisualStyleBackColor = true;
			this.ManualButton.Click += new System.EventHandler(this.ManualButtonClicked);
			// 
			// MyCancelButton
			// 
			this.MyCancelButton.Location = new System.Drawing.Point(317, 346);
			this.MyCancelButton.Name = "MyCancelButton";
			this.MyCancelButton.Size = new System.Drawing.Size(90, 38);
			this.MyCancelButton.TabIndex = 2;
			this.MyCancelButton.Text = "Cancel";
			this.MyCancelButton.UseVisualStyleBackColor = true;
			this.MyCancelButton.Click += new System.EventHandler(this.CancelButtonClicked);
			// 
			// UpdateInfo
			// 
			this.UpdateInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.UpdateInfo.Location = new System.Drawing.Point(12, 12);
			this.UpdateInfo.Name = "UpdateInfo";
			this.UpdateInfo.ReadOnly = true;
			this.UpdateInfo.Size = new System.Drawing.Size(395, 328);
			this.UpdateInfo.TabIndex = 3;
			this.UpdateInfo.Text = "";
			// 
			// DriverUpdatePrompt
			// 
			this.AccessibleName = "DriverUpdatePrompt";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(419, 396);
			this.Controls.Add(this.UpdateInfo);
			this.Controls.Add(this.MyCancelButton);
			this.Controls.Add(this.ManualButton);
			this.Controls.Add(this.AutomaticButton);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "DriverUpdatePrompt";
			this.Text = "GeForce Auto Updater";
			this.Load += new System.EventHandler(this.DriverUpdatePromt_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button AutomaticButton;
		private System.Windows.Forms.Button ManualButton;
		private System.Windows.Forms.Button MyCancelButton;
		private System.Windows.Forms.RichTextBox UpdateInfo;
	}
}