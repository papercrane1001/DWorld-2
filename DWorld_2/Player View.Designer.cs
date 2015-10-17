namespace DWorld_2
{
    partial class Player_View
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
            this.picturePView = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picturePView)).BeginInit();
            this.SuspendLayout();
            // 
            // picturePView
            // 
            this.picturePView.Location = new System.Drawing.Point(12, 12);
            this.picturePView.Name = "picturePView";
            this.picturePView.Size = new System.Drawing.Size(100, 50);
            this.picturePView.TabIndex = 0;
            this.picturePView.TabStop = false;
            // 
            // Player_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.picturePView);
            this.Name = "Player_View";
            this.Text = "Player_View";
            ((System.ComponentModel.ISupportInitialize)(this.picturePView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picturePView;
    }
}