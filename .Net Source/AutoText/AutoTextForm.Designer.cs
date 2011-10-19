namespace AutoText
{
    partial class AutoTextForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoTextForm));
            this.autotextTextbox = new System.Windows.Forms.TextBox();
            this.topmostDetectorTimer = new System.Windows.Forms.Timer(this.components);
            this.companiesCombo = new System.Windows.Forms.ComboBox();
            this.countriesCombo = new System.Windows.Forms.ComboBox();
            this.autotextCombo = new System.Windows.Forms.ComboBox();
            this.toggleButton = new System.Windows.Forms.Button();
            this.buttonToolstrip = new System.Windows.Forms.ToolStrip();
            this.deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.reloadToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.pasteButton = new System.Windows.Forms.Button();
            this.buttonToolstrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // autotextTextbox
            // 
            this.autotextTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autotextTextbox.Location = new System.Drawing.Point(12, 84);
            this.autotextTextbox.Multiline = true;
            this.autotextTextbox.Name = "autotextTextbox";
            this.autotextTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.autotextTextbox.Size = new System.Drawing.Size(254, 110);
            this.autotextTextbox.TabIndex = 1;
            this.autotextTextbox.Text = "Autotext Text";
            this.autotextTextbox.TextChanged += new System.EventHandler(this.autotextTextbox_TextChanged);
            // 
            // topmostDetectorTimer
            // 
            this.topmostDetectorTimer.Enabled = true;
            this.topmostDetectorTimer.Tick += new System.EventHandler(this.topmostDetectorTimer_Tick);
            // 
            // companiesCombo
            // 
            this.companiesCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.companiesCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.companiesCombo.DropDownHeight = 108;
            this.companiesCombo.FormattingEnabled = true;
            this.companiesCombo.IntegralHeight = false;
            this.companiesCombo.ItemHeight = 13;
            this.companiesCombo.Location = new System.Drawing.Point(12, 12);
            this.companiesCombo.Name = "companiesCombo";
            this.companiesCombo.Size = new System.Drawing.Size(137, 21);
            this.companiesCombo.TabIndex = 8;
            this.companiesCombo.SelectedIndexChanged += new System.EventHandler(this.companiesCombo_SelectedIndexChanged);
            // 
            // countriesCombo
            // 
            this.countriesCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.countriesCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.countriesCombo.DropDownHeight = 108;
            this.countriesCombo.FormattingEnabled = true;
            this.countriesCombo.IntegralHeight = false;
            this.countriesCombo.Location = new System.Drawing.Point(164, 12);
            this.countriesCombo.Name = "countriesCombo";
            this.countriesCombo.Size = new System.Drawing.Size(130, 21);
            this.countriesCombo.TabIndex = 9;
            this.countriesCombo.SelectedIndexChanged += new System.EventHandler(this.countriesCombo_SelectedIndexChanged);
            // 
            // autotextCombo
            // 
            this.autotextCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.autotextCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.autotextCombo.DropDownHeight = 108;
            this.autotextCombo.FormattingEnabled = true;
            this.autotextCombo.IntegralHeight = false;
            this.autotextCombo.Location = new System.Drawing.Point(318, 12);
            this.autotextCombo.Name = "autotextCombo";
            this.autotextCombo.Size = new System.Drawing.Size(155, 21);
            this.autotextCombo.TabIndex = 10;
            this.autotextCombo.SelectedIndexChanged += new System.EventHandler(this.autotextCombo_SelectedIndexChanged);
            // 
            // toggleButton
            // 
            this.toggleButton.Location = new System.Drawing.Point(534, 11);
            this.toggleButton.Name = "toggleButton";
            this.toggleButton.Size = new System.Drawing.Size(30, 21);
            this.toggleButton.TabIndex = 12;
            this.toggleButton.UseVisualStyleBackColor = true;
            this.toggleButton.Click += new System.EventHandler(this.toggleButton_Click);
            // 
            // buttonToolstrip
            // 
            this.buttonToolstrip.BackColor = System.Drawing.SystemColors.Control;
            this.buttonToolstrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonToolstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.buttonToolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripButton,
            this.toolStripSeparator2,
            this.editToolStripButton,
            this.toolStripSeparator3,
            this.newToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator1,
            this.reloadToolStripButton});
            this.buttonToolstrip.Location = new System.Drawing.Point(0, 224);
            this.buttonToolstrip.Name = "buttonToolstrip";
            this.buttonToolstrip.Size = new System.Drawing.Size(714, 25);
            this.buttonToolstrip.TabIndex = 14;
            // 
            // deleteToolStripButton
            // 
            this.deleteToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.deleteToolStripButton.AutoSize = false;
            this.deleteToolStripButton.AutoToolTip = false;
            this.deleteToolStripButton.BackColor = System.Drawing.SystemColors.Control;
            this.deleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.deleteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripButton.Image")));
            this.deleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteToolStripButton.Name = "deleteToolStripButton";
            this.deleteToolStripButton.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.deleteToolStripButton.Size = new System.Drawing.Size(50, 22);
            this.deleteToolStripButton.Text = "&Delete";
            this.deleteToolStripButton.Click += new System.EventHandler(this.deleteToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // editToolStripButton
            // 
            this.editToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.editToolStripButton.AutoSize = false;
            this.editToolStripButton.AutoToolTip = false;
            this.editToolStripButton.BackColor = System.Drawing.SystemColors.Control;
            this.editToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.editToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("editToolStripButton.Image")));
            this.editToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editToolStripButton.Name = "editToolStripButton";
            this.editToolStripButton.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.editToolStripButton.Size = new System.Drawing.Size(50, 22);
            this.editToolStripButton.Text = "&Edit";
            this.editToolStripButton.Click += new System.EventHandler(this.editToolStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.newToolStripButton.AutoSize = false;
            this.newToolStripButton.AutoToolTip = false;
            this.newToolStripButton.BackColor = System.Drawing.SystemColors.Control;
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.newToolStripButton.Size = new System.Drawing.Size(50, 22);
            this.newToolStripButton.Text = "&New";
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.AutoSize = false;
            this.saveToolStripButton.AutoToolTip = false;
            this.saveToolStripButton.BackColor = System.Drawing.SystemColors.Control;
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.saveToolStripButton.Size = new System.Drawing.Size(50, 22);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // reloadToolStripButton
            // 
            this.reloadToolStripButton.AutoToolTip = false;
            this.reloadToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.reloadToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("reloadToolStripButton.Image")));
            this.reloadToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reloadToolStripButton.Name = "reloadToolStripButton";
            this.reloadToolStripButton.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.reloadToolStripButton.Size = new System.Drawing.Size(47, 22);
            this.reloadToolStripButton.Text = "&Reload";
            this.reloadToolStripButton.Click += new System.EventHandler(this.reloadToolStripButton_Click);
            // 
            // pasteButton
            // 
            this.pasteButton.Image = global::AutoText.Properties.Resources.paste;
            this.pasteButton.Location = new System.Drawing.Point(498, 11);
            this.pasteButton.Name = "pasteButton";
            this.pasteButton.Size = new System.Drawing.Size(30, 21);
            this.pasteButton.TabIndex = 11;
            this.pasteButton.UseVisualStyleBackColor = true;
            this.pasteButton.Click += new System.EventHandler(this.pasteButton_click);
            // 
            // AutoTextForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 249);
            this.Controls.Add(this.buttonToolstrip);
            this.Controls.Add(this.toggleButton);
            this.Controls.Add(this.pasteButton);
            this.Controls.Add(this.autotextCombo);
            this.Controls.Add(this.countriesCombo);
            this.Controls.Add(this.companiesCombo);
            this.Controls.Add(this.autotextTextbox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(720, 38);
            this.Name = "AutoTextForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Steppes AutoText";
            this.Load += new System.EventHandler(this.AutoText_Load);
            this.ResizeEnd += new System.EventHandler(this.AutoTextForm_ResizeEnd);
            this.buttonToolstrip.ResumeLayout(false);
            this.buttonToolstrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox autotextTextbox;
        private System.Windows.Forms.Timer topmostDetectorTimer;
        private System.Windows.Forms.ComboBox companiesCombo;
        private System.Windows.Forms.ComboBox countriesCombo;
        private System.Windows.Forms.ComboBox autotextCombo;
        private System.Windows.Forms.Button pasteButton;
        private System.Windows.Forms.Button toggleButton;
        private System.Windows.Forms.ToolStrip buttonToolstrip;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton editToolStripButton;
        private System.Windows.Forms.ToolStripButton deleteToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton reloadToolStripButton;
    }
}

