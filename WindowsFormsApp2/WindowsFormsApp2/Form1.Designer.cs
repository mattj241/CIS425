namespace WindowsFormsApp2
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
               ((System.ComponentModel.ISupportInitialize)(this.sql_Output)).BeginInit();
               ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
               this.SuspendLayout();
               // 
               // submitBtn
               // 
               this.submitBtn.Location = new System.Drawing.Point(558, 9);
               this.submitBtn.Name = "submitBtn";
               this.submitBtn.Size = new System.Drawing.Size(128, 52);
               this.submitBtn.TabIndex = 1;
               this.submitBtn.Text = "Submit";
               this.submitBtn.UseVisualStyleBackColor = true;
               this.submitBtn.Click += new System.EventHandler(this.submitBtn_Click);
               // 
               // sql_input
               // 
               this.sql_input.AcceptsReturn = true;
               this.sql_input.Location = new System.Drawing.Point(13, 9);
               this.sql_input.Name = "sql_input";
               this.sql_input.Size = new System.Drawing.Size(539, 22);
               this.sql_input.TabIndex = 2;
               this.sql_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sql_input_KeyDown);
               // 
               // sql_Output
               // 
               this.sql_Output.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
               this.sql_Output.Location = new System.Drawing.Point(12, 69);
               this.sql_Output.Name = "sql_Output";
               this.sql_Output.RowTemplate.Height = 24;
               this.sql_Output.Size = new System.Drawing.Size(665, 292);
               this.sql_Output.TabIndex = 3;
               // 
               // dataGridView1
               // 
               this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
               this.dataGridView1.Location = new System.Drawing.Point(701, 69);
               this.dataGridView1.Name = "dataGridView1";
               this.dataGridView1.RowTemplate.Height = 24;
               this.dataGridView1.Size = new System.Drawing.Size(665, 292);
               this.dataGridView1.TabIndex = 4;
               this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
               // 
               // carRental
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.ClientSize = new System.Drawing.Size(1422, 383);
               this.Controls.Add(this.dataGridView1);
               this.Controls.Add(this.sql_Output);
               this.Controls.Add(this.sql_input);
               this.Controls.Add(this.submitBtn);
               this.Name = "carRental";
               this.Text = "Car Rental";
               this.Load += new System.EventHandler(this.carRental_Load);
               ((System.ComponentModel.ISupportInitialize)(this.sql_Output)).EndInit();
               ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
               this.ResumeLayout(false);
               this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button submitBtn;
        private System.Windows.Forms.TextBox sql_input;
        private System.Windows.Forms.DataGridView sql_Output;
          private System.Windows.Forms.DataGridView dataGridView1;
     }
}

