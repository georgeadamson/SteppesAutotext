namespace AutoTextDataMigrator
{
    partial class AutoTextDataMigrator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoTextDataMigrator));
            this.convertRTFButton = new System.Windows.Forms.Button();
            this.migrateDataButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.oldDatabaseConnectionStringTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.steppes2ConnectionStringTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.progressListbox = new System.Windows.Forms.ListBox();
            this.conversionProgressBar = new System.Windows.Forms.ProgressBar();
            this.doNotMigrateMissingNameCheckbox = new System.Windows.Forms.CheckBox();
            this.doNotMigrateMissingTextCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // convertRTFButton
            // 
            this.convertRTFButton.Location = new System.Drawing.Point(292, 436);
            this.convertRTFButton.Name = "convertRTFButton";
            this.convertRTFButton.Size = new System.Drawing.Size(87, 23);
            this.convertRTFButton.TabIndex = 0;
            this.convertRTFButton.Text = "Convert RTF";
            this.convertRTFButton.UseVisualStyleBackColor = true;
            this.convertRTFButton.Click += new System.EventHandler(this.convertRTFButton_Click);
            // 
            // migrateDataButton
            // 
            this.migrateDataButton.Location = new System.Drawing.Point(385, 436);
            this.migrateDataButton.Name = "migrateDataButton";
            this.migrateDataButton.Size = new System.Drawing.Size(84, 23);
            this.migrateDataButton.TabIndex = 1;
            this.migrateDataButton.Text = "Migrate Data";
            this.migrateDataButton.UseVisualStyleBackColor = true;
            this.migrateDataButton.Click += new System.EventHandler(this.migrateDataButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(475, 436);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // oldDatabaseConnectionStringTextbox
            // 
            this.oldDatabaseConnectionStringTextbox.Location = new System.Drawing.Point(12, 25);
            this.oldDatabaseConnectionStringTextbox.Name = "oldDatabaseConnectionStringTextbox";
            this.oldDatabaseConnectionStringTextbox.Size = new System.Drawing.Size(538, 20);
            this.oldDatabaseConnectionStringTextbox.TabIndex = 3;
            this.oldDatabaseConnectionStringTextbox.Text = "Data Source=.;Initial Catalog=Steppes;User ID=SteppesCRM;Password=password;";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Old Database Connection String";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Steppes 2 Connection String";
            // 
            // steppes2ConnectionStringTextbox
            // 
            this.steppes2ConnectionStringTextbox.Location = new System.Drawing.Point(12, 73);
            this.steppes2ConnectionStringTextbox.Name = "steppes2ConnectionStringTextbox";
            this.steppes2ConnectionStringTextbox.Size = new System.Drawing.Size(538, 20);
            this.steppes2ConnectionStringTextbox.TabIndex = 5;
            this.steppes2ConnectionStringTextbox.Text = "Data Source=.;Initial Catalog=steppes2dev;User ID=SteppesCRM;Password=password;";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Progress";
            // 
            // progressListbox
            // 
            this.progressListbox.FormattingEnabled = true;
            this.progressListbox.Location = new System.Drawing.Point(12, 170);
            this.progressListbox.Name = "progressListbox";
            this.progressListbox.Size = new System.Drawing.Size(538, 251);
            this.progressListbox.TabIndex = 8;
            // 
            // conversionProgressBar
            // 
            this.conversionProgressBar.Location = new System.Drawing.Point(12, 141);
            this.conversionProgressBar.Name = "conversionProgressBar";
            this.conversionProgressBar.Size = new System.Drawing.Size(538, 23);
            this.conversionProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.conversionProgressBar.TabIndex = 9;
            // 
            // doNotMigrateMissingNameCheckbox
            // 
            this.doNotMigrateMissingNameCheckbox.AutoSize = true;
            this.doNotMigrateMissingNameCheckbox.Location = new System.Drawing.Point(15, 99);
            this.doNotMigrateMissingNameCheckbox.Name = "doNotMigrateMissingNameCheckbox";
            this.doNotMigrateMissingNameCheckbox.Size = new System.Drawing.Size(206, 17);
            this.doNotMigrateMissingNameCheckbox.TabIndex = 10;
            this.doNotMigrateMissingNameCheckbox.Text = "Do not migrate elements missing name";
            this.doNotMigrateMissingNameCheckbox.UseVisualStyleBackColor = true;
            // 
            // doNotMigrateMissingTextCheckbox
            // 
            this.doNotMigrateMissingTextCheckbox.AutoSize = true;
            this.doNotMigrateMissingTextCheckbox.Location = new System.Drawing.Point(260, 99);
            this.doNotMigrateMissingTextCheckbox.Name = "doNotMigrateMissingTextCheckbox";
            this.doNotMigrateMissingTextCheckbox.Size = new System.Drawing.Size(197, 17);
            this.doNotMigrateMissingTextCheckbox.TabIndex = 11;
            this.doNotMigrateMissingTextCheckbox.Text = "Do not migrate elements missing text";
            this.doNotMigrateMissingTextCheckbox.UseVisualStyleBackColor = true;
            // 
            // AutoTextDataMigrator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 472);
            this.Controls.Add(this.doNotMigrateMissingTextCheckbox);
            this.Controls.Add(this.doNotMigrateMissingNameCheckbox);
            this.Controls.Add(this.conversionProgressBar);
            this.Controls.Add(this.progressListbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.steppes2ConnectionStringTextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.oldDatabaseConnectionStringTextbox);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.migrateDataButton);
            this.Controls.Add(this.convertRTFButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AutoTextDataMigrator";
            this.Text = "Steppes AutoText Data Migrator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button convertRTFButton;
        private System.Windows.Forms.Button migrateDataButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.TextBox oldDatabaseConnectionStringTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox steppes2ConnectionStringTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox progressListbox;
        private System.Windows.Forms.ProgressBar conversionProgressBar;
        private System.Windows.Forms.CheckBox doNotMigrateMissingNameCheckbox;
        private System.Windows.Forms.CheckBox doNotMigrateMissingTextCheckbox;
    }
}

