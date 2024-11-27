namespace PRESENTATION
{
    partial class Form_Registrar_Equipo
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
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnRegistrar = new Guna.UI2.WinForms.Guna2Button();
            this.txtCodigoInterno = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtPlacaSena = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtSerial = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtNombreMarca = new Guna.UI2.WinForms.Guna2TextBox();
            this.cbDisponible = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RadioButton_Accesorio = new Guna.UI2.WinForms.Guna2CustomRadioButton();
            this.guna2ContextMenuStrip1 = new Guna.UI2.WinForms.Guna2ContextMenuStrip();
            this.txtDescripcion = new Guna.UI2.WinForms.Guna2TextBox();
            this.RadioButton_Equipo = new Guna.UI2.WinForms.Guna2CustomRadioButton();
            this.txtNombreProducto = new Guna.UI2.WinForms.Guna2TextBox();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(136, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 16);
            this.label8.TabIndex = 7;
            this.label8.Text = "Equipo";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(91, 456);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 16);
            this.label10.TabIndex = 10;
            this.label10.Text = "Descripción";
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRegistrar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnRegistrar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnRegistrar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnRegistrar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(77)))));
            this.btnRegistrar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRegistrar.ForeColor = System.Drawing.Color.White;
            this.btnRegistrar.Location = new System.Drawing.Point(125, 743);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(209, 45);
            this.btnRegistrar.TabIndex = 21;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // txtCodigoInterno
            // 
            this.txtCodigoInterno.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(77)))));
            this.txtCodigoInterno.BorderRadius = 8;
            this.txtCodigoInterno.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCodigoInterno.DefaultText = "";
            this.txtCodigoInterno.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtCodigoInterno.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtCodigoInterno.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCodigoInterno.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCodigoInterno.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.txtCodigoInterno.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCodigoInterno.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCodigoInterno.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(77)))));
            this.txtCodigoInterno.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCodigoInterno.Location = new System.Drawing.Point(96, 106);
            this.txtCodigoInterno.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCodigoInterno.Name = "txtCodigoInterno";
            this.txtCodigoInterno.PasswordChar = '\0';
            this.txtCodigoInterno.PlaceholderText = "Ingrese el codigo interno";
            this.txtCodigoInterno.SelectedText = "";
            this.txtCodigoInterno.Size = new System.Drawing.Size(278, 33);
            this.txtCodigoInterno.TabIndex = 25;
            this.txtCodigoInterno.TextChanged += new System.EventHandler(this.txtCodigoInterno_TextChanged);
            // 
            // txtPlacaSena
            // 
            this.txtPlacaSena.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(77)))));
            this.txtPlacaSena.BorderRadius = 8;
            this.txtPlacaSena.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPlacaSena.DefaultText = "";
            this.txtPlacaSena.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtPlacaSena.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtPlacaSena.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPlacaSena.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPlacaSena.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.txtPlacaSena.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPlacaSena.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPlacaSena.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(77)))));
            this.txtPlacaSena.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPlacaSena.Location = new System.Drawing.Point(94, 281);
            this.txtPlacaSena.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPlacaSena.Name = "txtPlacaSena";
            this.txtPlacaSena.PasswordChar = '\0';
            this.txtPlacaSena.PlaceholderText = "Ingrese el numero de placa SENA";
            this.txtPlacaSena.SelectedText = "";
            this.txtPlacaSena.Size = new System.Drawing.Size(278, 33);
            this.txtPlacaSena.TabIndex = 27;
            // 
            // txtSerial
            // 
            this.txtSerial.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(77)))));
            this.txtSerial.BorderRadius = 8;
            this.txtSerial.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSerial.DefaultText = "";
            this.txtSerial.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSerial.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSerial.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSerial.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSerial.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.txtSerial.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSerial.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSerial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(77)))));
            this.txtSerial.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSerial.Location = new System.Drawing.Point(94, 221);
            this.txtSerial.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSerial.Name = "txtSerial";
            this.txtSerial.PasswordChar = '\0';
            this.txtSerial.PlaceholderText = "Ingrese el numero de serial";
            this.txtSerial.SelectedText = "";
            this.txtSerial.Size = new System.Drawing.Size(278, 33);
            this.txtSerial.TabIndex = 28;
            // 
            // txtNombreMarca
            // 
            this.txtNombreMarca.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(77)))));
            this.txtNombreMarca.BorderRadius = 8;
            this.txtNombreMarca.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNombreMarca.DefaultText = "";
            this.txtNombreMarca.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtNombreMarca.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtNombreMarca.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNombreMarca.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNombreMarca.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.txtNombreMarca.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNombreMarca.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNombreMarca.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(77)))));
            this.txtNombreMarca.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNombreMarca.Location = new System.Drawing.Point(93, 344);
            this.txtNombreMarca.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNombreMarca.Name = "txtNombreMarca";
            this.txtNombreMarca.PasswordChar = '\0';
            this.txtNombreMarca.PlaceholderText = "Ingrese el nombre de la marca";
            this.txtNombreMarca.SelectedText = "";
            this.txtNombreMarca.Size = new System.Drawing.Size(278, 33);
            this.txtNombreMarca.TabIndex = 29;
            // 
            // cbDisponible
            // 
            this.cbDisponible.BackColor = System.Drawing.Color.Transparent;
            this.cbDisponible.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(77)))));
            this.cbDisponible.BorderRadius = 8;
            this.cbDisponible.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbDisponible.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDisponible.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.cbDisponible.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbDisponible.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbDisponible.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbDisponible.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(77)))));
            this.cbDisponible.ItemHeight = 30;
            this.cbDisponible.Items.AddRange(new object[] {
            "Disponible",
            "No Disponible"});
            this.cbDisponible.Location = new System.Drawing.Point(96, 401);
            this.cbDisponible.Name = "cbDisponible";
            this.cbDisponible.Size = new System.Drawing.Size(279, 36);
            this.cbDisponible.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(267, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Accesorio";
            // 
            // RadioButton_Accesorio
            // 
            this.RadioButton_Accesorio.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(77)))));
            this.RadioButton_Accesorio.CheckedState.BorderThickness = 0;
            this.RadioButton_Accesorio.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(77)))));
            this.RadioButton_Accesorio.CheckedState.InnerColor = System.Drawing.Color.White;
            this.RadioButton_Accesorio.Location = new System.Drawing.Point(244, 48);
            this.RadioButton_Accesorio.Name = "RadioButton_Accesorio";
            this.RadioButton_Accesorio.Size = new System.Drawing.Size(20, 20);
            this.RadioButton_Accesorio.TabIndex = 8;
            this.RadioButton_Accesorio.Text = "guna2CustomRadioButton2";
            this.RadioButton_Accesorio.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.RadioButton_Accesorio.UncheckedState.BorderThickness = 2;
            this.RadioButton_Accesorio.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.RadioButton_Accesorio.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.RadioButton_Accesorio.CheckedChanged += new System.EventHandler(this.RadioButton_Accesorio_CheckedChanged_1);
            // 
            // guna2ContextMenuStrip1
            // 
            this.guna2ContextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.guna2ContextMenuStrip1.Name = "guna2ContextMenuStrip1";
            this.guna2ContextMenuStrip1.RenderStyle.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.guna2ContextMenuStrip1.RenderStyle.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2ContextMenuStrip1.RenderStyle.ColorTable = null;
            this.guna2ContextMenuStrip1.RenderStyle.RoundedEdges = true;
            this.guna2ContextMenuStrip1.RenderStyle.SelectionArrowColor = System.Drawing.Color.White;
            this.guna2ContextMenuStrip1.RenderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.guna2ContextMenuStrip1.RenderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.guna2ContextMenuStrip1.RenderStyle.SeparatorColor = System.Drawing.Color.Gainsboro;
            this.guna2ContextMenuStrip1.RenderStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.guna2ContextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(77)))));
            this.txtDescripcion.BorderRadius = 8;
            this.txtDescripcion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDescripcion.DefaultText = "";
            this.txtDescripcion.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtDescripcion.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtDescripcion.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDescripcion.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDescripcion.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.txtDescripcion.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDescripcion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(77)))));
            this.txtDescripcion.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDescripcion.Location = new System.Drawing.Point(94, 489);
            this.txtDescripcion.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.PasswordChar = '\0';
            this.txtDescripcion.PlaceholderText = "Ingrese las notas asociadas al elemento";
            this.txtDescripcion.SelectedText = "";
            this.txtDescripcion.Size = new System.Drawing.Size(278, 188);
            this.txtDescripcion.TabIndex = 40;
            // 
            // RadioButton_Equipo
            // 
            this.RadioButton_Equipo.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(77)))));
            this.RadioButton_Equipo.CheckedState.BorderThickness = 0;
            this.RadioButton_Equipo.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(77)))));
            this.RadioButton_Equipo.CheckedState.InnerColor = System.Drawing.Color.White;
            this.RadioButton_Equipo.Location = new System.Drawing.Point(111, 47);
            this.RadioButton_Equipo.Name = "RadioButton_Equipo";
            this.RadioButton_Equipo.Size = new System.Drawing.Size(20, 20);
            this.RadioButton_Equipo.TabIndex = 10;
            this.RadioButton_Equipo.Text = "guna2CustomRadioButton2";
            this.RadioButton_Equipo.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.RadioButton_Equipo.UncheckedState.BorderThickness = 2;
            this.RadioButton_Equipo.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.RadioButton_Equipo.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.RadioButton_Equipo.CheckedChanged += new System.EventHandler(this.RadioButton_Equipo_CheckedChanged);
            // 
            // txtNombreProducto
            // 
            this.txtNombreProducto.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(77)))));
            this.txtNombreProducto.BorderRadius = 8;
            this.txtNombreProducto.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNombreProducto.DefaultText = "";
            this.txtNombreProducto.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtNombreProducto.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtNombreProducto.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNombreProducto.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNombreProducto.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.txtNombreProducto.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNombreProducto.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNombreProducto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(48)))), ((int)(((byte)(77)))));
            this.txtNombreProducto.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNombreProducto.Location = new System.Drawing.Point(94, 166);
            this.txtNombreProducto.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNombreProducto.Name = "txtNombreProducto";
            this.txtNombreProducto.PasswordChar = '\0';
            this.txtNombreProducto.PlaceholderText = "Ingrese el nombre producto";
            this.txtNombreProducto.SelectedText = "";
            this.txtNombreProducto.Size = new System.Drawing.Size(278, 33);
            this.txtNombreProducto.TabIndex = 41;
            // 
            // Form_Registrar_Equipo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(475, 879);
            this.Controls.Add(this.txtNombreProducto);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RadioButton_Equipo);
            this.Controls.Add(this.RadioButton_Accesorio);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtPlacaSena);
            this.Controls.Add(this.cbDisponible);
            this.Controls.Add(this.txtNombreMarca);
            this.Controls.Add(this.txtSerial);
            this.Controls.Add(this.txtCodigoInterno);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.label10);
            this.Name = "Form_Registrar_Equipo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_Registrar_Equipo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private Guna.UI2.WinForms.Guna2Button btnRegistrar;
        private Guna.UI2.WinForms.Guna2TextBox txtCodigoInterno;
        private Guna.UI2.WinForms.Guna2TextBox txtPlacaSena;
        private Guna.UI2.WinForms.Guna2TextBox txtSerial;
        private Guna.UI2.WinForms.Guna2TextBox txtNombreMarca;
        private Guna.UI2.WinForms.Guna2ComboBox cbDisponible;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2CustomRadioButton RadioButton_Accesorio;
        private Guna.UI2.WinForms.Guna2ContextMenuStrip guna2ContextMenuStrip1;
        private Guna.UI2.WinForms.Guna2TextBox txtDescripcion;
        private Guna.UI2.WinForms.Guna2CustomRadioButton RadioButton_Equipo;
        private Guna.UI2.WinForms.Guna2TextBox txtNombreProducto;
    }
}