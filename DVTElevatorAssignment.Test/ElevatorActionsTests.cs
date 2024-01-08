using DVTElevatorAssignment.Actions;
using Models;

namespace DVTElevatorAssignment.Test
{
    [TestClass]
    public class ElevatorActionsTests
    {

        [TestMethod]
        public void Locomote_Elevators()
        {
            IList<Elevator> elevators = new List<Elevator>();
            Elevator elevator2 = new Elevator();
            elevator2.ElevatorId = 2;
            elevator2.Floor = 10;
            elevator2.ElevatorDirection = "Down";
            elevator2.Destinations = new List<int>();
            elevator2.Destinations.Add(5);
            elevator2.PersonsInElevator = new List<Person>();
            elevator2.PersonsInElevator.Add(new Person { DestinationFloor = 5, Id = Guid.NewGuid(), Name = "Test RiderDown" });
            elevators.Add(elevator2);

            Assert.IsTrue(ElevatorActions.LocomoteElevators(elevators));
        }

        [TestMethod]
        public void Locomote_Elevators_Negative()
        {
            IList<Elevator> elevators = new List<Elevator>();
            Elevator elevator2 = new Elevator();
            elevator2.ElevatorId = 2;
            elevator2.Floor = 10;
            elevator2.ElevatorDirection = "Down";
            elevator2.Destinations = new List<int>();
            elevator2.PersonsInElevator = new List<Person>();
            elevator2.PersonsInElevator.Add(new Person { DestinationFloor = 5, Id = Guid.NewGuid(), Name = "Test RiderDown" });
            elevators.Add(elevator2);

            Assert.IsFalse(ElevatorActions.LocomoteElevators(elevators));
        }

        [TestMethod]
        public void ElevatorMoveUp_Postive()
        {
            IElevatorActions elevatorActions = new ElevatorActions();
            Elevator elevator = new Elevator();
            elevator.Floor = 1;

            elevator = elevatorActions.ElevatorMoveUp(elevator);
            Assert.AreEqual(2, elevator.Floor);
        }

        [TestMethod]
        public void ElevatorMoveUp_Negative()
        {
            IElevatorActions elevatorActions = new ElevatorActions();
            Elevator elevator = new Elevator();
            elevator.Floor = 1;

            elevator = elevatorActions.ElevatorMoveUp(elevator);
            Assert.AreNotEqual(1, elevator.Floor);
        }

        [TestMethod]
        public void ElevatorMoveDown_Postive()
        {
            IElevatorActions elevatorActions = new ElevatorActions();
            Elevator elevator = new Elevator();
            elevator.Floor = 2;

            elevator = elevatorActions.ElevatorMoveDown(elevator);
            Assert.AreEqual(1, elevator.Floor);
        }

        [TestMethod]
        public void ElevatorMoveDown_Negative()
        {
            IElevatorActions elevatorActions = new ElevatorActions();
            Elevator elevator = new Elevator();
            elevator.Floor = 1;

            elevator = elevatorActions.ElevatorMoveDown(elevator);
            Assert.AreNotEqual(1, elevator.Floor);
        }
    }
}
