
namespace COVENTAF.PuntoVenta
{
    partial class frmPuntoVenta
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cboTipoFiltro = new System.Windows.Forms.ComboBox();
            this.dgvPuntoVenta = new System.Windows.Forms.DataGridView();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.barraTitulo = new System.Windows.Forms.Panel();
            this.btnMinizar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.btnNuevaFactura = new System.Windows.Forms.Button();
            this.pnRight = new System.Windows.Forms.Panel();
            this.pnlButtom = new System.Windows.Forms.Panel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblNoCierre = new System.Windows.Forms.Label();
            this.lblCajaApertura = new System.Windows.Forms.Label();
            this.pictureBox26 = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnNuevaFactura1 = new FontAwesome.Sharp.IconButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnAperturaCaja = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.btnCierreCaja = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPuntoVenta)).BeginInit();
            this.barraTitulo.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboTipoFiltro
            // 
            this.cboTipoFiltro.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboTipoFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoFiltro.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboTipoFiltro.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cboTipoFiltro.ForeColor = System.Drawing.Color.Maroon;
            this.cboTipoFiltro.FormattingEnabled = true;
            this.cboTipoFiltro.Items.AddRange(new object[] {
            "Factura del dia",
            "Recuperar Factura",
            "No Factura",
            "Devolucion",
            "Rango de Fecha"});
            this.cboTipoFiltro.Location = new System.Drawing.Point(180, 19);
            this.cboTipoFiltro.Name = "cboTipoFiltro";
            this.cboTipoFiltro.Size = new System.Drawing.Size(174, 26);
            this.cboTipoFiltro.TabIndex = 26;
            this.cboTipoFiltro.SelectedIndexChanged += new System.EventHandler(this.cboTipoFiltro_SelectedIndexChanged);
            // 
            // dgvPuntoVenta
            // 
            this.dgvPuntoVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPuntoVenta.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPuntoVenta.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPuntoVenta.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPuntoVenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPuntoVenta.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPuntoVenta.Location = new System.Drawing.Point(11, 173);
            this.dgvPuntoVenta.Name = "dgvPuntoVenta";
            this.dgvPuntoVenta.RowTemplate.Height = 25;
            this.dgvPuntoVenta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPuntoVenta.Size = new System.Drawing.Size(1130, 301);
            this.dgvPuntoVenta.TabIndex = 25;
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtBusqueda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBusqueda.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtBusqueda.ForeColor = System.Drawing.Color.Maroon;
            this.txtBusqueda.Location = new System.Drawing.Point(377, 19);
            this.txtBusqueda.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(375, 26);
            this.txtBusqueda.TabIndex = 24;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Olive;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200)))));
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(778, 17);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(141, 30);
            this.btnBuscar.TabIndex = 22;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // barraTitulo
            // 
            this.barraTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(50)))), ((int)(((byte)(124)))));
            this.barraTitulo.Controls.Add(this.btnMinizar);
            this.barraTitulo.Controls.Add(this.btnCerrar);
            this.barraTitulo.Controls.Add(this.button1);
            this.barraTitulo.Controls.Add(this.button2);
            this.barraTitulo.Controls.Add(this.lblTitulo);
            this.barraTitulo.Cursor = System.Windows.Forms.Cursors.Default;
            this.barraTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.barraTitulo.Location = new System.Drawing.Point(0, 0);
            this.barraTitulo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.barraTitulo.Name = "barraTitulo";
            this.barraTitulo.Size = new System.Drawing.Size(1154, 25);
            this.barraTitulo.TabIndex = 28;
            // 
            // btnMinizar
            // 
            this.btnMinizar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnMinizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinizar.FlatAppearance.BorderSize = 0;
            this.btnMinizar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnMinizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnMinizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinizar.Image = global::COVENTAF.Properties.Resources.Icono_Minimizar;
            this.btnMinizar.Location = new System.Drawing.Point(1098, 5);
            this.btnMinizar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnMinizar.Name = "btnMinizar";
            this.btnMinizar.Size = new System.Drawing.Size(13, 15);
            this.btnMinizar.TabIndex = 12;
            this.btnMinizar.UseVisualStyleBackColor = true;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Image = global::COVENTAF.Properties.Resources.Icono_cerrar_FN;
            this.btnCerrar.Location = new System.Drawing.Point(1128, 5);
            this.btnCerrar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(13, 15);
            this.btnCerrar.TabIndex = 11;
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::COVENTAF.Properties.Resources.Icono_Minimizar;
            this.button1.Location = new System.Drawing.Point(1711, -38);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(47, 46);
            this.button1.TabIndex = 10;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = global::COVENTAF.Properties.Resources.Icono_cerrar_FN;
            this.button2.Location = new System.Drawing.Point(1763, -38);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(47, 46);
            this.button2.TabIndex = 9;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.lblTitulo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(4, 4);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(110, 17);
            this.lblTitulo.TabIndex = 8;
            this.lblTitulo.Text = "Punto de Venta";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CalendarForeColor = System.Drawing.Color.Gold;
            this.dtpFechaInicio.CalendarMonthBackground = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dtpFechaInicio.CalendarTitleForeColor = System.Drawing.Color.Gold;
            this.dtpFechaInicio.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicio.Location = new System.Drawing.Point(613, 598);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(159, 26);
            this.dtpFechaInicio.TabIndex = 30;
            this.dtpFechaInicio.Visible = false;
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CalendarMonthBackground = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dtpFechaFinal.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFinal.Location = new System.Drawing.Point(791, 598);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(163, 26);
            this.dtpFechaFinal.TabIndex = 31;
            this.dtpFechaFinal.Visible = false;
            // 
            // btnNuevaFactura
            // 
            this.btnNuevaFactura.FlatAppearance.BorderSize = 0;
            this.btnNuevaFactura.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevaFactura.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnNuevaFactura.Image = global::COVENTAF.Properties.Resources.add_notes;
            this.btnNuevaFactura.Location = new System.Drawing.Point(25, 534);
            this.btnNuevaFactura.Name = "btnNuevaFactura";
            this.btnNuevaFactura.Size = new System.Drawing.Size(64, 57);
            this.btnNuevaFactura.TabIndex = 39;
            this.btnNuevaFactura.UseVisualStyleBackColor = true;
            this.btnNuevaFactura.Visible = false;
            this.btnNuevaFactura.Click += new System.EventHandler(this.btnNuevaFactura_Click);
            // 
            // pnRight
            // 
            this.pnRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(50)))), ((int)(((byte)(124)))));
            this.pnRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnRight.Location = new System.Drawing.Point(1149, 25);
            this.pnRight.Name = "pnRight";
            this.pnRight.Size = new System.Drawing.Size(5, 629);
            this.pnRight.TabIndex = 147;
            // 
            // pnlButtom
            // 
            this.pnlButtom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(50)))), ((int)(((byte)(124)))));
            this.pnlButtom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtom.Location = new System.Drawing.Point(0, 649);
            this.pnlButtom.Name = "pnlButtom";
            this.pnlButtom.Size = new System.Drawing.Size(1149, 5);
            this.pnlButtom.TabIndex = 149;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(50)))), ((int)(((byte)(124)))));
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 25);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(5, 624);
            this.pnlLeft.TabIndex = 150;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(13)))), ((int)(((byte)(66)))));
            this.panel5.Controls.Add(this.pictureBox2);
            this.panel5.Controls.Add(this.lblNoCierre);
            this.panel5.Controls.Add(this.lblCajaApertura);
            this.panel5.Controls.Add(this.pictureBox26);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.label20);
            this.panel5.Controls.Add(this.pictureBox11);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(5, 25);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1144, 69);
            this.panel5.TabIndex = 151;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Image = global::COVENTAF.Properties.Resources.logo;
            this.pictureBox2.Location = new System.Drawing.Point(736, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(410, 55);
            this.pictureBox2.TabIndex = 146;
            this.pictureBox2.TabStop = false;
            // 
            // lblNoCierre
            // 
            this.lblNoCierre.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblNoCierre.AutoSize = true;
            this.lblNoCierre.BackColor = System.Drawing.Color.Transparent;
            this.lblNoCierre.Font = new System.Drawing.Font("Bahnschrift Condensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblNoCierre.ForeColor = System.Drawing.Color.SeaShell;
            this.lblNoCierre.Location = new System.Drawing.Point(309, 36);
            this.lblNoCierre.Name = "lblNoCierre";
            this.lblNoCierre.Size = new System.Drawing.Size(84, 25);
            this.lblNoCierre.TabIndex = 145;
            this.lblNoCierre.Text = "No. Cierre:";
            // 
            // lblCajaApertura
            // 
            this.lblCajaApertura.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCajaApertura.AutoSize = true;
            this.lblCajaApertura.BackColor = System.Drawing.Color.Transparent;
            this.lblCajaApertura.Font = new System.Drawing.Font("Bahnschrift Condensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCajaApertura.ForeColor = System.Drawing.Color.SeaShell;
            this.lblCajaApertura.Location = new System.Drawing.Point(264, 8);
            this.lblCajaApertura.Name = "lblCajaApertura";
            this.lblCajaApertura.Size = new System.Drawing.Size(282, 25);
            this.lblCajaApertura.TabIndex = 144;
            this.lblCajaApertura.Text = "Caja de Apertura: Sin Apertura de Caja";
            // 
            // pictureBox26
            // 
            this.pictureBox26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox26.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox26.Image = global::COVENTAF.Properties.Resources.logo;
            this.pictureBox26.Location = new System.Drawing.Point(2564, 3);
            this.pictureBox26.Name = "pictureBox26";
            this.pictureBox26.Size = new System.Drawing.Size(410, 55);
            this.pictureBox26.TabIndex = 143;
            this.pictureBox26.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label10.ForeColor = System.Drawing.Color.Gold;
            this.label10.Location = new System.Drawing.Point(10, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(121, 30);
            this.label10.TabIndex = 140;
            this.label10.Text = "COVENTAF";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label20.ForeColor = System.Drawing.Color.SeaShell;
            this.label20.Location = new System.Drawing.Point(34, 36);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(151, 23);
            this.label20.TabIndex = 141;
            this.label20.Text = "Punto de Venta";
            // 
            // pictureBox11
            // 
            this.pictureBox11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox11.Image = global::COVENTAF.Properties.Resources.logo;
            this.pictureBox11.Location = new System.Drawing.Point(3387, 2);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(410, 55);
            this.pictureBox11.TabIndex = 142;
            this.pictureBox11.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnNuevaFactura1);
            this.panel2.Controls.Add(this.btnBuscar);
            this.panel2.Controls.Add(this.cboTipoFiltro);
            this.panel2.Controls.Add(this.txtBusqueda);
            this.panel2.Location = new System.Drawing.Point(13, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(949, 67);
            this.panel2.TabIndex = 153;
            // 
            // btnNuevaFactura1
            // 
            this.btnNuevaFactura1.FlatAppearance.BorderSize = 0;
            this.btnNuevaFactura1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevaFactura1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnNuevaFactura1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnNuevaFactura1.IconChar = FontAwesome.Sharp.IconChar.FileCirclePlus;
            this.btnNuevaFactura1.IconColor = System.Drawing.Color.Gainsboro;
            this.btnNuevaFactura1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNuevaFactura1.Location = new System.Drawing.Point(17, 7);
            this.btnNuevaFactura1.Name = "btnNuevaFactura1";
            this.btnNuevaFactura1.Size = new System.Drawing.Size(115, 51);
            this.btnNuevaFactura1.TabIndex = 0;
            this.btnNuevaFactura1.Text = "Nueva Factura";
            this.btnNuevaFactura1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNuevaFactura1.UseVisualStyleBackColor = true;
            this.btnNuevaFactura1.Click += new System.EventHandler(this.btnNuevaFactura1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(42)))), ((int)(((byte)(81)))));
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.btnAperturaCaja);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.btnCierreCaja);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1154, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(186, 654);
            this.panel1.TabIndex = 152;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Bahnschrift Condensed", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(51, 238);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(72, 23);
            this.label21.TabIndex = 148;
            this.label21.Text = "Consultas";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.Location = new System.Drawing.Point(33, 221);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(128, 2);
            this.panel6.TabIndex = 147;
            // 
            // btnAperturaCaja
            // 
            this.btnAperturaCaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAperturaCaja.FlatAppearance.BorderSize = 0;
            this.btnAperturaCaja.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(92)))), ((int)(((byte)(133)))));
            this.btnAperturaCaja.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(50)))), ((int)(((byte)(97)))));
            this.btnAperturaCaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAperturaCaja.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAperturaCaja.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnAperturaCaja.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAperturaCaja.Location = new System.Drawing.Point(2, 48);
            this.btnAperturaCaja.Margin = new System.Windows.Forms.Padding(2);
            this.btnAperturaCaja.Name = "btnAperturaCaja";
            this.btnAperturaCaja.Size = new System.Drawing.Size(182, 40);
            this.btnAperturaCaja.TabIndex = 5;
            this.btnAperturaCaja.Text = "Apertura de Caja";
            this.btnAperturaCaja.UseVisualStyleBackColor = true;
            this.btnAperturaCaja.Click += new System.EventHandler(this.btnAperturaCaja_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bahnschrift Condensed", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(30, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 29);
            this.label3.TabIndex = 4;
            this.label3.Text = "Punto de Venta";
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.button6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button6.ForeColor = System.Drawing.Color.Gainsboro;
            this.button6.Image = global::COVENTAF.Properties.Resources.logout;
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(2, 1478);
            this.button6.Margin = new System.Windows.Forms.Padding(2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(188, 40);
            this.button6.TabIndex = 2;
            this.button6.Text = "Salir del Sistema";
            this.button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(92)))), ((int)(((byte)(133)))));
            this.button7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(50)))), ((int)(((byte)(97)))));
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button7.ForeColor = System.Drawing.Color.Gainsboro;
            this.button7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.Location = new System.Drawing.Point(0, 158);
            this.button7.Margin = new System.Windows.Forms.Padding(2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(184, 40);
            this.button7.TabIndex = 0;
            this.button7.Text = "Seguridad";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Visible = false;
            // 
            // btnCierreCaja
            // 
            this.btnCierreCaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCierreCaja.FlatAppearance.BorderSize = 0;
            this.btnCierreCaja.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(92)))), ((int)(((byte)(133)))));
            this.btnCierreCaja.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(50)))), ((int)(((byte)(97)))));
            this.btnCierreCaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCierreCaja.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCierreCaja.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnCierreCaja.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCierreCaja.Location = new System.Drawing.Point(2, 96);
            this.btnCierreCaja.Margin = new System.Windows.Forms.Padding(2);
            this.btnCierreCaja.Name = "btnCierreCaja";
            this.btnCierreCaja.Size = new System.Drawing.Size(176, 40);
            this.btnCierreCaja.TabIndex = 0;
            this.btnCierreCaja.Text = "Cierre de Caja";
            this.btnCierreCaja.UseVisualStyleBackColor = true;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // frmPuntoVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(92)))), ((int)(((byte)(133)))));
            this.ClientSize = new System.Drawing.Size(1340, 654);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlButtom);
            this.Controls.Add(this.pnRight);
            this.Controls.Add(this.btnNuevaFactura);
            this.Controls.Add(this.dtpFechaFinal);
            this.Controls.Add(this.dtpFechaInicio);
            this.Controls.Add(this.barraTitulo);
            this.Controls.Add(this.dgvPuntoVenta);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPuntoVenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Punto de Venta";
            this.Load += new System.EventHandler(this.frmPuntoVenta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPuntoVenta)).EndInit();
            this.barraTitulo.ResumeLayout(false);
            this.barraTitulo.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }



        private System.Windows.Forms.ComboBox cboTipoFiltro;
        private System.Windows.Forms.DataGridView dgvPuntoVenta;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Panel barraTitulo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnMinizar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Button btnNuevaFactura;
        private System.Windows.Forms.Panel pnRight;
        private System.Windows.Forms.Panel pnlButtom;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox pictureBox26;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.PictureBox pictureBox11;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button btnCierreCaja;
        private System.Windows.Forms.Panel panel2;
        private FontAwesome.Sharp.IconButton btnNuevaFactura1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button btnAperturaCaja;
        private System.Windows.Forms.Label lblNoCierre;
        private System.Windows.Forms.Label lblCajaApertura;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label21;
    }
}