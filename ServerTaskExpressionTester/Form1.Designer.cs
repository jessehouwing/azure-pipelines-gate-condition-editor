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
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.Statusbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
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
            this.responseEditor.Size = new System.Drawing.Size(747, 127);
            this.responseEditor.TabIndex = 0;
            this.responseEditor.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // expressionEditor
            // 
            this.expressionEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expressionEditor.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expressionEditor.Location = new System.Drawing.Point(0, 13);
            this.expressionEditor.Multiline = true;
            this.expressionEditor.Name = "expressionEditor";
            this.expressionEditor.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.expressionEditor.Size = new System.Drawing.Size(746, 189);
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
            this.dataGridView1.Location = new System.Drawing.Point(0, 331);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 102;
            this.dataGridView1.Size = new System.Drawing.Size(746, 102);
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
            this.Statusbar.Size = new System.Drawing.Size(1024, 22);
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
            this.Llog.Location = new System.Drawing.Point(747, 0);
            this.Llog.Margin = new System.Windows.Forms.Padding(1);
            this.Llog.Multiline = true;
            this.Llog.Name = "Llog";
            this.Llog.ReadOnly = true;
            this.Llog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Llog.Size = new System.Drawing.Size(277, 574);
            this.Llog.TabIndex = 5;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(746, 0);
            this.splitter1.Margin = new System.Windows.Forms.Padding(1);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1, 434);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(0, 330);
            this.splitter2.Margin = new System.Windows.Forms.Padding(1);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(746, 1);
            this.splitter2.TabIndex = 7;
            this.splitter2.TabStop = false;
            // 
            // splitter3
            // 
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter3.Location = new System.Drawing.Point(0, 433);
            this.splitter3.Margin = new System.Windows.Forms.Padding(1);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(746, 1);
            this.splitter3.TabIndex = 8;
            this.splitter3.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 317);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Variables";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView2.Location = new System.Drawing.Point(0, 215);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 102;
            this.dataGridView2.Size = new System.Drawing.Size(746, 102);
            this.dataGridView2.TabIndex = 10;
            this.dataGridView2.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Key";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 12;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 250;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Value";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 12;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 250;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(0, 202);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Task Inputs";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Success Expression";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(0, 434);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "JSON Response";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 596);
            this.Controls.Add(this.expressionEditor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.splitter3);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.responseEditor);
            this.Controls.Add(this.Llog);
            this.Controls.Add(this.Statusbar);
            this.Name = "Form1";
            this.Text = "Gate Condition Editor for Azure Pipelines";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.Statusbar.ResumeLayout(false);
            this.Statusbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

