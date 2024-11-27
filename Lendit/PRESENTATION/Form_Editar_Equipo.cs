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
    public partial class Form_Editar_Equipo : Form
    {
        private readonly ProductoService productoService;
        private readonly TipoProductoService tipoProductoService;
        public Form_Editar_Equipo()
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Crear un objeto Producto con los valores del formulario
            Producto producto = new Producto
            {
                CodigoInterno = txtCodigoInterno.Text,
                CodigoSena = txtPlacaSena.Text,
                Serial = txtSerial.Text,
                NombreProducto = txtNombreProducto.Text,
                Marca = txtNombreMarca.Text,
                Descripcion = txtDescripcion.Text,
                Estado = cbDisponible.Text == "Disponible" ? "Disponible" : "No Disponible",
                IdTipoProducto = RadioButton_Equipo.Checked ? 1 : (RadioButton_Accesorio.Checked ? 2 : 0) // Determina el tipo de producto
            };

            // Validar que se haya seleccionado un tipo de producto válido
            if (producto.IdTipoProducto == 0)
            {
                MessageBox.Show("Por favor, seleccione un tipo de producto (Equipo o Accesorio).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Llamar al servicio para editar el producto
            string resultadoEdicion = productoService.EditarProducto(producto);

            // Mostrar el resultado de la operación
            MessageBox.Show(resultadoEdicion, "Resultado de la Edición", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarCampos();
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            
            string codigoInterno = txtCodigoInterno.Text;

            
            Producto producto = productoService.BuscarProductoPorCodigoInterno(codigoInterno);

            if (producto != null)
            {
                
                txtCodigoInterno.Text = producto.CodigoInterno;
                txtPlacaSena.Text = producto.CodigoSena;
                txtSerial.Text = producto.Serial;
                txtNombreProducto.Text = producto.NombreProducto;
                txtNombreMarca.Text = producto.Marca;
                txtDescripcion.Text = producto.Descripcion;
                cbDisponible.Text = producto.Estado == "Disponible" ? "Disponible" : "No disponible";

    
                if (producto.IdTipoProducto == 1)
                {
                    RadioButton_Equipo.Checked = true;
                }
                else if (producto.IdTipoProducto == 2)
                {
                    RadioButton_Accesorio.Checked = true;
                }
                
                txtCodigoInterno.ReadOnly = true;

            }
            else
            {
                
                MessageBox.Show("Producto no encontrado.", "Buscar Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
            }

        }
    }
}

