using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AcaFinal.Interfaces.Login;

namespace AcaFinal.Pages
{
    // Interfaz de la p�gina Razor para el login
    public class LoginModel : PageModel
    {
        // Interface del servicio de login
        private readonly ILogin _loginService;
        // Clave para almacenar los intentos en TempData
        private const string TempDataKey = "LoginIntentos";
        // N�mero m�ximo de intentos permitidos
        private const int MaxIntentos = 3;
        // Constructor que inyecta el servicio de login
        public LoginModel(ILogin loginService)
        {
            _loginService = loginService;
        }
        // Decorador para tener bidirecci�n entre la vista y el modelo
        [BindProperty]
        // Propiedad para almacenar la contrase�a ingresada por el usuario
        public string Password { get; set; }
        // Propiedad para almacenar el mensaje de resultado
        public string Mensaje { get; set; }
        // M�todo para obtener el n�mero de intentos desde TempData
        private int ObtenerIntentos()
        {
            if (TempData[TempDataKey] != null && int.TryParse(TempData[TempDataKey].ToString(), out int intentos))
                return intentos;
            return 0;
        }
        // M�todo para guardar el n�mero de intentos en TempData
        private void GuardarIntentos(int intentos)
        {
            TempData[TempDataKey] = intentos.ToString();
        }
        // M�todo que se ejecuta al cargar la p�gina
        public void OnGet()
        {
            GuardarIntentos(0); // Reinicia los intentos al cargar la p�gina
        }
        // M�todo que se ejecuta al enviar el formulario de login
        public IActionResult OnPost()
        {
            // Verifica la cantidad de intentos almacenados en TempData
            int intentos = ObtenerIntentos();

            // Verifica si la cuenta est� bloqueada
            if (intentos >= MaxIntentos)
            {
                Mensaje = "Cuenta bloqueada.";
                GuardarIntentos(intentos); // Mantener bloqueado
                return Page();
            }
            // Verifica si la contrase�a es v�lida utilizando el servicio de login
            if (_loginService.ValidarPassword(Password))
            {
                Mensaje = "Acceso concedido.";
                GuardarIntentos(0); // Reinicia los intentos al acceder correctamente
            }
            else
            {
                // Si la contrase�a es incorrecta, incrementa el contador de intentos
                intentos++;
             
                if (intentos >= MaxIntentos)
                    // Actualiza el mensaje seg�n el n�mero de intentos restantes
                    Mensaje = "Cuenta bloqueada.";
                else
                    // Si la contrase�a es incorrecta, muestra el n�mero de intentos restantes
                    Mensaje = $"Contrase�a incorrecta. Intentos restantes: {MaxIntentos - intentos}";
                // Registra el intento fallido en el servicio de login
                GuardarIntentos(intentos);
            }
            // Retorna la p�gina actual con el mensaje actualizado
            return Page();
        }
    }
}