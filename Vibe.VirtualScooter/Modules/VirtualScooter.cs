using Vibe.Tools.Result;

namespace Vibe.VirtualScooter.Modules
{
    public class VirtualScooter
    {
        private static VirtualScooter _instance;

        public Guid ScooterId { get; private set; }
        public Double Latitude { get; private set; }
        public Double Longitude { get; private set; }

        public ScooterState State { get; private set; }
        public Boolean IsLocked { get; private set; } = true;
        public Battery Battery { get; private set; } = new Battery();

        public static VirtualScooter Instance
        {
            get
            {
                if (_instance != null) return _instance;
                    
                return _instance = new VirtualScooter();
            }
        }

        public void PrintVirtualScooterInfo()
        {
            Console.WriteLine(nameof(ScooterId) + $" {ScooterId}");
            Console.WriteLine(nameof(Latitude) + $" {Latitude}");
            Console.WriteLine(nameof(Longitude) + $" {Longitude}");
            Console.WriteLine(nameof(State) + $" {State}");
            Console.WriteLine(nameof(IsLocked) + $" {IsLocked}");
            Console.WriteLine(nameof(Battery) + $" {Battery.Charge}");
        }

        public Result CheckScooterAvailability()
        {
            if (!Battery.IsCharged) return Result.Fail("Самокат не заряжен на достаточном уровне");

            if (State != ScooterState.AvailableForRent)
            {
                switch (State)
                {
                    case ScooterState.Rented:
                        return Result.Fail("Выбранный самокат уже арендован");
                    case ScooterState.HaveErrors:
                        return Result.Fail("Выбранный самокат не возможно арендовать, имеет ошибки");
                    default:
                        break;
                }
            }

            return Result.Success;
        }

        public async void MoveTo(Double amountX, Double amountY)
        {
            do
            {
                await Task.Delay(5000);
                amountX = CalcAmount(Latitude, amountX);
                amountY = CalcAmount(Longitude, amountY);
                Move(amountX, amountY);

            } while (Latitude != amountX && Longitude != amountY);

            Double CalcAmount(Double currentPoint, Double resultPoint)
            {
                Double amount =  Math.MinMagnitude(0.1, resultPoint - currentPoint);
                
                if (currentPoint > resultPoint) return -amount;
                return amount;
            }

            void Move(Double amountX, Double amountY)
            {
                Latitude += amountX;
                Longitude += amountY;
                Battery.Discharge();
            }
        }

        public void Recharge()
        {
            Battery.Recharge();
        }

        public void SetScooterState(ScooterState state)
        {
            State = state;
        }

        public void StartRent()
        {
            IsLocked = false;
            State = ScooterState.Rented;
        }

        public void EndRent()
        {
            IsLocked = true;
            State = ScooterState.AvailableForRent;
        }

        public void SetCoordinates(Double x, Double y)
        {
            Latitude = x;
            Longitude = y;
        }
    }
}
