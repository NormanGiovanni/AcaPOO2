using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AcaFinal.Pages
{
    //Interfaz de la p�gina Razor
    public class CalificacionModel : PageModel
    {
        // decorador para tener bidirecci�n entre la vista y el modelo
        [BindProperty]
        // Propiedad para almacenar la calificaci�n ingresada por el usuario
        public decimal Calificacion { get; set; }
        // Propiedad para almacenar el mensaje de resultado
        public string Mensaje { get; set; }
        // M�todo que se ejecuta al enviar el formulario
        public void OnPost()
        {
            // Verifica si la calificaci�n est� fuera del rango permitido
            if (Calificacion < 0 || Calificacion > 5)
            {
                Mensaje = "La calificaci�n debe estar entre 0 y 5.";
                return;
            }
            // Utiliza una expresi�n switch para determinar el mensaje basado en la calificaci�n
            Mensaje = Calificacion switch
            {
                >= 4.6m and <= 5.0m => "Excelente",
                >= 4.0m and < 4.6m => "Bueno",
                >= 3.0m and < 4.0m => "Regular",
                < 3.0m => "Deficiente",
                _ => "Valor no reconocido"
            };
        }
    }
}