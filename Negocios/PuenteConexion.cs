using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Datos;
using System.Data;
using System.Data.SqlClient;
using EntidadFact;

namespace Negocios
{
    public class PuenteConexion
    {
        ConexionSql conect = new ConexionSql(); //Creamos el objeto de la clase para hacer la conexion

        public int consulta (string user, string pass)
        {
            return conect.consultaLogin(user, pass);
        }

        //Usuario

        public DataTable ConsultaUsuarios()
        {
            return conect.ConsultaUsuariosDG();
        }

        public int AgregarUsuario(string name, string lastname, string cedula, string phone, string user, string passw)
        {
            return conect.AddUser(name, lastname, cedula, phone, user, passw);
        }

        public int ModificaUsuario(string name, string lastname, string cedula, string phone, string user, string passw)
        {
            return conect.UpdateUser(name, lastname, cedula, phone, user, passw);
        }

        public int EliminarUsuario(string user)
        {
            return conect.DeleteUser(user);
        }

        //Inventario

        public DataTable ConsultaInventario()
        {
            return conect.ConsultaInventarioDG();
        }

        public int AgregarProducto(string name, string categoria, float precio, int cantidad, int codigoProducto)
        {
            return conect.AddProduct(name, categoria, precio, cantidad, codigoProducto);
        }

        public int ModificaProducto(string name, string categoria, float precio, int cantidad, int codigoProducto)
        {
            return conect.UpdateProduct(name, categoria, precio, cantidad, codigoProducto);
        }

        public int EliminaProducto(string preducto)
        {
            return conect.DeleteProduct(preducto);
        }

        //Facturacion

        public string ConsultarFactura()
        {
            return conect.ConsultFactura();
        }

        public Tuple<string,string> ConsultarProductoPrecio(string codigo)
        {
            return conect.ConsultProductPrice(codigo);
        }

        public Tuple<string, double> ConsultarDescuentoCliente(string codigo)
        {
            return conect.ConsultDiscountClient(codigo);
        }

        public void insertarFactura(List<Facturacion> faq)
        {
            conect.InsertFactura(faq);
        }
    }
}
