namespace MPPCSharp.Forms
{
    partial class MainForm
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
            this.childTableDataGridView = new System.Windows.Forms.DataGridView();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.filterButton = new System.Windows.Forms.Button();
            this.resetFilterButton = new System.Windows.Forms.Button();
            this.addChildToCompetitionButton = new System.Windows.Forms.Button();
            this.trialsComboBox = new System.Windows.Forms.ComboBox();
            this.ageBracketsComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.childTableDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // childTableDataGridView
            // 
            this.childTableDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.childTableDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.childTableDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.childTableDataGridView.Location = new System.Drawing.Point(44, 63);
            this.childTableDataGridView.Name = "childTableDataGridView";
            this.childTableDataGridView.ReadOnly = true;
            this.childTableDataGridView.RowHeadersWidth = 62;
            this.childTableDataGridView.Size = new System.Drawing.Size(698, 129);
            this.childTableDataGridView.TabIndex = 0;
            // 
            // disconnectButton
            // 
            this.disconnectButton.Location = new System.Drawing.Point(585, 453);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(128, 50);
            this.disconnectButton.TabIndex = 2;
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.onDisconnectClick);
            // 
            // filterButton
            // 
            this.filterButton.Location = new System.Drawing.Point(585, 259);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(143, 45);
            this.filterButton.TabIndex = 3;
            this.filterButton.Text = "Filter";
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.Click += new System.EventHandler(this.onFilterButtonClick);
            // 
            // resetFilterButton
            // 
            this.resetFilterButton.Location = new System.Drawing.Point(585, 336);
            this.resetFilterButton.Name = "resetFilterButton";
            this.resetFilterButton.Size = new System.Drawing.Size(147, 44);
            this.resetFilterButton.TabIndex = 4;
            this.resetFilterButton.Text = "Reset filters";
            this.resetFilterButton.UseVisualStyleBackColor = true;
            this.resetFilterButton.Click += new System.EventHandler(this.onResetFilterButtonClick);
            // 
            // addChildToCompetitionButton
            // 
            this.addChildToCompetitionButton.Location = new System.Drawing.Point(73, 453);
            this.addChildToCompetitionButton.Name = "addChildToCompetitionButton";
            this.addChildToCompetitionButton.Size = new System.Drawing.Size(163, 50);
            this.addChildToCompetitionButton.TabIndex = 5;
            this.addChildToCompetitionButton.Text = "Inscriere copil";
            this.addChildToCompetitionButton.UseVisualStyleBackColor = true;
            this.addChildToCompetitionButton.Click += new System.EventHandler(this.onAddChildToCompetitionButtonClick);
            // 
            // trialsComboBox
            // 
            this.trialsComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.trialsComboBox.FormattingEnabled = true;
            this.trialsComboBox.Location = new System.Drawing.Point(35, 297);
            this.trialsComboBox.Name = "trialsComboBox";
            this.trialsComboBox.Size = new System.Drawing.Size(234, 20);
            this.trialsComboBox.TabIndex = 6;
            // 
            // ageBracketsComboBox
            // 
            this.ageBracketsComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.ageBracketsComboBox.FormattingEnabled = true;
            this.ageBracketsComboBox.Location = new System.Drawing.Point(303, 297);
            this.ageBracketsComboBox.Name = "ageBracketsComboBox";
            this.ageBracketsComboBox.Size = new System.Drawing.Size(234, 32);
            this.ageBracketsComboBox.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 520);
            this.Controls.Add(this.ageBracketsComboBox);
            this.Controls.Add(this.trialsComboBox);
            this.Controls.Add(this.addChildToCompetitionButton);
            this.Controls.Add(this.resetFilterButton);
            this.Controls.Add(this.filterButton);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.childTableDataGridView);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.childTableDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView childTableDataGridView;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.Button resetFilterButton;
        private System.Windows.Forms.Button addChildToCompetitionButton;
        private System.Windows.Forms.ComboBox trialsComboBox;
        private System.Windows.Forms.ComboBox ageBracketsComboBox;
    }
}