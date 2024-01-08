using Models;

namespace DVTElevatorAssignment.Actions
{
    public interface IElevatorActions
    {
        Elevator ElevatorMoveDown(Elevator elevator);
        Elevator ElevatorMoveUp(Elevator elevator);
    }
}