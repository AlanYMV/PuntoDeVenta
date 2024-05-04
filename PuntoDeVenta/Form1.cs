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
    public partial class Form1 : Form
    {
        PuenteConexion conect = new PuenteConexion();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {

            //Login

            if (conect.consulta(txtUser.Text, txtPassword.Text) == 1)
            {
                MessageBox.Show("Inicio de sesion\nCorrecta");

                this.Hide(); //Esconder la ventana principal

                VentanaPrincipal v1 = new VentanaPrincipal();
                v1.Show(); //Abrir la ventana principal 
            }
            else
            {
                MessageBox.Show("No se encontro usuario\n:c");
            }
        }
    }
}
