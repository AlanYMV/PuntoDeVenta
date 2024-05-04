using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

using EntidadFact;

namespace Datos
{
     public class ConexionSql
    {
        static string conexionstr = "server = localhost; " +
                          "database = PuntoDeVenta; " +
                          "integrated security = true";

        SqlConnection conect = new SqlConnection(conexionstr);

        public int consultaLogin (string User, string Password)
        {
            int contador;

            conect.Open(); //Abrimos conexion

            string query = "select count(*) from Persona where Usuario = '" + User + 
                                                        "' and Contrasena = '" + Password + "'"; //Consulta que nos devuelve cuantos resultados encuenta

            SqlCommand comando = new SqlCommand(query, conect); //mandamos la consulta con la conexion al servidor y BD

            contador = Convert.ToInt32(comando.ExecuteScalar()); //Ejecutamos la consulta y nos devuelve cuantos usuario existen con ese usuario y contraseña con executeScalar()

            conect.Close(); //Cerramos conexion

            return contador;
        }

        //Usuarios

        public DataTable ConsultaUsuariosDG () //datagriedview
        {
            string query = "select * from Persona";
            SqlCommand command = new SqlCommand(query, conect);

            SqlDataAdapter data = new SqlDataAdapter(command);

            DataTable table = new DataTable();

            data.Fill(table);

            return table;
        }

        public int AddUser (string name, string lastname, string cedula, string phone, string user, string passw)
        {
            int nice = 0;

            conect.Open();

            string query = "insert into persona Nombre, Apellido, Cedula, telefono, Usuario, Contrasena values ('" + name + "','" + lastname + "','" + cedula + "','" + phone + "','" + user + "','" + passw + "')";
            SqlCommand command = new SqlCommand(query, conect);
            nice = command.ExecuteNonQuery();

            conect.Close();

            return nice;
        }

        public int UpdateUser (string name, string lastname, string cedula, string phone, string user, string passw)
        {
            int nice = 0;

            conect.Open();

            string query = "update Persona set Nombre = '" + name + "', Apellido = '" + lastname + "', Cedula = '" + cedula + "', telefono = '" + phone + "', Usuario = '" + user + "', Contrasena = '" + passw + "' where Usuario = '" + user + "'";
            SqlCommand command = new SqlCommand(query, conect);
            nice = command.ExecuteNonQuery();

            conect.Close();

            return nice;
        }

        public int DeleteUser (string user)
        {
            int nice = 0;

            conect.Open();

            string query = "delete from Persona where Usuario = '" + user + "'";
            SqlCommand command = new SqlCommand(query, conect);
            nice = command.ExecuteNonQuery();

            conect.Close();

            return nice;
        }

        //Inventario

        public DataTable ConsultaInventarioDG() //datagriedview
        {
            string query = "select * from Inventario";
            SqlCommand command = new SqlCommand(query, conect);

            SqlDataAdapter data = new SqlDataAdapter(command);

            DataTable table = new DataTable();

            data.Fill(table);

            return table;
        }

        public int AddProduct(string name, string category, float money, int amoung, int codeProduct)
        {
            int nice = 0;

            conect.Open();

            string query = "insert into Inventario (Producto, Categoria, Precio, Cantidad, CodigoProducto) values ('" + name + "','" + category + "','" + money + "','" + amoung + "', '" + codeProduct + "')";
            SqlCommand command = new SqlCommand(query, conect);
            nice = command.ExecuteNonQuery();

            conect.Close();

            return nice;
        }

        public int UpdateProduct(string name, string category, float money, int amoung, int codeProduct)
        {
            int nice = 0;

            conect.Open();

            string query = "update Inventario set Producto = '" + name + "', Categoria = '" + category + "', Precio = '" + money + "', Cantidad = '" + amoung + "', CodigoProducto = '" + codeProduct + "' where Producto = '" + name + "'";
            SqlCommand command = new SqlCommand(query, conect);
            nice = command.ExecuteNonQuery();

            conect.Close();

            return nice;
        }

        public int DeleteProduct(string product)
        {
            int nice = 0;

            conect.Open();

            string query = "delete from Inventario where Producto = '" + product + "'";
            SqlCommand command = new SqlCommand(query, conect);
            nice = command.ExecuteNonQuery();

            conect.Close();

            return nice;
        }

        // Factura

        public string ConsultFactura ()
        {
            string result = "Null";

            conect.Open();

            string query = "select top 1 (Codigo) + 1 as Codigo from Facturacion order by Codigo desc";
            SqlCommand command = new SqlCommand(query, conect);

            SqlDataReader registro = command.ExecuteReader();

            if (registro.Read()) //Si tiene algo el sqldatareader corre el if
            {
                result = registro["Codigo"].ToString();
            }
            
            conect.Close();

            return result;

        }

        public Tuple<string,string> ConsultProductPrice(string code)
        {
            string result1 = "Null", result2 = "Null";

            conect.Open();

            string query = "select * from Inventario where CodigoProducto = '" + code + "'";
            SqlCommand command = new SqlCommand(query, conect);

            SqlDataReader registro = command.ExecuteReader();

            if (registro.Read()) //Si tiene algo el sqldatareader corre el if
            {
                result1 = registro["Producto"].ToString();
                result2 = registro["Precio"].ToString();
            }

            conect.Close();

            return Tuple.Create(result1, result2);
        }

        public Tuple<string, double> ConsultDiscountClient(string code)
        {
            string result1 = "Null";
            double result2 = 0;

            conect.Open();

            string query = "select (Nombre + ' ' + Apellido) as Nombre, Descuento from Clientes where Codigo = '" + code + "'";
            SqlCommand command = new SqlCommand(query, conect);

            SqlDataReader registro = command.ExecuteReader();

            if (registro.Read()) //Si tiene algo el sqldatareader corre el if
            {
                result1 = registro["Nombre"].ToString();
                result2 = double.Parse((registro["Descuento"]).ToString());
            }

            conect.Close();

            return Tuple.Create(result1, result2);
        }

        public void InsertFactura(List<Facturacion> faq)
        {
            conect.Open();

            foreach(Facturacion facturacion in faq)
            {
                string query = "Insert into Facturacion (Codigo, Producto, PrecioUnidad, Cantidad, Cliente, DescuentoCliente, PrecioTotal) values ('" + facturacion.Codigo + "', '" + facturacion.Producto + "', '" + float.Parse(facturacion.PrecioUnidad) + "', '" + Convert.ToInt32(facturacion.Cantidad) + "', '" + facturacion.Cliente + "', '" + float.Parse(facturacion.DescuentoCliente) + "', '" + float.Parse(facturacion.PrecioUnidad) + "')";

                SqlCommand command = new SqlCommand(query,conect);

                command.ExecuteNonQuery();
                
            }

            conect.Close();
        }
    }
}
