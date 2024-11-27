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
    public partial class Form_Registrar_Equipo : Form
    {
        private readonly ProductoService productoService;
        private readonly TipoProductoService tipoProductoService;
        public Form_Registrar_Equipo()
        {
            InitializeComponent();
            Diseños();
            productoService = new ProductoService();
            tipoProductoService = new TipoProductoService();
        }
        private void Diseños()
        {
            label1.Font = new Font("Work Sans", 13F, FontStyle.Bold);
            label8.Font = new Font("Work Sans", 13F, FontStyle.Bold);
            label10.Font = new Font("Work Sans", 13F, FontStyle.Bold);

           
        }

        public void LimpiarCampos()
        {
            txtCodigoInterno.Text = "";
            txtPlacaSena.Text = "";
            txtSerial.Text = "";
            txtNombreMarca.Text = "";
            txtNombreProducto.Text = "";
            txtDescripcion.Text = "";
            cbDisponible.SelectedIndex = -1;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {

            int idTipoProducto = 0;
            if (RadioButton_Equipo.Checked)
            {
                idTipoProducto = 1; // ID para "Equipo"
            }
            else if (RadioButton_Accesorio.Checked)
            {
                idTipoProducto = 2; // ID para "Accesorio"
            }
        
            if (idTipoProducto == 0)
            {
                MessageBox.Show("Por favor seleccione el tipo de producto (Equipo o Accesorio).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Console.WriteLine("IdTipoEquipo: "+ idTipoProducto);
            Producto nuevoProducto = new Producto
            {
                CodigoInterno = txtCodigoInterno.Text,
                CodigoSena = txtPlacaSena.Text,
                Serial = txtSerial.Text,
                NombreProducto = txtNombreProducto.Text,
                Marca = txtNombreMarca.Text,
                Descripcion = txtDescripcion.Text,
                Estado = cbDisponible.Text,
                IdTipoProducto = idTipoProducto
            };

            // Llamar al servicio para registrar el producto
            string resultadoProducto = productoService.RegistrarProducto(nuevoProducto);
            MessageBox.Show(resultadoProducto, "Registro de Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Si el registro fue exitoso, recargar datos y limpiar campos
            if (resultadoProducto.Contains("exitoso"))
            {
                // Recargar los datos en el formulario principal
                Principal principalForm = (Principal)Application.OpenForms["Principal"];
                if (principalForm != null)
                {
                    principalForm.CargarDatosDesdeBaseDeDatos();
                }
                LimpiarCampos();

            }
        }

        private void RadioButton_Accesorio_CheckedChanged_1(object sender, EventArgs e)
        {
            // Habilitar los campos necesarios para "Accesorio"
            txtCodigoInterno.Enabled = true;
            txtNombreProducto.Enabled = true;
            cbDisponible.Enabled = true;
            txtDescripcion.Enabled = true;
            // Deshabilitar los demás campos
            txtSerial.Enabled = false;
            txtPlacaSena.Enabled = false;
            txtNombreMarca.Enabled = false;

        }

        private void RadioButton_Equipo_CheckedChanged(object sender, EventArgs e)
        {
            txtCodigoInterno.Enabled = true;
            txtNombreProducto.Enabled = true;
            cbDisponible.Enabled = true;
            txtDescripcion.Enabled = true;
            txtSerial.Enabled = true;
            txtPlacaSena.Enabled = true;
            txtNombreMarca.Enabled = true;
        }

        private void txtCodigoInterno_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

