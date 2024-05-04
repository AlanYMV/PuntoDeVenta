using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadFact
{
    public class Facturacion
    {
        private string codigo = "";
        private string producto = "";
        private string precioUnidad = "";
        private string cantidad = "";
        private string cliente = "";
        private string descuentoCliente = "";
        private string precioTotal = "";

        public string Codigo { get => codigo; set => codigo = value; }
        public string Producto { get => producto; set => producto = value; }
        public string PrecioUnidad { get => precioUnidad; set => precioUnidad = value; }
        public string Cantidad { get => cantidad; set => cantidad = value; }
        public string Cliente { get => cliente; set => cliente = value; }
        public string DescuentoCliente { get => descuentoCliente; set => descuentoCliente = value; }
        public string PrecioTotal { get => precioTotal; set => precioTotal = value; }
    }
}
