namespace InteligenciaArtificial
{
    partial class ChatNativo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatNativo));
            this.button1 = new System.Windows.Forms.Button();
            this.chatText = new System.Windows.Forms.TextBox();
            this.chatHistoric = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(36)))), ((int)(((byte)(98)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(-1, 403);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(332, 43);
            this.button1.TabIndex = 0;
            this.button1.Text = "ENVIAR";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chatText
            // 
            this.chatText.AcceptsReturn = true;
            this.chatText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chatText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chatText.ForeColor = System.Drawing.Color.Black;
            this.chatText.Location = new System.Drawing.Point(-1, 334);
            this.chatText.Multiline = true;
            this.chatText.Name = "chatText";
            this.chatText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.chatText.Size = new System.Drawing.Size(346, 69);
            this.chatText.TabIndex = 2;
            this.chatText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chatText_KeyDown);
            // 
            // chatHistoric
            // 
            this.chatHistoric.AcceptsReturn = true;
            this.chatHistoric.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatHistoric.BackColor = System.Drawing.Color.White;
            this.chatHistoric.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chatHistoric.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chatHistoric.ForeColor = System.Drawing.Color.Black;
            this.chatHistoric.Location = new System.Drawing.Point(-1, 0);
            this.chatHistoric.Multiline = true;
            this.chatHistoric.Name = "chatHistoric";
            this.chatHistoric.ReadOnly = true;
            this.chatHistoric.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.chatHistoric.Size = new System.Drawing.Size(346, 329);
            this.chatHistoric.TabIndex = 1;
            this.chatHistoric.TextChanged += new System.EventHandler(this.chatHistoric_TextChanged);
            // 
            // ChatNativo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(330, 445);
            this.Controls.Add(this.chatHistoric);
            this.Controls.Add(this.chatText);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(346, 484);
            this.MinimumSize = new System.Drawing.Size(346, 484);
            this.Name = "ChatNativo";
            this.Opacity = 0.98D;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CHAT JARVIS";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox chatText;
        private System.Windows.Forms.TextBox chatHistoric;
    }
}