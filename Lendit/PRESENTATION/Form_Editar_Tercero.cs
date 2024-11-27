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
    public partial class Form_Editar_Tercero : Form
    {
        private readonly TerceroService terceroService;
        private readonly UsuarioService usuarioService;
        private readonly CodigoProgramaService codigoProgramaService;
        private readonly SolicitanteService solicitanteService;
        private readonly RolSolicitanteService rolSolicitanteService;
        private readonly FichaService fichaService;
        public Form_Editar_Tercero()
        {
            InitializeComponent();
            terceroService = new TerceroService();
            usuarioService = new UsuarioService();
            codigoProgramaService = new CodigoProgramaService();
            solicitanteService = new SolicitanteService();
            rolSolicitanteService = new RolSolicitanteService();
            fichaService = new FichaService();  
            btnEditar.Enabled = false; 

        }


        private void LlenarComboBoxProgramas(Solicitante solicitante)
        {
            if (solicitante == null)
            {
                LlenarComboBoxProgramas();
                LlenarComboBoxFicha();
                return;
            }
            // Obtener la lista de programas desde el servicio
            List<Tuple<string, string>> programas = codigoProgramaService.ObtenerProgramas();

            cbCodigoPrograma.Items.Clear();

            // Llenar el ComboBox con los programas disponibles
            foreach (var programa in programas)
            {
                // Crear un objeto anónimo para almacenar el código del programa y su nombre
                cbCodigoPrograma.Items.Add(new { Text = programa.Item2, Value = programa.Item1 });
            }

            // Establecer las propiedades DisplayMember y ValueMember
            cbCodigoPrograma.DisplayMember = "Text";
            cbCodigoPrograma.ValueMember = "Value";

            // Verificar que el ComboBox tiene elementos antes de intentar seleccionar uno
            if (cbCodigoPrograma.Items.Count > 0)
            {
                // Buscar el programa correspondiente en base al CodPrograma del solicitante
                var itemSeleccionado = cbCodigoPrograma.Items.Cast<dynamic>()
                    .FirstOrDefault(item => item.Value == solicitante.CodPrograma);

                // Si se encontró el programa correspondiente, seleccionarlo
                if (itemSeleccionado != null)
                {
                    cbCodigoPrograma.SelectedItem = itemSeleccionado;
                }
            }
        }

        private void LlenarComboBoxFichas(Solicitante solicitante)
        {

            if (solicitante == null)
            {
                LlenarComboBoxProgramas();
                LlenarComboBoxFicha();
                return;
            }
            // Obtener la lista de fichas disponibles (deberías tener un servicio para obtenerlas)
            List<Tuple<string, string>> fichas = fichaService.ObtenerFichasBuscar();

            cbFicha.Items.Clear();

            // Llenar el ComboBox con las fichas disponibles
            foreach (var ficha in fichas)
            {
                cbFicha.Items.Add(new { Text = ficha.Item2, Value = ficha.Item1 });
            }

            // Establecer las propiedades DisplayMember y ValueMember
            cbFicha.DisplayMember = "Text";
            cbFicha.ValueMember = "Value";

            // Verificar que el ComboBox tiene elementos antes de intentar seleccionar uno
            if (cbFicha.Items.Count > 0)
            {
                // Buscar la ficha correspondiente en base al CodFicha del solicitante
                var itemSeleccionado = cbFicha.Items.Cast<dynamic>()
                    .FirstOrDefault(item => item.Value == solicitante.CodFicha);

                // Si se encontró la ficha correspondiente, seleccionarla
                if (itemSeleccionado != null)
                {
                    cbFicha.SelectedItem = itemSeleccionado;
                }
            }
        }

        private void LlenarComboBoxRolesSolicitante(Tercero tercero)
        {
         

            List<Tuple<int, string>> rolesSolicitante = rolSolicitanteService.ObtenerRolesSolicitante();

            if (tercero == null)
            {
                MessageBox.Show("Tercero no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cbRolSolicitante.Items.Clear();

            foreach (var rol in rolesSolicitante)
            {
                cbRolSolicitante.Items.Add(new { Text = rol.Item2, Value = rol.Item1 });
            }

            cbRolSolicitante.DisplayMember = "Text";
            cbRolSolicitante.ValueMember = "Value";
      
            if (cbRolSolicitante.Items.Count > 0)
            {
               
                var itemSeleccionado = cbRolSolicitante.Items.Cast<dynamic>()
                    .FirstOrDefault(item => item.Value == tercero.IdRol);

          
                if (itemSeleccionado != null)
                {
                    cbRolSolicitante.SelectedItem = itemSeleccionado;
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Obtener valores de los ComboBoxes
            var selectedItemRol = cbRolSolicitante.SelectedItem as dynamic;
            int rolSolicitanteId = selectedItemRol?.Value;

            Tercero tercero = new Tercero
            {
                Identificacion = txtIdentificacion.Text,
                PrimerNombre = txtPrimerNombre.Text,
                SegundoNombre = txtSegundoNombre.Text,
                PrimerApellido = txtPrimerApellido.Text,
                SegundoApellido = txtSegundoApellido.Text,
                Correo = txtCorreo.Text,
                Telefono = txtTelefono.Text,
                Genero = cbGenero.Text == "Masculino" ? "M" : "F",
                Activo = cbEstado.Text == "Si" ? "S" : "N",
                IdRol = rolSolicitanteId,
            };

            // Editar Tercero
            MessageBox.Show(terceroService.EditarTercero(tercero));

           

            var selectedItemFicha = cbFicha.SelectedItem as dynamic;
            string fichaSolicitanteId = selectedItemFicha?.Key;

            var programaSeleccionado = cbCodigoPrograma.SelectedItem;
            string codPrograma = (programaSeleccionado as dynamic)?.Value;

            // Verificar si el rol es "Aprendiz"
            string rolSolicitante = selectedItemRol?.Text;
            if (rolSolicitante == "Aprendiz")
            {
                // Comprobar si el solicitante existe
                Solicitante solicitanteExistente = solicitanteService.BuscarSolicitantePorId(txtIdentificacion.Text);

                if (solicitanteExistente != null)
                {
                    Console.WriteLine(solicitanteExistente.ToString() + " entro"); 
                    // Si el solicitante existe, editarlo
                    Solicitante solicitante = new Solicitante
                    {
                        Identificacion = txtIdentificacion.Text,
                     
                        CodFicha = fichaSolicitanteId,
                        CodPrograma = codPrograma
                    };

                    MessageBox.Show(solicitanteService.EditarSolicitante(solicitante));
                }
                else
                {
                    // Si el solicitante no existe, crear uno nuevo
                    string fichaId = null;
                    if (cbFicha.SelectedItem != null)
                    {
                        fichaId = selectedItemFicha?.Value;
                    }

                    Solicitante nuevoSolicitante = new Solicitante
                    {
                        Identificacion = txtIdentificacion.Text,
                        CodPrograma = codPrograma,
                        CodFicha = fichaId
                    };

                    string msgSolicitante = solicitanteService.RegistrarSolicitante(nuevoSolicitante);
                    MessageBox.Show(msgSolicitante, "Registro de Solicitante", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refrescar los datos de la vista
                    Principal principalForm = (Principal)Application.OpenForms["Principal"];
                    if (principalForm != null)
                    {
                        principalForm.CargarDatosDesdeBaseDeDatos();
                    }
                }
            }
        }


        private void LimpiarCampos()
        {
            txtPrimerNombre.Text = "";
            txtSegundoNombre.Text = "";
            txtPrimerApellido.Text = "";
            txtSegundoApellido.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";
            cbGenero.SelectedIndex = -1;
            cbCodigoPrograma.SelectedIndex = -1;
            cbFicha.SelectedIndex = -1;

        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string identificacion = txtIdentificacion.Text;

            // Buscar en Tercero
            Tercero tercero = terceroService.BuscarTerceroPorIdentificacion(identificacion);
           
            if (tercero != null)
            {
                // Llenar los campos con los datos del tercero encontrado
                txtPrimerNombre.Text = tercero.PrimerNombre;
                txtSegundoNombre.Text = tercero.SegundoNombre;
                txtPrimerApellido.Text = tercero.PrimerApellido;
                txtSegundoApellido.Text = tercero.SegundoApellido;
                txtCorreo.Text = tercero.Correo;
                txtTelefono.Text = tercero.Telefono;
                cbGenero.Text = tercero.Genero == "M" ? "Masculino" : "Femenino";
                txtIdentificacion.ReadOnly = true;
                cbEstado.Text = tercero.Activo == "S" ? "Si" : "No";

                Solicitante solicitante = solicitanteService.BuscarSolicitantePorId(identificacion);
                LlenarComboBoxRolesSolicitante(tercero);
                LlenarComboBoxProgramas(solicitante);
                LlenarComboBoxFichas(solicitante);
                if (tercero.IdRol == 2)  // Asumimos que 2 es el IdRol para empleados
                {
                    // Limpiar ComboBox de Programa y Ficha
                    cbCodigoPrograma.SelectedIndex = -1; // Limpia la selección
                    cbFicha.SelectedIndex = -1;    // Limpia la selección

                    // Deshabilitar los ComboBox
                    cbCodigoPrograma.Enabled = false;
                    cbFicha.Enabled = false;
                }
                btnEditar.Enabled = true;
            }
            else
            {
                MessageBox.Show("Tercero no encontrado.", "Buscar Tercero", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
            }
        }



        private void SetComboBoxSelectedValueByName(ComboBox comboBox, string programName)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                var item = (KeyValuePair<string, string>)comboBox.Items[i];
                if (item.Value == programName)
                {
                    comboBox.SelectedIndex = i;
                    return;
                }
            }
            comboBox.SelectedIndex = -1; // Si no se encuentra el nombre, deseleccionar
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
        private void cbRolSolicitante_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = cbRolSolicitante.SelectedItem as dynamic;
            string rolSolicitante = selectedItem?.Text;  // Accede al texto del rol

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

        private void cbCodigoPrograma_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }






        /*
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

private void cbRolSolicitante_SelectedIndexChanged(object sender, EventArgs e)
{
   var selectedItem = cbRolSolicitante.SelectedItem as dynamic;
   string rolSolicitante = selectedItem?.Text;  // Accede al texto del rol

   // Comprobar si el rol es "Aprendiz"
   if (rolSolicitante == "Aprendiz")
   {
       // Habilitar el ComboBox de programa
       cbCodigoPrograma.Enabled = true;

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
   }
}*/
    }

}