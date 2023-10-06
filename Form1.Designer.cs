namespace aiCorporation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tbLog = new System.Windows.Forms.TextBox();
            this.tbClear = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbRandomiseRecordOrder = new System.Windows.Forms.CheckBox();
            this.tbBulkTestRounds = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbRunBulkTest = new System.Windows.Forms.Button();
            this.tbMaxBankAccounts = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbMinBankAccounts = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbMaxClients = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMinClients = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMaxSalesAgents = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbMinSalesAgents = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btTest = new System.Windows.Forms.Button();
            this.btBrowse = new System.Windows.Forms.Button();
            this.tbCSVFilename = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbLog
            // 
            this.tbLog.BackColor = System.Drawing.SystemColors.Window;
            this.tbLog.Location = new System.Drawing.Point(12, 12);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbLog.Size = new System.Drawing.Size(776, 367);
            this.tbLog.TabIndex = 0;
            this.tbLog.WordWrap = false;
            // 
            // tbClear
            // 
            this.tbClear.Location = new System.Drawing.Point(713, 385);
            this.tbClear.Name = "tbClear";
            this.tbClear.Size = new System.Drawing.Size(75, 23);
            this.tbClear.TabIndex = 1;
            this.tbClear.Text = "Clear";
            this.tbClear.UseVisualStyleBackColor = true;
            this.tbClear.Click += new System.EventHandler(this.tbClear_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbRandomiseRecordOrder);
            this.groupBox1.Controls.Add(this.tbBulkTestRounds);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tbRunBulkTest);
            this.groupBox1.Controls.Add(this.tbMaxBankAccounts);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbMinBankAccounts);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbMaxClients);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbMinClients);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbMaxSalesAgents);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbMinSalesAgents);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 478);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 84);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bulk Test";
            // 
            // cbRandomiseRecordOrder
            // 
            this.cbRandomiseRecordOrder.AutoSize = true;
            this.cbRandomiseRecordOrder.Location = new System.Drawing.Point(525, 52);
            this.cbRandomiseRecordOrder.Name = "cbRandomiseRecordOrder";
            this.cbRandomiseRecordOrder.Size = new System.Drawing.Size(146, 17);
            this.cbRandomiseRecordOrder.TabIndex = 32;
            this.cbRandomiseRecordOrder.Text = "Randomise Record Order";
            this.cbRandomiseRecordOrder.UseVisualStyleBackColor = true;
            // 
            // tbBulkTestRounds
            // 
            this.tbBulkTestRounds.Location = new System.Drawing.Point(629, 23);
            this.tbBulkTestRounds.Name = "tbBulkTestRounds";
            this.tbBulkTestRounds.Size = new System.Drawing.Size(50, 20);
            this.tbBulkTestRounds.TabIndex = 31;
            this.tbBulkTestRounds.Text = "10";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(522, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Bulk Test Rounds";
            // 
            // tbRunBulkTest
            // 
            this.tbRunBulkTest.Location = new System.Drawing.Point(691, 23);
            this.tbRunBulkTest.Name = "tbRunBulkTest";
            this.tbRunBulkTest.Size = new System.Drawing.Size(75, 46);
            this.tbRunBulkTest.TabIndex = 29;
            this.tbRunBulkTest.Text = "Run Bulk Test";
            this.tbRunBulkTest.UseVisualStyleBackColor = true;
            this.tbRunBulkTest.Click += new System.EventHandler(this.tbRunBulkTest_Click);
            // 
            // tbMaxBankAccounts
            // 
            this.tbMaxBankAccounts.Location = new System.Drawing.Point(444, 49);
            this.tbMaxBankAccounts.Name = "tbMaxBankAccounts";
            this.tbMaxBankAccounts.Size = new System.Drawing.Size(50, 20);
            this.tbMaxBankAccounts.TabIndex = 28;
            this.tbMaxBankAccounts.Text = "5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(337, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Max Bank Account";
            // 
            // tbMinBankAccounts
            // 
            this.tbMinBankAccounts.Location = new System.Drawing.Point(444, 23);
            this.tbMinBankAccounts.Name = "tbMinBankAccounts";
            this.tbMinBankAccounts.Size = new System.Drawing.Size(50, 20);
            this.tbMinBankAccounts.TabIndex = 26;
            this.tbMinBankAccounts.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(337, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Min Bank Accounts";
            // 
            // tbMaxClients
            // 
            this.tbMaxClients.Location = new System.Drawing.Point(248, 49);
            this.tbMaxClients.Name = "tbMaxClients";
            this.tbMaxClients.Size = new System.Drawing.Size(50, 20);
            this.tbMaxClients.TabIndex = 24;
            this.tbMaxClients.Text = "100";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(181, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Max Clients";
            // 
            // tbMinClients
            // 
            this.tbMinClients.Location = new System.Drawing.Point(248, 23);
            this.tbMinClients.Name = "tbMinClients";
            this.tbMinClients.Size = new System.Drawing.Size(50, 20);
            this.tbMinClients.TabIndex = 22;
            this.tbMinClients.Text = "25";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(181, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Min Clients";
            // 
            // tbMaxSalesAgents
            // 
            this.tbMaxSalesAgents.Location = new System.Drawing.Point(106, 49);
            this.tbMaxSalesAgents.Name = "tbMaxSalesAgents";
            this.tbMaxSalesAgents.Size = new System.Drawing.Size(50, 20);
            this.tbMaxSalesAgents.TabIndex = 20;
            this.tbMaxSalesAgents.Text = "1000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Max Sales Agents";
            // 
            // tbMinSalesAgents
            // 
            this.tbMinSalesAgents.Location = new System.Drawing.Point(106, 23);
            this.tbMinSalesAgents.Name = "tbMinSalesAgents";
            this.tbMinSalesAgents.Size = new System.Drawing.Size(50, 20);
            this.tbMinSalesAgents.TabIndex = 18;
            this.tbMinSalesAgents.Text = "250";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Min Sales Agents";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btTest);
            this.groupBox2.Controls.Add(this.btBrowse);
            this.groupBox2.Controls.Add(this.tbCSVFilename);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(12, 414);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(776, 58);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Test Single File";
            // 
            // btTest
            // 
            this.btTest.Location = new System.Drawing.Point(691, 21);
            this.btTest.Name = "btTest";
            this.btTest.Size = new System.Drawing.Size(75, 23);
            this.btTest.TabIndex = 22;
            this.btTest.Text = "Test";
            this.btTest.UseVisualStyleBackColor = true;
            this.btTest.Click += new System.EventHandler(this.btTest_Click);
            // 
            // btBrowse
            // 
            this.btBrowse.Location = new System.Drawing.Point(500, 21);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(27, 23);
            this.btBrowse.TabIndex = 21;
            this.btBrowse.Text = "...";
            this.btBrowse.UseVisualStyleBackColor = true;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // tbCSVFilename
            // 
            this.tbCSVFilename.Location = new System.Drawing.Point(106, 23);
            this.tbCSVFilename.Name = "tbCSVFilename";
            this.tbCSVFilename.Size = new System.Drawing.Size(388, 20);
            this.tbCSVFilename.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "CSV File";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 568);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbClear);
            this.Controls.Add(this.tbLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Flat Data Reading";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Button tbClear;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbBulkTestRounds;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button tbRunBulkTest;
        private System.Windows.Forms.TextBox tbMaxBankAccounts;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbMinBankAccounts;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbMaxClients;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbMinClients;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbMaxSalesAgents;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbMinSalesAgents;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btTest;
        private System.Windows.Forms.Button btBrowse;
        private System.Windows.Forms.TextBox tbCSVFilename;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbRandomiseRecordOrder;
    }
}

