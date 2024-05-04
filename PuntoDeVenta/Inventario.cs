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
    public partial class Inventario : Form
    {

        PuenteConexion pc = new PuenteConexion();

        public Inventario()
        {
            InitializeComponent();
            dataGridView1.DataSource = pc.ConsultaInventario();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            pc.AgregarProducto(txtProduct.Text,txtCategory.Text,(float)Convert.ToDouble(txtPrice.Text),Convert.ToInt32(txtAmount.Text),Convert.ToInt32(txtCodigoProducto.Text));

            dataGridView1.DataSource = pc.ConsultaInventario();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            pc.ModificaProducto(txtProduct.Text, txtCategory.Text, (float)Convert.ToDouble(txtPrice.Text), Convert.ToInt32(txtAmount.Text),Convert.ToInt32(txtCodigoProducto.Text));

            dataGridView1.DataSource = pc.ConsultaInventario();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            pc.EliminaProducto(txtProduct.Text);

            dataGridView1.DataSource = pc.ConsultaInventario();
        }
    }
}
