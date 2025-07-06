using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AcaFinal.Models.Calculadora.Dto;
using AcaFinal.Interfaces.Calculadora;
using System.Text.Json; 

namespace AcaFinal.Pages
{
    //Interfaz de la p�gina Razor
    public class CalculadoraModel : PageModel
    {
        //Interface del servicio
        private readonly ICalculadora _calculadoraService;
        //Se inicializa la clase y se injecta el servicio
        public CalculadoraModel(ICalculadora calculadoraService)
        {
            _calculadoraService = calculadoraService;
        }
        // Propiedades de la p�gina Razor para la cuenta
        [BindProperty]
        public Cuenta Cuenta { get; set; } = new Cuenta
        {
            Detalles = new List<DetalleCuenta>()
        };
        // Propiedades de la p�gina Razor para almacenar el nuevo producto
        [BindProperty]
        public DetalleCuenta NuevoProducto { get; set; } = new DetalleCuenta();
        // Propiedades de la p�gina Razor para almacenar el porcentaje de la propina
        [BindProperty]
        public decimal PorcentajePropina { get; set; } = 10;
        // Propiedad para indicar si la liquidaci�n ha sido generada campo bandera
        public bool LiquidacionGenerada { get; set; } = false;

        //Guardar cuenta para la persistencia
        private void GuardarCuentaEnTempData()
        {
            TempData["Cuenta"] = JsonSerializer.Serialize(Cuenta);
        }
        //Guardar la persistencia de los datos
        private void RecuperarCuentaDeTempData()
        {
            if (TempData["Cuenta"] != null)
            {
                Cuenta = JsonSerializer.Deserialize<Cuenta>(TempData["Cuenta"].ToString());
            }
        }
        // M�todo que procesa el agregar un producto al front
        public IActionResult OnPostAgregarProducto()
        {
            //Trae los datos de nuestra temporal
            RecuperarCuentaDeTempData();

            //Verfica que detalles no este null y si cumple instancia la clase para agregar un nuevo producto 
            //y si no cumple adiciona el producto a la lista de detalles
            if (Cuenta.Detalles != null)
                Cuenta.Detalles.Add(NuevoProducto);
            else
                Cuenta.Detalles = new() { NuevoProducto };
            //Almacena la cuenta en TempData para persistencia
            GuardarCuentaEnTempData();

            // Reiniciar el producto para el siguiente ingreso
            NuevoProducto = new DetalleCuenta();
            //retorna la p�gina actual
            return Page();
        }

        public IActionResult OnPostEliminarProducto(string id)
        {
            // Recupera la cuenta de TempData para trabajar con los datos actuales
            RecuperarCuentaDeTempData();
            // Busca el producto a eliminar por su c�digo
            var producto = Cuenta.Detalles.FirstOrDefault(p => p.Codigo == id);
            if (producto != null)
            {
                Cuenta.Detalles.Remove(producto);
            }
            //Almacena la cuenta en TempData para persistencia
            GuardarCuentaEnTempData();
            //retorna la p�gina actual
            return Page();
        }

        public async Task<IActionResult> OnPostCalcular()
        {
            // Recupera la cuenta de TempData para trabajar con los datos actuales
            RecuperarCuentaDeTempData();
            // toma los datos de nuestro servicio de calcular operacion
            Cuenta = await _calculadoraService.CalcularPropinaAsync(Cuenta, PorcentajePropina);
            //marca la cuenta como liquidaci�n generada
            LiquidacionGenerada = true;
            GuardarCuentaEnTempData();
            //retorna la p�gina actual
            return Page();
        }
    }
}
