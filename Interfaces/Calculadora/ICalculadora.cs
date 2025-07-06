using AcaFinal.Models.Calculadora.Dto;

namespace AcaFinal.Interfaces.Calculadora
{
    public interface ICalculadora
    {
        Task<Cuenta> CalcularPropinaAsync(Cuenta cuenta, decimal porcentajePropina);
    }
}

