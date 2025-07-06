using AcaFinal.Models.PagoElectronico;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AcaFinal.Pages
{
    public class PagoElectronicoModel : PageModel
    {
        [BindProperty]
        public decimal Monto { get; set; }

        [BindProperty]
        public string Metodo { get; set; }

        public decimal? TotalCalculado { get; set; }
        public string Mensaje { get; set; }

        public void OnPost()
        {
            if (Monto <= 0)
            {
                Mensaje = "El monto debe ser mayor a cero.";
                return;
            }

            Pago pago = Metodo switch
            {
                "tarjeta" => new PagoTarjeta(Monto),
                "efectivo" => new PagoEfectivo(Monto),
                _ => null
            };

            if (pago == null)
            {
                Mensaje = "Debe seleccionar un método de pago.";
                return;
            }

            TotalCalculado = pago.ProcesarPago();
        }
    }

    

   

  
}