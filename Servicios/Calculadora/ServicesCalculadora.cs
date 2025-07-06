using AcaFinal.Interfaces.Calculadora;
using AcaFinal.Models.Calculadora.Dto;

namespace AcaFinal.Servicios.Calculadora
{
    public class ServicesCalculadora : ICalculadora
    {
        public Task<Cuenta> CalcularPropinaAsync(Cuenta cuenta, decimal porcentajePropina)
        {

            cuenta.SubTotal = cuenta.Detalles.Sum(b => b.Subtotal);
            cuenta.Propina = Math.Round(cuenta.SubTotal * (porcentajePropina / 100), 2);
            cuenta.Total = Math.Round(cuenta.SubTotal + cuenta.Propina, 2);
            return Task.FromResult(cuenta);
        }
    }
}
