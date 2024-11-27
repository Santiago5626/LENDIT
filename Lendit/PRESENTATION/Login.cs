using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using ENTITY;
using bll;

namespace PRESENTATION
{
    public partial class Login : Form
    {
        private readonly UsuarioService serviceUsuario;

        // Variables para arrastrar el formulario
        private bool dragging = false;
        private Point startPoint = new Point(0, 0);

        public Login()
        {
            InitializeComponent();
            Diseños();
            serviceUsuario = new UsuarioService();

        }
        private void Diseños()
        {
            this.MouseDown += new MouseEventHandler(Form_MouseDown);
            this.MouseMove += new MouseEventHandler(Form_MouseMove);
            this.MouseUp += new MouseEventHandler(Form_MouseUp);
            guna2HtmlLabel1.ForeColor = ColorTranslator.FromHtml("#00304D");
            guna2HtmlLabel1.Font = new Font("Work Sans", 32F, FontStyle.Bold);
            passwordTxt.PasswordChar = '•';
        }

        private void Button_iniciar_Click(object sender, EventArgs e)
        {
            string resultadoLogin = serviceUsuario.Login(usuarioTxt.Text, passwordTxt.Text);

            if (resultadoLogin == "Administrador")
            {
                MessageBox.Show("Bienvenido Administrador");
                Principal principal = new Principal();
                Principal_Inventario inventario = new Principal_Inventario();
                Principal_Encargado encargado = new Principal_Encargado();
                Principal_Circulacion circulacion = new Principal_Circulacion();
                AddOwnedForm(principal);
                AddOwnedForm(inventario);
                AddOwnedForm(encargado);
                AddOwnedForm(circulacion);
                string nombreUsuario = serviceUsuario.BuscarNombreUsuario(usuarioTxt.Text);
                
                Sesion.nombre = nombreUsuario;

                principal.Show();
                this.Hide();
            }
            else if (resultadoLogin == "Gestor")
            {
                MessageBox.Show("Bienvenido Encargado");
                Principal_Encargado principal2 = new Principal_Encargado();

                string nombreUsuario = serviceUsuario.BuscarNombreUsuario(usuarioTxt.Text);

                Sesion.nombre = nombreUsuario;
                principal2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show(resultadoLogin);
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

        // Métodos para arrastrar el formulario
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

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }
    }
}

