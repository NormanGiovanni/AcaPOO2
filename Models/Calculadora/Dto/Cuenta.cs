using AcaFinal.Models.Calculadora.Entidad;

namespace AcaFinal.Models.Calculadora.Dto
{
    public class Cuenta : Clientes
    {
        public decimal SubTotal { get; set; }
        public decimal Propina { get; set; }
        public decimal Total { get; set; }
        public List<DetalleCuenta> Detalles { get; set; }
    }
}
