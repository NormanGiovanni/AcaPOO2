namespace AcaFinal.Models.PagoElectronico
{
    public class PagoTarjeta : Pago
    {
        private const decimal Comision = 0.03m;
        public PagoTarjeta(decimal monto) : base(monto) { }
        public override decimal ProcesarPago() => Monto + (Monto * Comision);
    }
}
