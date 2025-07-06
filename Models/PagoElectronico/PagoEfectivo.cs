namespace AcaFinal.Models.PagoElectronico
{
    public class PagoEfectivo : Pago
    {
        private const decimal Descuento = 0.05m;
        public PagoEfectivo(decimal monto) : base(monto) { }
        public override decimal ProcesarPago() => Monto - (Monto * Descuento);
    }
}
