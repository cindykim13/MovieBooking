namespace MovieBookingClient.UI.UserControls.Admin
{
    partial class UC_Admin_Rooms
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            btnDelete = new Guna.UI2.WinForms.Guna2Button();
            btnAdd = new Guna.UI2.WinForms.Guna2Button();
            cboCinemaFilter = new Guna.UI2.WinForms.Guna2ComboBox();
            lblFilter = new Label();
            dgvRooms = new Guna.UI2.WinForms.Guna2DataGridView();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRooms).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(btnDelete);
            pnlHeader.Controls.Add(btnAdd);
            pnlHeader.Controls.Add(cboCinemaFilter);
            pnlHeader.Controls.Add(lblFilter);
            pnlHeader.CustomizableEdges = customizableEdges7;
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(10, 10);
            pnlHeader.Margin = new Padding(5, 6, 5, 6);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.ShadowDecoration.CustomizableEdges = customizableEdges8;
            pnlHeader.Size = new Size(940, 60);
            pnlHeader.TabIndex = 0;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDelete.BorderRadius = 5;
            btnDelete.CustomizableEdges = customizableEdges1;
            btnDelete.DisabledState.BorderColor = Color.DarkGray;
            btnDelete.DisabledState.CustomBorderColor = Color.DarkGray;
            btnDelete.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnDelete.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnDelete.FillColor = Color.FromArgb(231, 76, 60);
            btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(830, 12);
            btnDelete.Margin = new Padding(5, 6, 5, 6);
            btnDelete.Name = "btnDelete";
            btnDelete.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnDelete.Size = new Size(100, 36);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "XÓA";
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAdd.BorderRadius = 5;
            btnAdd.CustomizableEdges = customizableEdges3;
            btnAdd.DisabledState.BorderColor = Color.DarkGray;
            btnAdd.DisabledState.CustomBorderColor = Color.DarkGray;
            btnAdd.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnAdd.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnAdd.FillColor = Color.FromArgb(46, 204, 113);
            btnAdd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(720, 12);
            btnAdd.Margin = new Padding(5, 6, 5, 6);
            btnAdd.Name = "btnAdd";
            btnAdd.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnAdd.Size = new Size(100, 36);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "THÊM";
            // 
            // cboCinemaFilter
            // 
            cboCinemaFilter.BackColor = Color.Transparent;
            cboCinemaFilter.BorderRadius = 5;
            cboCinemaFilter.CustomizableEdges = customizableEdges5;
            cboCinemaFilter.DrawMode = DrawMode.OwnerDrawFixed;
            cboCinemaFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCinemaFilter.FocusedColor = Color.FromArgb(94, 148, 255);
            cboCinemaFilter.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cboCinemaFilter.Font = new Font("Segoe UI", 10F);
            cboCinemaFilter.ForeColor = Color.FromArgb(68, 88, 112);
            cboCinemaFilter.ItemHeight = 30;
            cboCinemaFilter.Location = new Point(160, 12);
            cboCinemaFilter.Margin = new Padding(5, 6, 5, 6);
            cboCinemaFilter.Name = "cboCinemaFilter";
            cboCinemaFilter.ShadowDecoration.CustomizableEdges = customizableEdges6;
            cboCinemaFilter.Size = new Size(250, 36);
            cboCinemaFilter.TabIndex = 1;
            // 
            // lblFilter
            // 
            lblFilter.AutoSize = true;
            lblFilter.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFilter.Location = new Point(15, 20);
            lblFilter.Margin = new Padding(5, 0, 5, 0);
            lblFilter.Name = "lblFilter";
            lblFilter.Size = new Size(135, 28);
            lblFilter.TabIndex = 0;
            lblFilter.Text = "Lọc theo rạp:";
            // 
            // dgvRooms
            // 
            dgvRooms.AllowUserToAddRows = false;
            dgvRooms.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.White;
            dgvRooms.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvRooms.BackgroundColor = SystemColors.Info;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvRooms.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvRooms.ColumnHeadersHeight = 40;
            dgvRooms.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvRooms.DefaultCellStyle = dataGridViewCellStyle3;
            dgvRooms.Dock = DockStyle.Fill;
            dgvRooms.GridColor = Color.FromArgb(231, 229, 255);
            dgvRooms.Location = new Point(10, 70);
            dgvRooms.Margin = new Padding(5, 6, 5, 6);
            dgvRooms.Name = "dgvRooms";
            dgvRooms.ReadOnly = true;
            dgvRooms.RowHeadersVisible = false;
            dgvRooms.RowHeadersWidth = 62;
            dgvRooms.RowTemplate.Height = 35;
            dgvRooms.Size = new Size(940, 610);
            dgvRooms.TabIndex = 1;
            dgvRooms.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgvRooms.ThemeStyle.AlternatingRowsStyle.Font = null;
            dgvRooms.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dgvRooms.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dgvRooms.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dgvRooms.ThemeStyle.BackColor = SystemColors.Info;
            dgvRooms.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dgvRooms.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dgvRooms.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvRooms.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            dgvRooms.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dgvRooms.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvRooms.ThemeStyle.HeaderStyle.Height = 40;
            dgvRooms.ThemeStyle.ReadOnly = true;
            dgvRooms.ThemeStyle.RowsStyle.BackColor = Color.White;
            dgvRooms.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvRooms.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            dgvRooms.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dgvRooms.ThemeStyle.RowsStyle.Height = 35;
            dgvRooms.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dgvRooms.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // UC_Admin_Rooms
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            Controls.Add(dgvRooms);
            Controls.Add(pnlHeader);
            Margin = new Padding(5, 6, 5, 6);
            Name = "UC_Admin_Rooms";
            Padding = new Padding(10);
            Size = new Size(960, 690);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRooms).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private Guna.UI2.WinForms.Guna2ComboBox cboCinemaFilter;
        private System.Windows.Forms.Label lblFilter;
        private Guna.UI2.WinForms.Guna2Button btnAdd;
        private Guna.UI2.WinForms.Guna2Button btnDelete;
        private Guna.UI2.WinForms.Guna2DataGridView dgvRooms;
    }
}