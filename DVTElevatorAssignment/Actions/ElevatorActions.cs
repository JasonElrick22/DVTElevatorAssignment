using Models;
using System;

namespace DVTElevatorAssignment.Actions
{
    public class ElevatorActions : IElevatorActions
    {
        public ElevatorActions()
        {
        }

        public static int ElevatorReportInformation(Elevator elevator)
        {
            Console.WriteLine("===========ELEVATOR STATUS===========");
            Console.WriteLine("Elevator ID : " + elevator.ElevatorId);
            Console.WriteLine("Elevator floor : " + elevator.Floor + " ");
            Console.WriteLine("Direction : " + elevator.ElevatorDirection);
            Console.Write("Destinations : ");
            foreach (var destination in elevator.Destinations)
            {
                Console.Write(destination + " ");

            }
            Console.WriteLine();
            Console.WriteLine("Passengers : " + elevator.PersonsInElevator.Count + " ");

            for (int i = 0; i < elevator.PersonsInElevator.Count; i++)
            {
                if (elevator.PersonsInElevator is not null)
                {
                    Console.WriteLine("Passengers: " + elevator.PersonsInElevator[i].Name + " Id: " + elevator.PersonsInElevator[i].Id + " Destination: " + elevator.PersonsInElevator[i].DestinationFloor + " Direction: " + elevator.PersonsInElevator[i].Direction);
                }
            }

            Console.WriteLine("================================");
            Console.WriteLine();
            return 0;
        }

        public static bool LocomoteElevators(IList<Elevator> elevators)
        {
            int destinationCount = 0;
            foreach (Elevator __elevator in elevators)
            {
                destinationCount = destinationCount + __elevator.Destinations.Count();
            }
            if (destinationCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Elevator ElevatorMoveUp(Elevator elevator)
        {
            elevator.Floor++;
            return elevator;
        }

        public Elevator ElevatorMoveDown(Elevator elevator)
        {
            elevator.Floor--;
            return elevator;
        }
    }
}