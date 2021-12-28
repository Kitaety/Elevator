using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elevator
{
    class Elevator
    {
        public float Speed { get; private set; }
        public int CurrentFloor { get; private set; }
        public int NextFloor { get; set; }
        private int _period;
        private Timer _timer;

        public Elevator(float speed)
        {
            Speed = speed;
            _period = (int)(1000 / speed);
            CurrentFloor = 1;
            NextFloor = 1;
        }

        public void Start()
        {
            _timer = CreateTimerAndStart();
        }

        private Timer CreateTimerAndStart()
        {
            return new Timer(new TimerCallback(OnTimerTicket), GetDirection(), _period, _period);
        }

        private void OnTimerTicket(object obj)
        {
            int direction = (int) obj;

            if (CurrentFloor == NextFloor)
            {
                _timer.Dispose();
                EndMove?.Invoke(NextFloor);
                return;
            }

            Console.WriteLine($@"C:{CurrentFloor} N:{NextFloor} Direction:{direction}");
            CurrentFloor += direction;
            Arrival?.Invoke(CurrentFloor);
        }

        private int GetDirection()
        {
            int floorDifference = NextFloor - CurrentFloor;

            if (floorDifference > 0)
            {
                return 1;
            }
            else
            {
                if (floorDifference < 0)
                {
                    return -1;
                }
            }

            return 0;
        }

        public delegate void ElevatorEvent(int floor);
        public event ElevatorEvent Arrival;
        public event ElevatorEvent EndMove;
    }
}
