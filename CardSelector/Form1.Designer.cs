namespace CardSelector
{
    partial class FrmCardSelector
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpCardContainer = new System.Windows.Forms.GroupBox();
            this.txtCard = new System.Windows.Forms.TextBox();
            this.butMarkBlack = new System.Windows.Forms.Button();
            this.butMarkRed = new System.Windows.Forms.Button();
            this.butMarkYellow = new System.Windows.Forms.Button();
            this.butPick = new System.Windows.Forms.Button();
            this.butNext = new System.Windows.Forms.Button();
            this.butPrev = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chkPickAll = new System.Windows.Forms.CheckBox();
            this.grpCardContainer.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpCardContainer
            // 
            this.grpCardContainer.Controls.Add(this.txtCard);
            this.grpCardContainer.Location = new System.Drawing.Point(12, 27);
            this.grpCardContainer.Name = "grpCardContainer";
            this.grpCardContainer.Size = new System.Drawing.Size(312, 246);
            this.grpCardContainer.TabIndex = 0;
            this.grpCardContainer.TabStop = false;
            this.grpCardContainer.Text = "Card";
            // 
            // txtCard
            // 
            this.txtCard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCard.Location = new System.Drawing.Point(6, 19);
            this.txtCard.Multiline = true;
            this.txtCard.Name = "txtCard";
            this.txtCard.ReadOnly = true;
            this.txtCard.Size = new System.Drawing.Size(300, 221);
            this.txtCard.TabIndex = 0;
            this.txtCard.Text = "Ein paar Vollidioten, die lieber Spiele spielen, anstatt wie normale Menschen zu " +
    "interagieren";
            this.txtCard.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCardSelector_KeyDown);
            // 
            // butMarkBlack
            // 
            this.butMarkBlack.Location = new System.Drawing.Point(12, 279);
            this.butMarkBlack.Name = "butMarkBlack";
            this.butMarkBlack.Size = new System.Drawing.Size(100, 23);
            this.butMarkBlack.TabIndex = 1;
            this.butMarkBlack.Text = "Statement/Black";
            this.butMarkBlack.UseVisualStyleBackColor = true;
            this.butMarkBlack.Click += new System.EventHandler(this.butMarkBlack_Click);
            // 
            // butMarkRed
            // 
            this.butMarkRed.Location = new System.Drawing.Point(118, 279);
            this.butMarkRed.Name = "butMarkRed";
            this.butMarkRed.Size = new System.Drawing.Size(100, 23);
            this.butMarkRed.TabIndex = 2;
            this.butMarkRed.Text = "OBJECT/Red";
            this.butMarkRed.UseVisualStyleBackColor = true;
            this.butMarkRed.Click += new System.EventHandler(this.butMarkRed_Click);
            // 
            // butMarkYellow
            // 
            this.butMarkYellow.Location = new System.Drawing.Point(224, 279);
            this.butMarkYellow.Name = "butMarkYellow";
            this.butMarkYellow.Size = new System.Drawing.Size(100, 23);
            this.butMarkYellow.TabIndex = 3;
            this.butMarkYellow.Text = "Verb/Yellow";
            this.butMarkYellow.UseVisualStyleBackColor = true;
            this.butMarkYellow.Click += new System.EventHandler(this.butMarkYellow_Click);
            // 
            // butPick
            // 
            this.butPick.Location = new System.Drawing.Point(118, 308);
            this.butPick.Name = "butPick";
            this.butPick.Size = new System.Drawing.Size(100, 23);
            this.butPick.TabIndex = 4;
            this.butPick.Text = "Pick Card";
            this.butPick.UseVisualStyleBackColor = true;
            this.butPick.Click += new System.EventHandler(this.butPick_Click);
            // 
            // butNext
            // 
            this.butNext.Location = new System.Drawing.Point(224, 308);
            this.butNext.Name = "butNext";
            this.butNext.Size = new System.Drawing.Size(100, 23);
            this.butNext.TabIndex = 5;
            this.butNext.Text = ">";
            this.butNext.UseVisualStyleBackColor = true;
            this.butNext.Click += new System.EventHandler(this.butNext_Click);
            // 
            // butPrev
            // 
            this.butPrev.Location = new System.Drawing.Point(12, 308);
            this.butPrev.Name = "butPrev";
            this.butPrev.Size = new System.Drawing.Size(100, 23);
            this.butPrev.TabIndex = 6;
            this.butPrev.Text = "<";
            this.butPrev.UseVisualStyleBackColor = true;
            this.butPrev.Click += new System.EventHandler(this.butPrev_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(554, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveDatabaseToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.resetToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveDatabaseToolStripMenuItem
            // 
            this.saveDatabaseToolStripMenuItem.Name = "saveDatabaseToolStripMenuItem";
            this.saveDatabaseToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.saveDatabaseToolStripMenuItem.Text = "Save Database";
            this.saveDatabaseToolStripMenuItem.Click += new System.EventHandler(this.saveDatabaseToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.resetToolStripMenuItem.Text = "Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // chkPickAll
            // 
            this.chkPickAll.AutoSize = true;
            this.chkPickAll.Location = new System.Drawing.Point(330, 46);
            this.chkPickAll.Name = "chkPickAll";
            this.chkPickAll.Size = new System.Drawing.Size(89, 17);
            this.chkPickAll.TabIndex = 8;
            this.chkPickAll.Text = "Pick all mode";
            this.chkPickAll.UseVisualStyleBackColor = true;
            this.chkPickAll.CheckedChanged += new System.EventHandler(this.chkPickAll_CheckedChanged);
            // 
            // FrmCardSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 348);
            this.Controls.Add(this.chkPickAll);
            this.Controls.Add(this.butPrev);
            this.Controls.Add(this.butNext);
            this.Controls.Add(this.butPick);
            this.Controls.Add(this.butMarkYellow);
            this.Controls.Add(this.butMarkRed);
            this.Controls.Add(this.butMarkBlack);
            this.Controls.Add(this.grpCardContainer);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmCardSelector";
            this.Text = "CardSelector";
            this.Load += new System.EventHandler(this.FrmCardSelector_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCardSelector_KeyDown);
            this.grpCardContainer.ResumeLayout(false);
            this.grpCardContainer.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCardContainer;
        private System.Windows.Forms.TextBox txtCard;
        private System.Windows.Forms.Button butMarkBlack;
        private System.Windows.Forms.Button butMarkRed;
        private System.Windows.Forms.Button butMarkYellow;
        private System.Windows.Forms.Button butPick;
        private System.Windows.Forms.Button butNext;
        private System.Windows.Forms.Button butPrev;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkPickAll;
    }
}

