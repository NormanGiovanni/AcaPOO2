using AcaFinal.Interfaces.Login;

namespace AcaFinal.Servicios.Login
{
    public class ServicesLogin : ILogin
    {
        // Password correcta para el acceso
        private const string PasswordCorrecta = "12345";
        // Contador de intentos fallidos
        private int _intentos = 0;
        // Número máximo de intentos permitidos
        private const int MaxIntentos = 3;
        // Propiedad para obtener el número máximo de intentos
        public int IntentosRestantes => MaxIntentos - _intentos;
        // Propiedad para verificar si la cuenta está bloqueada
        public bool CuentaBloqueada => _intentos >= MaxIntentos;
        // Constructor que inicializa el servicio de login
        public bool ValidarPassword(string password)
        {
            return password == PasswordCorrecta;
        }
        // Método para registrar un intento de acceso
        public void RegistrarIntento(bool exito)
        {
            if (!exito)
                _intentos++;
        }
        // Método para reiniciar el contador de intentos
        public void ReiniciarIntentos()
        {
            _intentos = 0;
        }
        // Método para establecer el número de intentos restantes
        public void SetIntentosRestantes(int intentos)
        {
            _intentos = MaxIntentos - intentos;
            if (_intentos < 0) _intentos = 0;
            if (_intentos > MaxIntentos) _intentos = MaxIntentos;
        }
    }
}
