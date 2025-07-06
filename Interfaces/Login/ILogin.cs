namespace AcaFinal.Interfaces.Login
{
    public interface ILogin
    {
        bool ValidarPassword(string password);
        int IntentosRestantes { get; }
        void RegistrarIntento(bool exito);
        void ReiniciarIntentos();
        bool CuentaBloqueada { get; }
    }
}
