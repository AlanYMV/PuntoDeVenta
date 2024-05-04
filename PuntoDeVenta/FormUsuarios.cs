using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Negocios;

namespace PuntoDeVenta
{
    public partial class FormUsuarios : Form
    {
        PuenteConexion pc = new PuenteConexion();

        public FormUsuarios()
        {
            InitializeComponent();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormUsuarios_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = pc.ConsultaUsuarios();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            pc.AgregarUsuario(txtNombre.Text, txtApellido.Text, txtCedula.Text, txtTelefono.Text, txtUsuario.Text, txtContrasena.Text);
            dataGridView1.DataSource = pc.ConsultaUsuarios();
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            pc.ModificaUsuario(txtNombre.Text, txtApellido.Text, txtCedula.Text, txtTelefono.Text, txtUsuario.Text, txtContrasena.Text);
            dataGridView1.DataSource = pc.ConsultaUsuarios();
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            pc.EliminarUsuario(txtUsuario.Text);
            dataGridView1.DataSource = pc.ConsultaUsuarios();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
