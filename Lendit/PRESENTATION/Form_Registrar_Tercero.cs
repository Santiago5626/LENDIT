using bll;
using BLL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRESENTATION
{
    public partial class Form_Registrar_Tercero : Form
    {
        private readonly TerceroService terceroService;
        private readonly CodigoProgramaService codigoProgramaService;
        private readonly RolSolicitanteService rolSolicitanteService;
        private readonly SolicitanteService solicitanteService;
        private readonly FichaService fichaService;




        public Form_Registrar_Tercero()
        {
            InitializeComponent();
            terceroService = new TerceroService();
            codigoProgramaService = new CodigoProgramaService();
            rolSolicitanteService = new RolSolicitanteService();
            solicitanteService = new SolicitanteService();
            fichaService = new FichaService();
            Diseños();
        }
        private void Diseños()
        {
            label1.Font = new Font("Work Sans", 13F, FontStyle.Bold);
            label2.Font = new Font("Work Sans", 13F, FontStyle.Bold);
            label3.Font = new Font("Work Sans", 13F, FontStyle.Bold);
            label4.Font = new Font("Work Sans", 13F, FontStyle.Bold);
            label5.Font = new Font("Work Sans", 13F, FontStyle.Bold);
            label6.Font = new Font("Work Sans", 13F, FontStyle.Bold);
            label7.Font = new Font("Work Sans", 13F, FontStyle.Bold);
            label8.Font = new Font("Work Sans", 13F, FontStyle.Bold);
            label9.Font = new Font("Work Sans", 13F, FontStyle.Bold);
            label10.Font = new Font("Work Sans", 13F, FontStyle.Bold);
            LlenarComboBoxProgramas();
            LlenarComboBoxRolesSolicitante();

            cbCodigoPrograma.Enabled = false;
            cbFicha.Enabled = false;
        }

        public void LimpiarCampos()
        {
            txtIdentificacion.Text = "";
            txtPrimerNombre.Text = "";
            txtSegundoNombre.Text = "";
            txtPrimerApellido.Text = "";
            txtSegundoApellido.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";
            cbGenero.SelectedIndex = -1;
            cbCodigoPrograma.SelectedIndex = -1;
            cbRolSolicitante.SelectedIndex = -1;
        }



        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            var programaSeleccionado = cbCodigoPrograma.SelectedItem;
            string codPrograma = (programaSeleccionado as dynamic)?.Value;

            var selectedItem = cbRolSolicitante.SelectedItem as dynamic;
            int rolSolicitanteId = selectedItem?.Value;



            Tercero nuevoTercero = new Tercero
            {
                Identificacion = txtIdentificacion.Text,
                PrimerNombre = txtPrimerNombre.Text,
                SegundoNombre = txtSegundoNombre.Text,
                PrimerApellido = txtPrimerApellido.Text,
                SegundoApellido = txtSegundoApellido.Text,
                Correo = txtCorreo.Text,
                Telefono = txtTelefono.Text,
                Genero = cbGenero.Text == "Masculino" ? "M" : "F",
                CodPrograma = codPrograma,
                IdRol = rolSolicitanteId
            };
            string resultadoTercero = terceroService.RegistrarTercero(nuevoTercero);
            MessageBox.Show(resultadoTercero, "Registro de Tercero", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (resultadoTercero.Contains("exitoso"))
            {

                string fichaId = null;

                if (cbFicha.SelectedItem != null)
                {
                    var selectedItemFicha = cbFicha.SelectedItem as dynamic;
                    fichaId = selectedItemFicha.Key;
                }
                Solicitante nuevoSolicitante = new Solicitante
                {
                    Identificacion = nuevoTercero.Identificacion,
                    CodPrograma = codPrograma,
                    CodFicha = fichaId,
                  
                };

                string msgSolicitante = solicitanteService.RegistrarSolicitante(nuevoSolicitante);
                MessageBox.Show(msgSolicitante, "Registro de Solicitante", MessageBoxButtons.OK, MessageBoxIcon.Information);


                Principal principalForm = (Principal)Application.OpenForms["Principal"];
                if (principalForm != null)
                {
                    principalForm.CargarDatosDesdeBaseDeDatos();
                }
                LimpiarCampos();


            }

        }

        private void LlenarComboBoxProgramas()
        {
            List<Tuple<string, string>> programas = codigoProgramaService.ObtenerProgramas();

            cbCodigoPrograma.Items.Clear();
            foreach (var programa in programas)
            {
                cbCodigoPrograma.Items.Add(new { Text = programa.Item2, Value = programa.Item1 });
            }

            cbCodigoPrograma.DisplayMember = "Text";
            cbCodigoPrograma.ValueMember = "Value";
        }
        private void LlenarComboBoxFicha()
        {
            // Obtener el código del programa seleccionado desde el ComboBox
            var codPrograma = cbCodigoPrograma.SelectedItem as dynamic;
            string codigoPrograma = codPrograma?.Value.ToString();  // Obtener el código del programa

            if (!string.IsNullOrEmpty(codigoPrograma))
            {
                // Obtener las fichas desde el servicio usando el código de programa
                List<Tuple<string, string, DateTime, DateTime>> fichas = fichaService.ObtenerFichas(codigoPrograma);

                // Limpiar los elementos actuales del ComboBox
                cbFicha.Items.Clear();

                // Llenar el ComboBox con los elementos obtenidos
                foreach (var ficha in fichas)
                {
                    // Crear un KeyValuePair para almacenar el código de la ficha y su nombre
                    var item = new KeyValuePair<string, string>(ficha.Item1, ficha.Item2); // ficha.Item1 es el código, ficha.Item2 es el nombre
                    cbFicha.Items.Add(item);
                }

                // Definir qué propiedad se mostrará y cuál será el valor
                cbFicha.DisplayMember = "Value";  // Se mostrará el nombre de la ficha (ficha.Item2)
                cbFicha.ValueMember = "Key";      // El valor será el código de la ficha (ficha.Item1)
            }
            else
            {
                // Limpiar el ComboBox si no hay un código de programa válido
                cbFicha.Items.Clear();
                cbFicha.Enabled = false;
            }
        }



        private void LlenarComboBoxRolesSolicitante()
        {
            List<Tuple<int, string>> rolesSolicitante = rolSolicitanteService.ObtenerRolesSolicitante();

            cbRolSolicitante.Items.Clear();
            foreach (var rol in rolesSolicitante)
            {
                cbRolSolicitante.Items.Add(new { Text = rol.Item2, Value = rol.Item1 });
            }

            cbRolSolicitante.DisplayMember = "Text";
            cbRolSolicitante.ValueMember = "Value";
        }


        private void cbRolSolicitante_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = cbRolSolicitante.SelectedItem as dynamic;
            string rolSolicitante = selectedItem?.Text;  // Accede al texto del rol

            Console.WriteLine(rolSolicitante);
            // Comprobar si el rol es "Aprendiz"
            if (rolSolicitante == "Aprendiz")
            {
                // Habilitar el ComboBox de programa
                cbCodigoPrograma.Enabled = true;
                LlenarComboBoxProgramas();
                // Solo habilitar el ComboBox de fichas si se ha seleccionado un programa
                if (cbCodigoPrograma.SelectedItem != null)
                {
                    cbFicha.Enabled = true;
                    LlenarComboBoxFicha(); // Llenar las fichas basadas en el programa seleccionado
                }
                else
                {
                    cbFicha.Enabled = false; // Si no hay programa seleccionado, deshabilitar ficha
                }
            }
            else
            {
                // Deshabilitar ambos ComboBox si el rol no es "Aprendiz"
                cbCodigoPrograma.Enabled = false;
                cbFicha.Enabled = false;

                // Limpiar los ComboBox
                cbFicha.Items.Clear();
                cbCodigoPrograma.Items.Clear();
            }
        }




        private void cbCodigoPrograma_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Solo llenar el ComboBox de fichas si se ha seleccionado un programa
            if (cbCodigoPrograma.SelectedItem != null)
            {
                cbFicha.Enabled = true;
                LlenarComboBoxFicha(); // Llenar las fichas basadas en el programa seleccionado
            }
            else
            {
                cbFicha.Enabled = false;
                cbFicha.Items.Clear();
            }
        }

        private void cbFicha_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtIdentificacion_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

