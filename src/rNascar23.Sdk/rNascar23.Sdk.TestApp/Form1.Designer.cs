namespace rNascar23.Sdk.TestApp
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnAudio = new System.Windows.Forms.Button();
            this.btnDrivers = new System.Windows.Forms.Button();
            this.btnCup = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.grpSchedules = new System.Windows.Forms.GroupBox();
            this.btnThisWeek = new System.Windows.Forms.Button();
            this.btnToday = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.grpSchedules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Api Sources";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnApiSources_Click);
            // 
            // btnAudio
            // 
            this.btnAudio.Location = new System.Drawing.Point(94, 13);
            this.btnAudio.Name = "btnAudio";
            this.btnAudio.Size = new System.Drawing.Size(75, 23);
            this.btnAudio.TabIndex = 1;
            this.btnAudio.Text = "Audio/Video";
            this.btnAudio.UseVisualStyleBackColor = true;
            this.btnAudio.Click += new System.EventHandler(this.btnAudio_Click);
            // 
            // btnDrivers
            // 
            this.btnDrivers.Location = new System.Drawing.Point(175, 12);
            this.btnDrivers.Name = "btnDrivers";
            this.btnDrivers.Size = new System.Drawing.Size(75, 23);
            this.btnDrivers.TabIndex = 2;
            this.btnDrivers.Text = "Drivers";
            this.btnDrivers.UseVisualStyleBackColor = true;
            this.btnDrivers.Click += new System.EventHandler(this.btnDrivers_Click);
            // 
            // btnCup
            // 
            this.btnCup.Location = new System.Drawing.Point(87, 19);
            this.btnCup.Name = "btnCup";
            this.btnCup.Size = new System.Drawing.Size(75, 23);
            this.btnCup.TabIndex = 3;
            this.btnCup.Text = "Cup";
            this.btnCup.UseVisualStyleBackColor = true;
            this.btnCup.Click += new System.EventHandler(this.btnCup_Click);
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(6, 19);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(75, 23);
            this.btnAll.TabIndex = 4;
            this.btnAll.Text = "All";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // grpSchedules
            // 
            this.grpSchedules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSchedules.Controls.Add(this.dataGridView1);
            this.grpSchedules.Controls.Add(this.btnToday);
            this.grpSchedules.Controls.Add(this.btnThisWeek);
            this.grpSchedules.Controls.Add(this.btnCup);
            this.grpSchedules.Controls.Add(this.btnAll);
            this.grpSchedules.Location = new System.Drawing.Point(13, 42);
            this.grpSchedules.Name = "grpSchedules";
            this.grpSchedules.Size = new System.Drawing.Size(532, 200);
            this.grpSchedules.TabIndex = 5;
            this.grpSchedules.TabStop = false;
            this.grpSchedules.Text = "Schedules";
            // 
            // btnThisWeek
            // 
            this.btnThisWeek.Location = new System.Drawing.Point(168, 19);
            this.btnThisWeek.Name = "btnThisWeek";
            this.btnThisWeek.Size = new System.Drawing.Size(75, 23);
            this.btnThisWeek.TabIndex = 5;
            this.btnThisWeek.Text = "This Week";
            this.btnThisWeek.UseVisualStyleBackColor = true;
            this.btnThisWeek.Click += new System.EventHandler(this.btnThisWeek_Click);
            // 
            // btnToday
            // 
            this.btnToday.Location = new System.Drawing.Point(249, 19);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(75, 23);
            this.btnToday.TabIndex = 6;
            this.btnToday.Text = "Today";
            this.btnToday.UseVisualStyleBackColor = true;
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(7, 49);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(519, 145);
            this.dataGridView1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 310);
            this.Controls.Add(this.grpSchedules);
            this.Controls.Add(this.btnDrivers);
            this.Controls.Add(this.btnAudio);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.grpSchedules.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnAudio;
        private System.Windows.Forms.Button btnDrivers;
        private System.Windows.Forms.Button btnCup;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.GroupBox grpSchedules;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.Button btnThisWeek;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

