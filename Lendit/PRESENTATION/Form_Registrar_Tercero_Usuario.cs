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
    public partial class Form_Registrar_Tercero_Usuario : Form
    {
        private readonly TerceroService terceroService;
        private readonly RolSolicitanteService rolSolicitanteService;
        private readonly RolUsuarioService rolUsuarioService;

        private readonly UsuarioService usuarioService;

        public Form_Registrar_Tercero_Usuario()
        {
            InitializeComponent();
            terceroService = new TerceroService();
            rolSolicitanteService = new RolSolicitanteService();
            rolUsuarioService = new RolUsuarioService();
            usuarioService = new UsuarioService();
            LlenarComboBoxRolesUsuario();
            LlenarComboBoxRolesSolicitante();
            Diseños();
            cbRolSolicitante.SelectedIndex = 0;
            
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
            cbRolUsuario.SelectedIndex = -1;
            txtContraseña.Text = "";
        }


        private void btnRegistrar_Click(object sender, EventArgs e)
        {
     


            var selectedItem = cbRolSolicitante.SelectedItem as dynamic;
            int rolSolicitanteId = selectedItem?.Value;

            // lo cree como gestor y se toteo

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
                CodPrograma = null,
                IdRol = rolSolicitanteId
            };
            string resultadoTercero = terceroService.RegistrarTercero(nuevoTercero);
            MessageBox.Show(resultadoTercero, "Registro de Tercero", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (resultadoTercero.Contains("exitoso"))
            {
                var selectedItemUsuario = cbRolUsuario.SelectedItem as dynamic;
            int rolUsuarioId = selectedItemUsuario?.Value;

            Usuario nuevoUsuario = new Usuario
            {
                Identificacion = txtIdentificacion.Text,
                Password = txtContraseña.Text, // Asumiendo que txtContraseña es el control para la contraseña
                IdRol = rolUsuarioId,
                EstadoUsuario =  1 
            };

            // Registrar el usuario usando el servicio correspondiente
            string resultadoUsuario = usuarioService.RegistrarUsuario(nuevoUsuario);

            // Mostrar el resultado del registro
            MessageBox.Show(resultadoUsuario, "Registro de Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);


            Principal principalForm = (Principal)Application.OpenForms["Principal"];
                if (principalForm != null)
                {
                    principalForm.CargarDatosDesdeBaseDeDatos();
                }
                LimpiarCampos();
            }

        }

        private void LlenarComboBoxRolesSolicitante()
        {
            List<Tuple<int, string>> rolesSolicitante = rolSolicitanteService.ObtenerRolesSolicitante();

            cbRolSolicitante.Items.Clear();

            foreach (var rol in rolesSolicitante)
            {
                // Asegurarse de que rol.Item2 sea un string y verificar si contiene "Usuario" (sin importar mayúsculas/minúsculas)
                if (!string.IsNullOrEmpty(rol.Item2) && rol.Item2.IndexOf("Usuario", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    cbRolSolicitante.Items.Add(new { Text = rol.Item2, Value = rol.Item1 });
                }
            }

            cbRolSolicitante.DisplayMember = "Text";
            cbRolSolicitante.ValueMember = "Value";
            /* var selectedItem = cbRolSolicitante.SelectedItem as dynamic;
               string rolSolicitante = selectedItem?.Text; */
        }


        private void LlenarComboBoxRolesUsuario()
        {
            List<Tuple<int, string>> rolesUsuario = rolUsuarioService.ObtenerRolesUsuario();

            cbRolUsuario.Items.Clear();
            foreach (var rol in rolesUsuario)
            {
                cbRolUsuario.Items.Add(new { Text = rol.Item2, Value = rol.Item1 });
            }

            cbRolUsuario.DisplayMember = "Text";
            cbRolUsuario.ValueMember = "Value";
            /* var selectedItem = cbRolUsuario.SelectedItem as dynamic;
               string rolUsuario = selectedItem?.Text; */
        }



        private void txtIdentificacion_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

