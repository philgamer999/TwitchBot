namespace TwitchBot
{
    partial class BotController
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BotController));
            this.ButtonToggle = new System.Windows.Forms.Button();
            this.ButtonChatFish = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButtonToggle
            // 
            this.ButtonToggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ButtonToggle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ButtonToggle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ButtonToggle.Location = new System.Drawing.Point(108, 54);
            this.ButtonToggle.Name = "ButtonToggle";
            this.ButtonToggle.Size = new System.Drawing.Size(75, 23);
            this.ButtonToggle.TabIndex = 0;
            this.ButtonToggle.Text = "Toggle Bot";
            this.ButtonToggle.UseVisualStyleBackColor = false;
            this.ButtonToggle.Click += new System.EventHandler(this.ButtonToggle_Click);
            // 
            // ButtonChatFish
            // 
            this.ButtonChatFish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ButtonChatFish.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ButtonChatFish.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ButtonChatFish.Location = new System.Drawing.Point(108, 102);
            this.ButtonChatFish.Name = "ButtonChatFish";
            this.ButtonChatFish.Size = new System.Drawing.Size(75, 23);
            this.ButtonChatFish.TabIndex = 1;
            this.ButtonChatFish.Text = "Chat Fish";
            this.ButtonChatFish.UseVisualStyleBackColor = false;
            this.ButtonChatFish.Click += new System.EventHandler(this.ButtonChatFish_Click);
            // 
            // BotController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(284, 361);
            this.Controls.Add(this.ButtonChatFish);
            this.Controls.Add(this.ButtonToggle);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BotController";
            this.Text = "BotController";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonToggle;
        private System.Windows.Forms.Button ButtonChatFish;
    }
}