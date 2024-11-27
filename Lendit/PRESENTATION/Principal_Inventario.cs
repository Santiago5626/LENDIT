using bll;
using BLL;
using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PRESENTATION
{
    public partial class Principal_Inventario : Form
    {
        private bool dragging = false;
        private Point startPoint = new Point(0, 0);
        private readonly ProductoService _productoService;
        private readonly EstadisticaService _estadisticaService;
        public Principal_Inventario()
        {
            InitializeComponent();
            _productoService = new ProductoService();
            SetupDataGridView();
            _estadisticaService = new EstadisticaService();
            this.MouseDown += new MouseEventHandler(Form_MouseDown);
            this.MouseMove += new MouseEventHandler(Form_MouseMove);
            this.MouseUp += new MouseEventHandler(Form_MouseUp);
        }

        private void Principal_Inventario_Load(object sender, EventArgs e)
        {
            Diseños();
            CargarDatosDesdeBaseDeDatos();
            label_Nombreusuario.Text = Sesion.nombre;
            LbEquipoDis.Text = _estadisticaService.ObtenerSolicitantes("Disponible", "Equipo").ToString();
            LbEquipoNoDis.Text = _estadisticaService.ObtenerSolicitantes("No disponible", "Equipo").ToString();
            LbAccesorioDis.Text = _estadisticaService.ObtenerSolicitantes("Disponible", "Accesorio").ToString();
            LbAccesorioNoDis.Text = _estadisticaService.ObtenerSolicitantes("No disponible", "Accesorio").ToString();
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
            guna2DataGridView1.Columns.Clear(); // Limpiar cualquier columna existente
            guna2DataGridView1.ScrollBars = ScrollBars.Both;
            guna2DataGridView1.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "CheckBox", HeaderText = "", Width = 30 });
            guna2DataGridView1.Columns.Add("CODIGOINTERNO", "Código Interno");
            guna2DataGridView1.Columns["CODIGOINTERNO"].Width = 120;
            guna2DataGridView1.Columns.Add("CODIGOSENA", "Código SENA");
            guna2DataGridView1.Columns["CODIGOSENA"].Width = 130;
            guna2DataGridView1.Columns.Add("SERIAL", "Serial");
            guna2DataGridView1.Columns["SERIAL"].Width = 180;
            guna2DataGridView1.Columns.Add("NOMBREPRODUCTO", "Nombre Producto");
            guna2DataGridView1.Columns["NOMBREPRODUCTO"].Width = 230;
            guna2DataGridView1.Columns.Add("ESTADO", "Estado");
            guna2DataGridView1.Columns["ESTADO"].Width = 100;

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




        public void CargarDatosDesdeBaseDeDatos(string serial = "", string codigointerno = "")
        {
            try
            {
                DataTable dtProductos = _productoService.ObtenerProductosTable();

                if (dtProductos != null && dtProductos.Rows.Count > 0)
                {
                    guna2DataGridView1.Rows.Clear();
                    foreach (DataRow row in dtProductos.Rows)
                    {
                        guna2DataGridView1.Rows.Add(false,
                            row["CODIGOINTERNO"].ToString(),
                            row["CODIGOSENA"].ToString(),
                            row["SERIAL"].ToString(),
                            row["NOMBREPRODUCTO"].ToString(),
                            row["ESTADO"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CargarDatosDesdeBaseDeDatosSerialCodigoInterno(string serial = "", string codigointerno = "")
        {
            try
            {
                DataTable dtProductos = _productoService.ObtenerProductoPorSerialYCodigoInterno(serial, codigointerno);

                if (dtProductos != null && dtProductos.Rows.Count > 0)
                {
                    guna2DataGridView1.Rows.Clear();
                    foreach (DataRow row in dtProductos.Rows)
                    {
                        guna2DataGridView1.Rows.Add(false,
                            row["CODIGOINTERNO"].ToString(),
                            row["CODIGOSENA"].ToString(),
                            row["SERIAL"].ToString(),
                            row["NOMBREPRODUCTO"].ToString(),
                            row["ESTADO"].ToString());
                    }
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

        private void Button_agregar_Click(object sender, EventArgs e)
        {
            Form_Registrar_Equipo form_Registrar_Equipo = new Form_Registrar_Equipo();
            form_Registrar_Equipo.Show();
        }

        private void Button_Editar_Click(object sender, EventArgs e)
        {
            Form_Editar_Equipo form_Editar_Equipo = new Form_Editar_Equipo();
            form_Editar_Equipo.Show();
        }

        private void Button_BuscarporID_Click(object sender, EventArgs e)
        {
            
            if(txtCodigo.Text == "")
            {
                CargarDatosDesdeBaseDeDatos();
                txtCodigo.Text = "";
            }
            else
            {

                CargarDatosDesdeBaseDeDatosSerialCodigoInterno("", txtCodigo.Text);
            }
        }

        private void Button_Buscarporficha_Click(object sender, EventArgs e)
        {
            if (txtCategoria.Text == "")
            {
                CargarDatosDesdeBaseDeDatos();
                txtCategoria.Text = "";

            }
            else
            {

                CargarDatosDesdeBaseDeDatosSerialCodigoInterno(txtCategoria.Text,"");
            }
        }


        private void Button_Inicio_Click(object sender, EventArgs e)
        {
            Principal form_Principal = new Principal();
            form_Principal.Show();
            this.Hide();
        }

        private void Button_Circulacion_Click(object sender, EventArgs e)
        {
            Principal_Circulacion principal_circulacion = new Principal_Circulacion();
            principal_circulacion.Show();
            this.Hide();
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
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

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
