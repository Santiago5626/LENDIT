using bll;
using BLL;
using Guna.UI2.WinForms;
using Org.BouncyCastle.Bcpg;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PRESENTATION
{
    public partial class Principal_Encargado : Form
    {
        private bool dragging = false;
        private Point startPoint = new Point(0, 0);
        private readonly TerceroService _terceroService;
        private readonly EstadisticaService _estadisticaService;
        public Principal_Encargado()
        {
            InitializeComponent();
            _terceroService = new TerceroService();
            SetupDataGridView();
            _estadisticaService = new EstadisticaService();
            this.MouseDown += new MouseEventHandler(Form_MouseDown);
            this.MouseMove += new MouseEventHandler(Form_MouseMove);
            this.MouseUp += new MouseEventHandler(Form_MouseUp);
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            Diseños();
            CargarTodosDatosDesdeBaseDeDatos();
            label_Nombreusuario.Text = Sesion.nombre;
            LbEquipoDis.Text = _estadisticaService.ObtenerSolicitantes("Disponible", "Equipo").ToString();
            LbEquipoNoDis.Text = _estadisticaService.ObtenerSolicitantes("No disponible", "Equipo").ToString();
            LbAccesorioDis.Text = _estadisticaService.ObtenerSolicitantes("Disponible", "Accesorio").ToString();
            LbAccesorioNoDis.Text = _estadisticaService.ObtenerSolicitantes("No disponible", "Accesorio").ToString();
        }

        private void Diseños()
        {
            //label5.Font = new Font("Work Sans SemiBold", 16F);
            label4.BackColor = Color.FromArgb(240, 249, 255);
            label4.ForeColor = Color.FromArgb(0, 48, 77);
            label_Nombreusuario.Font = new Font("Work Sans", 21F, FontStyle.Bold);
            LbEquipoDis.Font = new Font("Work Sans", 13F, FontStyle.Bold);
            LbEquipoNoDis.Font = new Font("Work Sans", 21F, FontStyle.Bold);
            LbAccesorioDis.Font = new Font("Work Sans", 21F, FontStyle.Bold);
            LbAccesorioNoDis.Font = new Font("Work Sans", 21F, FontStyle.Bold);
            label20.Font = new Font("Work Sans", 13F, FontStyle.Bold);
            label21.Font = new Font("Work Sans", 13F, FontStyle.Bold);
            label22.Font = new Font("Work Sans", 13F, FontStyle.Bold);
            label23.Font = new Font("Work Sans", 13F, FontStyle.Bold);
            Button_Inicio.Font = new Font("Work Sans", 16F);
            Button_Circulacion.Font = new Font("Work Sans", 16F);
            Button_Reportes.Font = new Font("Work Sans", 16F);
     
            Button_Configuracion.Font = new Font("Work Sans", 16F);
            Button_Cerrar_session.Font = new Font("Work Sans", 16F);
        }

        private void SetupDataGridView()
        {
            guna2DataGridView1.Columns.Clear(); // Limpiar cualquier columna existente
            guna2DataGridView1.ScrollBars = ScrollBars.Both;
            guna2DataGridView1.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "CheckBox", HeaderText = "", Width = 30 });
            guna2DataGridView1.Columns.Add("Nombre", "Nombre");
            guna2DataGridView1.Columns["Nombre"].Width = 150;
            guna2DataGridView1.Columns.Add("Identificación", "Identificación");
            guna2DataGridView1.Columns["Identificación"].Width = 120;
            guna2DataGridView1.Columns.Add("Teléfono", "Teléfono");
            guna2DataGridView1.Columns["Teléfono"].Width = 100;
            guna2DataGridView1.Columns.Add("Correo", "Correo");
            guna2DataGridView1.Columns["Correo"].Width = 200;
            guna2DataGridView1.Columns.Add("Ficha", "Ficha");
            guna2DataGridView1.Columns["Ficha"].Width = 100;
            guna2DataGridView1.Columns.Add("Programa", "Programa");
            guna2DataGridView1.Columns["Programa"].Width = 100;

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



        public void CargarDatosDesdeBaseDeDatos(string identificacion = "", string codficha = "")
        {
            try
            {
                DataTable dtTerceros = _terceroService.ObtenerTerceroPorIdCd(identificacion, codficha);

                if (dtTerceros != null && dtTerceros.Rows.Count > 0)
                {
                    guna2DataGridView1.Rows.Clear();
                    foreach (DataRow row in dtTerceros.Rows)
                    {
                        guna2DataGridView1.Rows.Add(false,
                            row["NOMBRE"].ToString(),
                            row["IDENTIFICACION"].ToString(),
                            row["TELEFONO"].ToString(),
                            row["CORREO"].ToString(),
                            row["CODFICHA"].ToString(),
                            row["CODPROGRAMA"].ToString());
                    }
                }
                else
                {
                    guna2DataGridView1.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CargarTodosDatosDesdeBaseDeDatos()
        {
            try
            {
                DataTable dtTerceros = _terceroService.ObtenerTodosLosTerceros();

                if (dtTerceros != null && dtTerceros.Rows.Count > 0)
                {
                    guna2DataGridView1.Rows.Clear();
                    foreach (DataRow row in dtTerceros.Rows)
                    {
                        guna2DataGridView1.Rows.Add(false,
                            row["NOMBRE"].ToString(),
                            row["IDENTIFICACION"].ToString(),
                            row["TELEFONO"].ToString(),
                            row["CORREO"].ToString(),
                            row["CODFICHA"].ToString(),
                            row["CODPROGRAMA"].ToString());
                    }
                }
                else
                {
                    guna2DataGridView1.Rows.Clear();
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
            Form_Registrar_Tercero form_Registrar_Tercero = new Form_Registrar_Tercero();
            form_Registrar_Tercero.Show();
        }

        private void Button_Editar_Click(object sender, EventArgs e)
        {
            Form_Editar_Tercero form_Editar_Tercero = new Form_Editar_Tercero();
            form_Editar_Tercero.Show();
        }

        private void Button_BuscarporID_Click(object sender, EventArgs e)
        {
            
            if(txtIdentificacion.Text == "")
            {
                CargarTodosDatosDesdeBaseDeDatos();
                txtIdentificacion.Text = "";
            }
            else
            {
                
                CargarDatosDesdeBaseDeDatos(txtIdentificacion.Text, "");
            }
        }

        private void Button_Buscarporficha_Click(object sender, EventArgs e)
        {
            if (txtCdFicha.Text == "")
            {
                CargarTodosDatosDesdeBaseDeDatos();
                txtCdFicha.Text = "";

            }
            else
            {

                CargarDatosDesdeBaseDeDatos("",txtCdFicha.Text);
            }
        }
        
        private void Button_Circulacion_Click(object sender, EventArgs e)
        {
            Principal_Circulacion_Encargado principal_circulacion = new Principal_Circulacion_Encargado();
            principal_circulacion.Show();
            this.Hide();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label_Nombreusuario_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
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

        private void Button_Inicio_Click(object sender, EventArgs e)
        {

        }
    }
}
