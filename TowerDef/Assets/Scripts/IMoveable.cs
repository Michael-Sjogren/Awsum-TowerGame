
using UnityEngine;

public interface IMoveable 
{
    AgentController Controller { get; set;}
    Stat GetMovementSpeed();
    void MoveTo( Vector3 position );
}