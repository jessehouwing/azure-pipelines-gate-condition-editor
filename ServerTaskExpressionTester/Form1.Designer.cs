namespace ServerTaskExpressionTester
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
            this.responseEditor = new System.Windows.Forms.TextBox();
            this.expressionEditor = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Statusbar = new System.Windows.Forms.StatusStrip();
            this.Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.Llog = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.splitter3 = new System.Windows.Forms.Splitter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.Statusbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // responseEditor
            // 
            this.responseEditor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.responseEditor.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.responseEditor.Location = new System.Drawing.Point(0, 447);
            this.responseEditor.Multiline = true;
            this.responseEditor.Name = "responseEditor";
            this.responseEditor.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.responseEditor.Size = new System.Drawing.Size(1189, 127);
            this.responseEditor.TabIndex = 0;
            this.responseEditor.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // expressionEditor
            // 
            this.expressionEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expressionEditor.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expressionEditor.Location = new System.Drawing.Point(0, 0);
            this.expressionEditor.Multiline = true;
            this.expressionEditor.Name = "expressionEditor";
            this.expressionEditor.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.expressionEditor.Size = new System.Drawing.Size(1189, 343);
            this.expressionEditor.TabIndex = 2;
            this.expressionEditor.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Key,
            this.Value});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 344);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 102;
            this.dataGridView1.Size = new System.Drawing.Size(1189, 102);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // Key
            // 
            this.Key.HeaderText = "Key";
            this.Key.MinimumWidth = 12;
            this.Key.Name = "Key";
            this.Key.ReadOnly = true;
            this.Key.Width = 250;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.MinimumWidth = 12;
            this.Value.Name = "Value";
            this.Value.Width = 250;
            // 
            // Statusbar
            // 
            this.Statusbar.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.Statusbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status});
            this.Statusbar.Location = new System.Drawing.Point(0, 574);
            this.Statusbar.Name = "Statusbar";
            this.Statusbar.Padding = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.Statusbar.Size = new System.Drawing.Size(1189, 22);
            this.Statusbar.TabIndex = 4;
            this.Statusbar.Text = "dfghdfgh";
            // 
            // Status
            // 
            this.Status.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Status.DoubleClickEnabled = true;
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(59, 17);
            this.Status.Text = "Loading...";
            this.Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Status.DoubleClick += new System.EventHandler(this.LLog_DoubleClick);
            // 
            // Llog
            // 
            this.Llog.Dock = System.Windows.Forms.DockStyle.Right;
            this.Llog.Location = new System.Drawing.Point(1190, 0);
            this.Llog.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.Llog.Multiline = true;
            this.Llog.Name = "Llog";
            this.Llog.ReadOnly = true;
            this.Llog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Llog.Size = new System.Drawing.Size(277, 596);
            this.Llog.TabIndex = 5;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(1189, 0);
            this.splitter1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1, 596);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(0, 343);
            this.splitter2.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(1189, 1);
            this.splitter2.TabIndex = 7;
            this.splitter2.TabStop = false;
            // 
            // splitter3
            // 
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter3.Location = new System.Drawing.Point(0, 446);
            this.splitter3.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(1189, 1);
            this.splitter3.TabIndex = 8;
            this.splitter3.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1467, 596);
            this.Controls.Add(this.expressionEditor);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.splitter3);
            this.Controls.Add(this.responseEditor);
            this.Controls.Add(this.Statusbar);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.Llog);
            this.Name = "Form1";
            this.Text = "Build & Release Expression Tester";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.Statusbar.ResumeLayout(false);
            this.Statusbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox responseEditor;
        private System.Windows.Forms.TextBox expressionEditor;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Key;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.StatusStrip Statusbar;
        private System.Windows.Forms.ToolStripStatusLabel Status;
        private System.Windows.Forms.TextBox Llog;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Splitter splitter3;
    }
}

