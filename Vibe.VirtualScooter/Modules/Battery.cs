namespace Vibe.VirtualScooter.Modules
{
    public class Battery
    {
        public Double Charge { get; private set; } = 100;
        public const Double MinimumCharge = 10;
        public Boolean IsCharged => Charge >= MinimumCharge;

        public void Discharge()
        {
            Charge -= 1;
        }

        public void Recharge() 
        {
            Charge = 100;
        }
    }
}
