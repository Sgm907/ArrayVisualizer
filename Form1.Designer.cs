﻿
namespace ArrayVisualizer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.SortButton = new System.Windows.Forms.Button();
            this.AlgorithmSelect = new System.Windows.Forms.ComboBox();
            this.ScrambleButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 17;
            this.timer.Tick += new System.EventHandler(this.Update);
            // 
            // SortButton
            // 
            this.SortButton.Location = new System.Drawing.Point(851, 36);
            this.SortButton.Name = "SortButton";
            this.SortButton.Size = new System.Drawing.Size(75, 23);
            this.SortButton.TabIndex = 0;
            this.SortButton.Text = "Sort";
            this.SortButton.UseVisualStyleBackColor = true;
            this.SortButton.Click += new System.EventHandler(this.SortButton_Click);
            // 
            // AlgorithmSelect
            // 
            this.AlgorithmSelect.DisplayMember = "2";
            this.AlgorithmSelect.FormattingEnabled = true;
            this.AlgorithmSelect.Items.AddRange(new object[] {
            "Algo1",
            "Algo2",
            "Algo3",
            "Algo4",
            "Algo5"});
            this.AlgorithmSelect.Location = new System.Drawing.Point(698, 35);
            this.AlgorithmSelect.Name = "AlgorithmSelect";
            this.AlgorithmSelect.Size = new System.Drawing.Size(121, 23);
            this.AlgorithmSelect.TabIndex = 1;
            this.AlgorithmSelect.Text = "Algo1";
            this.AlgorithmSelect.SelectedIndexChanged += new System.EventHandler(this.AlgorithmSelect_SelectedIndexChanged);
            // 
            // ScrambleButton
            // 
            this.ScrambleButton.Location = new System.Drawing.Point(960, 35);
            this.ScrambleButton.Name = "ScrambleButton";
            this.ScrambleButton.Size = new System.Drawing.Size(75, 23);
            this.ScrambleButton.TabIndex = 2;
            this.ScrambleButton.Text = "Scramble";
            this.ScrambleButton.UseVisualStyleBackColor = true;
            this.ScrambleButton.Click += new System.EventHandler(this.ScrambleButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1075, 608);
            this.Controls.Add(this.ScrambleButton);
            this.Controls.Add(this.AlgorithmSelect);
            this.Controls.Add(this.SortButton);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SortButton;
        private System.Windows.Forms.ComboBox AlgorithmSelect;
        private System.Windows.Forms.Button ScrambleButton;
        private System.Windows.Forms.Timer timer;
    }
}
