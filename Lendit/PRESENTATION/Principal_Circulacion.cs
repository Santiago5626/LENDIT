using bll;
using BLL;
using ENTITY;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PRESENTATION
{
    public partial class Principal_Circulacion : Form
    {
        private bool dragging = false;
        private Point startPoint = new Point(0, 0);
        private readonly ProductoService _productoService;
        private readonly TerceroService _terceroService;
        private readonly SolicitudService _solicitudService;
        public Principal_Circulacion()
        {
            InitializeComponent();
            _productoService = new ProductoService();
            _terceroService = new TerceroService();
            _solicitudService = new SolicitudService();
            SetupDataGridView();
            LlenarComboBoxProductos(cbCodigoInterno, "Disponible");
            //LlenarComboBoxProductos(cbCodigoInternoDevolucion, "No disponible");

            LlenarComboBoxTerceros(cbIdentificacion);
            //LlenarComboBoxTerceros(cbSolicitanteDevolver);
            this.MouseDown += new MouseEventHandler(Form_MouseDown);
            this.MouseMove += new MouseEventHandler(Form_MouseMove);
            this.MouseUp += new MouseEventHandler(Form_MouseUp);

        }

        private void Principal_Inventario_Load(object sender, EventArgs e)
        {
            Diseños();
            CargarDatosDesdeBaseDeDatos();
            label_Nombreusuario.Text = Sesion.nombre;

        }

        private void Diseños()
        {
            label4.BackColor = Color.FromArgb(240, 249, 255);
            label4.ForeColor = Color.FromArgb(0, 48, 77);
            Button_Inicio.Font = new Font("Work Sans", 16F);
            Button_Circulacion.Font = new Font("Work Sans", 16F);
            Button_Reportes.Font = new Font("Work Sans", 16F);
            Button_Inventario.Font = new Font("Work Sans", 16F);
            Button_Configuracion.Font = new Font("Work Sans", 16F);
            Button_Cerrar_session.Font = new Font("Work Sans", 16F);
        }

        private void SetupDataGridView()
        {
            guna2DataGridView1.Columns.Clear();// Limpiar cualquier columna existente
            guna2DataGridView1.ScrollBars = ScrollBars.Both;
            guna2DataGridView1.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "CheckBox", HeaderText = "", Width = 30 });
            guna2DataGridView1.Columns.Add("IDSOLICITUD","IDSOLICITUD");
            guna2DataGridView1.Columns.Add("Nombre", "Nombre");
            guna2DataGridView1.Columns["Nombre"].Width = 120;
            guna2DataGridView1.Columns.Add("Identificación", "Identificación");
            guna2DataGridView1.Columns["Identificación"].Width = 120;
            guna2DataGridView1.Columns.Add("Teléfono", "Teléfono");
            guna2DataGridView1.Columns["Teléfono"].Width = 120;
            guna2DataGridView1.Columns.Add("correo", "Teléfono");
            guna2DataGridView1.Columns["correo"].Width = 120;
            guna2DataGridView1.Columns.Add("Ficha", "Ficha");
            guna2DataGridView1.Columns["Ficha"].Width = 150;
            guna2DataGridView1.Columns.Add("Programa", "Programa");
            guna2DataGridView1.Columns["Programa"].Width = 200;
            guna2DataGridView1.Columns.Add("CODIGOINTERNO", "Código Interno");
            guna2DataGridView1.Columns["CODIGOINTERNO"].Width = 300;

            guna2DataGridView1.RowTemplate.Height = 40;
            guna2DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            guna2DataGridView1.EnableHeadersVisualStyles = false;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 48, 77);
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Work Sans", 12F, FontStyle.Bold);
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = guna2DataGridView1.ColumnHeadersDefaultCellStyle.BackColor;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            guna2DataGridView1.ColumnHeadersHeight = 40;
            guna2DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            guna2DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            guna2DataGridView1.AdvancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;
            guna2DataGridView1.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;
            guna2DataGridView1.AdvancedColumnHeadersBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single;
            guna2DataGridView1.RowHeadersVisible = false;
            guna2DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            guna2DataGridView1.RowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            guna2DataGridView1.RowsDefaultCellStyle.ForeColor = Color.Black;
            guna2DataGridView1.RowsDefaultCellStyle.Font = new Font("Arial", 10F);
            guna2DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            guna2DataGridView1.RowHeadersVisible = false;
            guna2DataGridView1.CellValueChanged += Guna2DataGridView1_CellValueChanged;
            guna2DataGridView1.CurrentCellDirtyStateChanged += Guna2DataGridView1_CurrentCellDirtyStateChanged;
        }




        public void CargarDatosDesdeBaseDeDatos()
        {
            try
            {
                DataTable dtProductos = _solicitudService.ObtenerSolicitudes();

           
                    guna2DataGridView1.Rows.Clear();
                    foreach (DataRow row in dtProductos.Rows)
                    {
                        guna2DataGridView1.Rows.Add(
                            false, // Columna de selección
                            row["IDSOLICITUD"] != null ? Convert.ToInt32(row["IDSOLICITUD"]) : 0,
                            row["NOMBRE"]?.ToString() ?? string.Empty, // Nombre completo del tercero
                            row["IDENTIFICACION"]?.ToString() ?? string.Empty, // Identificación del tercero
                            row["TELEFONO"]?.ToString() ?? string.Empty, // Teléfono
                            row["CORREO"]?.ToString() ?? string.Empty, // Correo electrónico
                            row["CODFICHA"]?.ToString() ?? string.Empty, // Código de ficha
                            row["CODPROGRAMA"]?.ToString() ?? string.Empty, // Código del programa
                            row["CODIGOSINTERNOS"]?.ToString() ?? string.Empty // Códigos internos concatenados
                        );
                    }
        
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public void CargarDatosPorIDyCI(string identificacion, string codigoInterno)
        {
            try
            {
                DataTable dtProductos = _solicitudService.ObtenerSolicitantePorIDyCI(identificacion, codigoInterno);

                if (dtProductos != null && dtProductos.Rows.Count > 0)
                {
                    guna2DataGridView1.Rows.Clear();
                    foreach (DataRow row in dtProductos.Rows)
                    {
                        guna2DataGridView1.Rows.Add(
                            false, // Columna de selección
                            row["IDSOLICITUD"] != null ? Convert.ToInt32(row["IDSOLICITUD"]) : 0,
                            row["NOMBRE"]?.ToString() ?? string.Empty, // Nombre completo del tercero
                            row["IDENTIFICACION"]?.ToString() ?? string.Empty, // Identificación del tercero
                            row["TELEFONO"]?.ToString() ?? string.Empty, // Teléfono
                            row["CORREO"]?.ToString() ?? string.Empty, // Correo electrónico
                            row["CODFICHA"]?.ToString() ?? string.Empty, // Código de ficha
                            row["CODPROGRAMA"]?.ToString() ?? string.Empty, // Código del programa
                            row["CODIGOSINTERNOS"]?.ToString() ?? string.Empty // Códigos internos concatenados
                        );
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron datos para mostrar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }



        private void Guna2DataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (guna2DataGridView1.IsCurrentCellDirty)
            {
                guna2DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void Guna2DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == guna2DataGridView1.Columns["CheckBox"].Index && e.RowIndex >= 0)
            {
                bool isChecked = Convert.ToBoolean(guna2DataGridView1.Rows[e.RowIndex].Cells["CheckBox"].Value);

                if (isChecked)
                {
                    foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                    {
                        if (row.Index != e.RowIndex)
                        {
                            row.Cells["CheckBox"].Value = false;
                        }
                    }
                }
            }
        }
        private void LlenarComboBoxProductos(ComboBox cbCodigoInterno, string estado)
        {
            List<Tuple<string, string, string, string>> productos = _productoService.ObtenerProductos(estado);

            cbCodigoInterno.Items.Clear();
            foreach (var producto in productos)
            {
                cbCodigoInterno.Items.Add(new
                {
                    Text = $"{producto.Item1} - {producto.Item2} - {producto.Item3} ({producto.Item4})", // Nombre del producto, marca y estado
                    Value = producto.Item1 // Código interno
                });
            }

            cbCodigoInterno.DisplayMember = "Text";
            cbCodigoInterno.ValueMember = "Value";
        }

        private void LlenarComboBoxTerceros(ComboBox cbIdentificacion)
        {
            // Obtener la lista de terceros desde el servicio o repositorio
            List<Tuple<string, string>> terceros = _terceroService.ObtenerTercerosCombo();

            // Limpiar los elementos existentes en el ComboBox
            cbIdentificacion.Items.Clear();

            // Agregar cada tercero al ComboBox
            foreach (var tercero in terceros)
            {
                cbIdentificacion.Items.Add(new
                {
                    Text = $"{tercero.Item1}, {tercero.Item2}", // Nombre, identificación y estado del tercero
                    Value = tercero.Item1 // Identificacion
                });
            }

            // Configurar los miembros para mostrar y obtener valores
            cbIdentificacion.DisplayMember = "Text";
            cbIdentificacion.ValueMember = "Value";

        }


        private void button_cerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Button_Cerrar_session_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - startPoint.X, p.Y - startPoint.Y);
            }
        }
       
        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

 

        private void Button_Editar_Click(object sender, EventArgs e)
        {
            Form_Editar_Equipo form_Editar_Equipo = new Form_Editar_Equipo();
            form_Editar_Equipo.Show();
        }

        private void Button_BuscarporID_Click(object sender, EventArgs e)
        {
            

        }

        private void Button_Buscarporficha_Click(object sender, EventArgs e)
        {
       
        }

        private void Button_Inventario_Click(object sender, EventArgs e)
        {
           Principal_Inventario principal_inventario = new Principal_Inventario();
            principal_inventario.Show();
           this.Hide();
        }

        private void Button_Inicio_Click(object sender, EventArgs e)
        {
            Principal form_Principal = new Principal();
            form_Principal.Show();
            this.Hide();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
        
        }
        
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // Crear instancia del servicio
            SolicitudService solicitudService = new SolicitudService();


            var selectedItemIdentificacio  = cbIdentificacion.SelectedItem as dynamic;
            string identificacion = selectedItemIdentificacio?.Value;
            var selectedItemCodigoCliente = cbCodigoInterno.SelectedItem as dynamic;
            string codigoInterno = selectedItemCodigoCliente?.Value;

            // Obtener los valores de los controles UI
            Console.WriteLine("IDENTIFICACION: " + identificacion);
            Console.WriteLine("CODIGOINTERNO: " + codigoInterno);

            // Llamar al servicio para registrar la solicitud
            string resultado = solicitudService.RegistrarSolicitud(
                identificacion,
                codigoInterno,
                1); 
            CargarDatosDesdeBaseDeDatos();
           
            MessageBox.Show(resultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);


            
            var result = MessageBox.Show("¿Desea agregar otro producto para esta identificación?", "Agregar Otro Producto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
               
                LlenarComboBoxProductos(cbCodigoInterno, "Disponible");
             
                cbCodigoInterno.SelectedIndex = -1; 
                if (cbCodigoInterno.Items.Count > 0)
                {
                    cbCodigoInterno.SelectedIndex = 0; 
                }
            }
            else
            {
          
                LlenarComboBoxProductos(cbCodigoInterno, "Disponible");
              
                LlenarComboBoxTerceros(cbIdentificacion);   
                if (cbIdentificacion.Items.Count > 0)
                {
                    cbIdentificacion.SelectedIndex = 0; 
                }
                if (cbCodigoInterno.Items.Count > 0)
                {
                    cbCodigoInterno.SelectedIndex = 0; // Seleccionar el primer tercero por defecto (si lo hay)
                }
                // Limpiar la selección actual de identificación
                cbIdentificacion.SelectedIndex = -1;

                cbCodigoInterno.SelectedIndex = -1; // Limpiar la selección actual de producto
            }
        }

        private void cbCodigoInterno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCodigoInterno.SelectedItem is KeyValuePair<string, string> selectedItem)
            {
                string serialSeleccionado = selectedItem.Key;
                string nombreProductoSeleccionado = selectedItem.Value;

                Console.WriteLine($"Serial seleccionado: {serialSeleccionado}, Producto: {nombreProductoSeleccionado}");
                
            }
        }

        private void cbIdentificacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIdentificacion.SelectedItem is KeyValuePair<string, string> selectedItem)
            {
                string identificacion = selectedItem.Key;
                string nombreProducto = selectedItem.Value;

                Console.WriteLine($"Serial seleccionado: {identificacion}, Producto: {nombreProducto}");
             
            }
        }
         
        private void btnDevolver_Click(object sender, EventArgs e)
        {
           
            SolicitudService solicitudService = new SolicitudService();
       
            int idSolicitud = -1; 
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                DataGridViewCheckBoxCell checkBox = row.Cells["CheckBox"] as DataGridViewCheckBoxCell;
                if (checkBox != null && Convert.ToBoolean(checkBox.Value)) // Verifica si está marcado
                {
                    idSolicitud = Convert.ToInt32(row.Cells["idSolicitud"].Value); 
                    break; 
                }
            }

       
            string resultado = solicitudService.RegistrarSolicitud(
                null,
                null,
                2,
                idSolicitud);
            CargarDatosDesdeBaseDeDatos();
            MessageBox.Show(resultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LlenarComboBoxProductos(cbCodigoInterno, "Disponible");
         
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtBuscar_Identificacion_TextChanged(object sender, EventArgs e)
        {
         
        }

        private void txtBuscar_Codigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (txtBuscar_Identificacion.Text == "")
            {
                CargarDatosDesdeBaseDeDatos();
                txtBuscar_Identificacion.Text = "";
            }
            else
            {
               
                CargarDatosPorIDyCI(txtBuscar_Identificacion.Text, "");
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (txtBuscar_Codigo.Text == "")
            {
                CargarDatosDesdeBaseDeDatos();
                txtBuscar_Codigo.Text = "";
            }
            else
            {

                CargarDatosPorIDyCI("", txtBuscar_Codigo.Text);
            }
        }

        private void cbCodigoInternoDevolucion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label_Nombreusuario_Click(object sender, EventArgs e)
        {

        }

        private void Button_Reportes_Click(object sender, EventArgs e)
        {
            try
            {
                // Abrir un cuadro de diálogo para seleccionar la ubicación del archivo
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    Title = "Guardar Reporte PDF",
                    FileName = $"Reporte_Solicitudes_{DateTime.Now:yyyyMMdd}.pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string rutaArchivo = saveFileDialog.FileName;

                    // Crear instancia de la capa BLL y generar el PDF
                    SolicitudService solicitudService = new SolicitudService();
                    solicitudService.GenerarPDFReporte(rutaArchivo);

                    MessageBox.Show("PDF generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al generar el PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_Circulacion_Click(object sender, EventArgs e)
        {

        }
    }
}
