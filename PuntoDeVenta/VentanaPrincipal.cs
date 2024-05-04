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
using EntidadFact;

namespace PuntoDeVenta
{
    public partial class VentanaPrincipal : Form
    {

        PuenteConexion pc = new PuenteConexion();

        private DataTable dt;

        private double subtotal = 0;
        private double total = 0;

        private double descuento = 0;

        public VentanaPrincipal()
        {
            InitializeComponent();

            txtImpVenta.Text = (double.Parse(txtImpVentaPestaña.Text) * 100).ToString();
            txtDescuento.Text = txtDescuentoPestaña.Text;

            this.LlenarTabla();

            txtNFasctura.Text = pc.ConsultarFactura();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide(); //Esconder la ventana principal

            FormUsuarios vuser = new FormUsuarios();
            vuser.ShowDialog(); //Abrir la ventana principal 

            this.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void inventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide(); //Esconder la ventana principal

            Inventario vuser = new Inventario();
            vuser.ShowDialog(); //Abrir la ventana principal 

            this.Show();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox2_TextChanged(object sender, EventArgs e)
        {
            txtImpVenta.Text = (double.Parse(txtImpVentaPestaña.Text) * 100).ToString();
        }

        
        private void txtDescuentoPestaña_TextChanged_1(object sender, EventArgs e)
        {
            txtDescuento.Text = txtDescuentoPestaña.Text;
        }

        private void btnAddProducto_Click(object sender, EventArgs e)
        {
            var result = pc.ConsultarProductoPrecio(txtCodigoProducto.Text);

            DataRow fila = dt.NewRow();

            fila["Codigo"] = txtNFasctura.Text;
            fila["Producto"] = result.Item1; //Columna Producto
            fila["Precio x Unidad"] = result.Item2; //Columna PrecioUnidad
            fila["Cantidad"] = txtCantidad.Text;
            fila["Descuento"] = txtDescuento.Text;
            fila["Precio Total"] = Convert.ToInt32(txtCantidad.Text) * Convert.ToInt32(result.Item2); //Campo calculado

            dt.Rows.Add(fila);

            //Sumando el subtotal y el total

            subtotal = subtotal + (Convert.ToInt32(txtCantidad.Text) * Convert.ToInt32(result.Item2));
            
            if(descuento == 0)
            {
                total = subtotal + (subtotal * double.Parse(txtImpVentaPestaña.Text));
            }
            else
            {
                total = subtotal + (subtotal * double.Parse(txtImpVentaPestaña.Text));
                total = total - (total * (descuento / 100));
            }

            lblSubTotal.Text = subtotal.ToString();
            lblTotal.Text = total.ToString();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var registro = pc.ConsultarDescuentoCliente(txtCodigoCliente.Text);

            txtCliente.Text = registro.Item1;
            txtDescuento.Text = registro.Item2.ToString();

            descuento = registro.Item2;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            List<Facturacion> listFac = new List<Facturacion>();
            try
            {

                foreach (DataRow fila in dt.Rows)
                {
                    Facturacion fa = new Facturacion();

                    fa.Codigo = txtNFasctura.Text;
                    fa.Producto = fila["Producto"].ToString();
                    fa.PrecioUnidad = fila["Precio x Unidad"].ToString();
                    fa.Cantidad = fila["Cantidad"].ToString();
                    fa.Cliente = txtCodigoCliente.Text;
                    fa.DescuentoCliente = fila["Descuento"].ToString();
                    fa.PrecioTotal = fila["Precio Total"].ToString();

                    listFac.Add(fa);
                }

                pc.insertarFactura(listFac);

                txtNFasctura.Text = pc.ConsultarFactura(); //Actualizamos el numero de factura

                MessageBox.Show("Factura agregada");

                //Actualizar el datagriedview

                this.LlenarTabla();

            }
            catch(Exception er)
            {
                MessageBox.Show("Error: " + er);
            }
        }

        public void LlenarTabla()
        {
            dt = new DataTable();

            dt.Columns.Add("Codigo");
            dt.Columns.Add("Producto");
            dt.Columns.Add("Precio x Unidad");
            dt.Columns.Add("Cantidad");
            dt.Columns.Add("Descuento");
            dt.Columns.Add("Precio Total");

            dataGridView1.DataSource = dt;
        }
    }
}
