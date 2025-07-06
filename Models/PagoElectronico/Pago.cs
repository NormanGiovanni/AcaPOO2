namespace AcaFinal.Models.PagoElectronico
{
    public abstract class Pago
    {
        private decimal monto;
        public decimal Monto
        {
            get => monto;
            set => monto = value > 0 ? value : throw new ArgumentException("El monto debe ser positivo.");
        }
        public Pago(decimal monto) { Monto = monto; }
        public abstract decimal ProcesarPago();
    }
}
