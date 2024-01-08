using DVTElevatorAssignment.Actions;
using DVTElevatorAssignment.Models;
using Models;
using System.Configuration;
using System.Linq;
using static DVTElevatorAssignment.Models.ElevatorDirection;

namespace DVTElevatorAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int MaxFloor = 10; //To do move to config

            #region Initialise Waiting Person array
            IList<Person> persons = new List<Person>();
            #endregion
            #region setup elevators, setup riders
            Elevator elevator1 = new Elevator();
            elevator1.ElevatorId = 1;
            elevator1.Floor = 1;
            elevator1.ElevatorDirection = "Stopped";
            elevator1.Destinations = new List<int>();
            //elevator1.Destinations.Add("2");
            Person person = new Person();
            elevator1.PersonsInElevator = new List<Person>();

            Elevator elevator2 = new Elevator();
            elevator2.ElevatorId = 2;
            elevator2.Floor = MaxFloor;
            elevator2.ElevatorDirection = "Down";
            elevator2.Destinations = new List<int>();
            elevator2.Destinations.Add(5);
            elevator2.PersonsInElevator = new List<Person>();
            elevator2.PersonsInElevator.Add(new Person { DestinationFloor = 5, Id = Guid.NewGuid(), Name = "Test RiderDown" });

            IList<Elevator> elevators = new List<Elevator>();
            elevators.Add(elevator1);
            elevators.Add(elevator2);
            #endregion

            bool addOnce = true;
            //}
            //---------------------
            IElevatorActions elevatorActions = new ElevatorActions();

            bool LocomoteElevators = true;
            while (LocomoteElevators)
            {
                //// Read User input--------
                Console.WriteLine("Press [Enter] to move the elevators.  Press [a] to Add a Waiting Passenger : Origin floor " + ConfigurationManager.AppSettings["OriginFloor"]  + ", Destination " + ConfigurationManager.AppSettings["DestinationFloor"]  + ", Direction " + ConfigurationManager.AppSettings["Direction"]);
                Console.WriteLine();
                ConsoleKeyInfo info = Console.ReadKey();
                if (info.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine("Moving Elevators at : " + DateTime.Now.ToShortTimeString());
                }
                #region Add a waiting person
                if (info.Key == ConsoleKey.A)
                {
                    try{
                        // Add Person
                        Person _person = new Person
                        {
                            Id = Guid.NewGuid(),
                            Name = "Rider Up",
                            OriginFloor = int.Parse(ConfigurationManager.AppSettings["OriginFloor"] ?? ""),
                            DestinationFloor = int.Parse(ConfigurationManager.AppSettings["DestinationFloor"] ?? ""),
                            Direction = ConfigurationManager.AppSettings["Direction"]
                        };
                        //Add person to waiting array
                        persons.Add(_person);

                        // Assign the person destination to relevant elevator
                        foreach (Elevator _elevator in elevators)
                        {
                            // If Elevator is stopped
                            if (_elevator.ElevatorDirection == "Stopped")
                            {
                                // Add destination to elevator Destinations array
                                _elevator.Destinations.Add(_person.OriginFloor);

                                // Determine direction
                                if (_elevator.Floor < _person.OriginFloor)
                                {
                                    _elevator.ElevatorDirection = "Up";
                                }
                                if (_elevator.Floor > _person.OriginFloor)
                                {
                                    _elevator.ElevatorDirection = "Down";
                                }

                                break;
                            }

                            // If Elevator Direction the same as waiting Person

                                // Add direction UP if elevator has not passed by
                                if (_person.Direction == "Up" && _person.OriginFloor - _elevator.Floor > 0)
                                {
                                    _elevator.Destinations.Add(_person.OriginFloor);
                                }

                                // Add direction DOWN if elevator has not passed by
                                if (_person.Direction == "Down" && _person.OriginFloor - _elevator.Floor < 0)
                                {
                                    _elevator.Destinations.Add(_person.OriginFloor);
                                }
                            

                        }
                    }catch (Exception ex)
                    {
                        Console.WriteLine("An error has occurred adding the waiting person.");
                    }
                }
                #endregion

                //Loop through elevators at the current floor
                foreach (Elevator elevator in elevators)
                {

                    //If Floor = one of the destinations, disembark passengers, remove original destination
                    if (elevator.Destinations.Contains(elevator.Floor))
                    {
                        elevator.Destinations.Remove(elevator.Floor);
                    }

                    // Disembark Persons at destination
                    // Disembarking Persons where destination = current floor
                    for (int j = 0; j < elevator.PersonsInElevator.Count; j++)
                    {
                        if (elevator.Floor == elevator.PersonsInElevator[j].DestinationFloor)
                        {   
                            PassengerActions.DisembarkPassenger(elevator.PersonsInElevator[j], elevator);
                            // Person remove from elevator
                            elevator.PersonsInElevator.Remove(elevator.PersonsInElevator[j]);
                            // Remove the current destination floor
                            elevator.Destinations.Remove(elevator.Floor);
                        }
                    }

                    // Embark Persons
                    // Embark Persons to elevator if the elevator is at the Origin floor and direction is the same
                    for (int j = 0; j < persons.Count; j++)
                    {
                        if (persons[j].OriginFloor == elevator.Floor && persons[j].Direction == elevator.ElevatorDirection)  //ADD Direction
                        {
                            PassengerActions.EmbarkPassenger(persons[j]);
                            // Person add to elevator
                            elevator.PersonsInElevator.Add(persons[j]);
                            // Add person's destination to elevator destination list
                            elevator.Destinations.Add(persons[j].DestinationFloor);
                            // Person Leave floor
                            persons.Remove(persons[j]);
                        }
                    }

                    // Check if elevator must stop on current floor
                    if (elevator.Destinations.Count == 0)
                    {
                        elevator.ElevatorDirection = "Stopped";
                    }

                    // ==== Report elevator Information ====
                    ElevatorActions.ElevatorReportInformation(elevator);
                    // =====================================

                    // Elevator Move logic 
                    // Move Elevator not set to a destination
                    if (elevator.Destinations.Count > 0)
                    {
                        // Move elevators that have riders
                        if (elevator.Destinations.Count > 0 && elevator.PersonsInElevator.Count() > 0)
                        {
                            if (elevator.ElevatorDirection == "Up")
                            {
                                elevator.Floor++;
                            }
                            if (elevator.ElevatorDirection == "Down")
                            {
                                elevator.Floor--;
                            }
                        }
                        else  // Move elevators that have no riders, but have destination
                        {
                            //If elevator floor less than max destination go up
                            if (elevator.Floor < elevator.Destinations.Max())
                            {
                                elevator.Floor++;
                            }
                            //If elevator floor more than max destination go down
                            if (elevator.Floor > elevator.Destinations.Max())
                            {
                                elevator.Floor--;
                            }
                        }
                    }
                }
                
                // Elevators must continue moving check
                LocomoteElevators = ElevatorActions.LocomoteElevators(elevators);
            }

            Console.WriteLine("No further pending destinations or People on elevators.");
        }
    }
}
