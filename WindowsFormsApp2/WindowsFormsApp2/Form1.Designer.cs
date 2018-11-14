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
            this.MergedView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MergedView)).BeginInit();
            this.SuspendLayout();
            // 
            // submitBtn
            // 
            this.submitBtn.Location = new System.Drawing.Point(421, 29);
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
            this.sql_input.Location = new System.Drawing.Point(12, 41);
            this.sql_input.Margin = new System.Windows.Forms.Padding(2);
            this.sql_input.Name = "sql_input";
            this.sql_input.Size = new System.Drawing.Size(405, 20);
            this.sql_input.TabIndex = 2;
            this.sql_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Sql_input_KeyDown);
            // 
            // MergedView
            // 
            this.MergedView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MergedView.Location = new System.Drawing.Point(12, 76);
            this.MergedView.Name = "MergedView";
            this.MergedView.Size = new System.Drawing.Size(935, 248);
            this.MergedView.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 22);
            this.label1.TabIndex = 7;
            this.label1.Text = "Query Box:";
            // 
            // carRental
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 336);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MergedView);
            this.Controls.Add(this.sql_input);
            this.Controls.Add(this.submitBtn);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "carRental";
            this.Text = "Car Rental";
            ((System.ComponentModel.ISupportInitialize)(this.MergedView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button submitBtn;
        private System.Windows.Forms.TextBox sql_input;
        private System.Windows.Forms.DataGridView MergedView;
        private System.Windows.Forms.Label label1;
    }
}

