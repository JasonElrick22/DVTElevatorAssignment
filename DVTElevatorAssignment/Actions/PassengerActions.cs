using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVTElevatorAssignment.Actions
{
    public class PassengerActions : IPassengerActions
    {
        public PassengerActions() { }

        public static void DisembarkPassenger(Person person, Elevator elevator)
        {
            Console.WriteLine();
            Console.WriteLine("---> Disembarking passengers: " + person.Name + " from Elevator : " + elevator.ElevatorId + " on floor: " + elevator.Floor);
            Console.WriteLine();
        }

        public static void EmbarkPassenger(Person person)
        {
            Console.WriteLine();
            Console.WriteLine("=====> Embarking : " + person.Name + " From floor: " + person.OriginFloor);
            Console.WriteLine();
        }
    }
}
