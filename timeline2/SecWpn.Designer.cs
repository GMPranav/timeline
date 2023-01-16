namespace timeline2
{
    partial class SecWpn
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.btn_edit = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.label_atk_pow = new System.Windows.Forms.Label();
            this.label_blk_end = new System.Windows.Forms.Label();
            this.label_atk_spd = new System.Windows.Forms.Label();
            this.label_wpn_durabity = new System.Windows.Forms.Label();
            this.label_wpn_special = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(19, 72);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(146, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(19, 99);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(146, 21);
            this.comboBox2.TabIndex = 1;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // btn_edit
            // 
            this.btn_edit.Location = new System.Drawing.Point(12, 257);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(75, 23);
            this.btn_edit.TabIndex = 2;
            this.btn_edit.Text = "Edit";
            this.btn_edit.UseVisualStyleBackColor = true;
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(93, 257);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 3;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // label_atk_pow
            // 
            this.label_atk_pow.AutoSize = true;
            this.label_atk_pow.Location = new System.Drawing.Point(16, 135);
            this.label_atk_pow.Name = "label_atk_pow";
            this.label_atk_pow.Size = new System.Drawing.Size(35, 13);
            this.label_atk_pow.TabIndex = 4;
            this.label_atk_pow.Text = "label1";
            // 
            // label_blk_end
            // 
            this.label_blk_end.AutoSize = true;
            this.label_blk_end.Location = new System.Drawing.Point(16, 157);
            this.label_blk_end.Name = "label_blk_end";
            this.label_blk_end.Size = new System.Drawing.Size(35, 13);
            this.label_blk_end.TabIndex = 5;
            this.label_blk_end.Text = "label2";
            // 
            // label_atk_spd
            // 
            this.label_atk_spd.AutoSize = true;
            this.label_atk_spd.Location = new System.Drawing.Point(16, 179);
            this.label_atk_spd.Name = "label_atk_spd";
            this.label_atk_spd.Size = new System.Drawing.Size(35, 13);
            this.label_atk_spd.TabIndex = 6;
            this.label_atk_spd.Text = "label3";
            // 
            // label_wpn_durabity
            // 
            this.label_wpn_durabity.AutoSize = true;
            this.label_wpn_durabity.Location = new System.Drawing.Point(16, 202);
            this.label_wpn_durabity.Name = "label_wpn_durabity";
            this.label_wpn_durabity.Size = new System.Drawing.Size(35, 13);
            this.label_wpn_durabity.TabIndex = 7;
            this.label_wpn_durabity.Text = "label4";
            // 
            // label_wpn_special
            // 
            this.label_wpn_special.AutoSize = true;
            this.label_wpn_special.Location = new System.Drawing.Point(16, 225);
            this.label_wpn_special.Name = "label_wpn_special";
            this.label_wpn_special.Size = new System.Drawing.Size(35, 13);
            this.label_wpn_special.TabIndex = 8;
            this.label_wpn_special.Text = "label5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(16, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Secondary Weapon";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Select:";
            // 
            // SecWpn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 292);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_wpn_special);
            this.Controls.Add(this.label_wpn_durabity);
            this.Controls.Add(this.label_atk_spd);
            this.Controls.Add(this.label_blk_end);
            this.Controls.Add(this.label_atk_pow);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_edit);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SecWpn";
            this.Text = "Edit...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Label label_atk_pow;
        private System.Windows.Forms.Label label_blk_end;
        private System.Windows.Forms.Label label_atk_spd;
        private System.Windows.Forms.Label label_wpn_durabity;
        private System.Windows.Forms.Label label_wpn_special;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}