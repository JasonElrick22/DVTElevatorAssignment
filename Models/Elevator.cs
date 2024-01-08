namespace Models
{
    public class Elevator
    {
        public int ElevatorId { get; set; }
        public int Floor { get; set; }
        public IList<Person> PersonsInElevator { get; set; }
    }
}
