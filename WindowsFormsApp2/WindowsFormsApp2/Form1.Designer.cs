﻿namespace WindowsFormsApp2
{
    partial class carRental
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
            this.submitBtn = new System.Windows.Forms.Button();
            this.sql_input = new System.Windows.Forms.TextBox();
            this.sql_Output = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.MergedView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.sql_Output)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MergedView)).BeginInit();
            this.SuspendLayout();
            // 
            // submitBtn
            // 
            this.submitBtn.Location = new System.Drawing.Point(418, 7);
            this.submitBtn.Margin = new System.Windows.Forms.Padding(2);
            this.submitBtn.Name = "submitBtn";
            this.submitBtn.Size = new System.Drawing.Size(96, 42);
            this.submitBtn.TabIndex = 1;
            this.submitBtn.Text = "Submit";
            this.submitBtn.UseVisualStyleBackColor = true;
            this.submitBtn.Click += new System.EventHandler(this.SubmitBtn_Click);
            // 
            // sql_input
            // 
            this.sql_input.AcceptsReturn = true;
            this.sql_input.Location = new System.Drawing.Point(10, 7);
            this.sql_input.Margin = new System.Windows.Forms.Padding(2);
            this.sql_input.Name = "sql_input";
            this.sql_input.Size = new System.Drawing.Size(405, 20);
            this.sql_input.TabIndex = 2;
            this.sql_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Sql_input_KeyDown);
            // 
            // sql_Output
            // 
            this.sql_Output.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sql_Output.Location = new System.Drawing.Point(9, 56);
            this.sql_Output.Margin = new System.Windows.Forms.Padding(2);
            this.sql_Output.Name = "sql_Output";
            this.sql_Output.RowTemplate.Height = 24;
            this.sql_Output.Size = new System.Drawing.Size(499, 237);
            this.sql_Output.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(526, 56);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(499, 237);
            this.dataGridView1.TabIndex = 4;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(29, 318);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(386, 200);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // MergedView
            // 
            this.MergedView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MergedView.Location = new System.Drawing.Point(526, 318);
            this.MergedView.Name = "MergedView";
            this.MergedView.Size = new System.Drawing.Size(499, 200);
            this.MergedView.TabIndex = 6;
            // 
            // carRental
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 530);
            this.Controls.Add(this.MergedView);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.sql_Output);
            this.Controls.Add(this.sql_input);
            this.Controls.Add(this.submitBtn);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "carRental";
            this.Text = "Car Rental";
            ((System.ComponentModel.ISupportInitialize)(this.sql_Output)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MergedView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button submitBtn;
        private System.Windows.Forms.TextBox sql_input;
        private System.Windows.Forms.DataGridView sql_Output;
          private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.DataGridView MergedView;
    }
}

