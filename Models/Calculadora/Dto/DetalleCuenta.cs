using AcaFinal.Models.Calculadora.Entidad;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AcaFinal.Models.Calculadora.Dto
{
    public class DetalleCuenta : Productos
    {
        public int Cantidad { get; set; }
        public decimal Subtotal => Cantidad * Precio;
        public DetalleCuenta()
        {
            Cantidad = 0;
            Precio = 0;
        }
        public DetalleCuenta(Productos producto, int cantidad)
        {
            Codigo = producto.Codigo;
            Nombre = producto.Nombre;
            Precio = producto.Precio;
            Cantidad = cantidad;
        }
    }
}
