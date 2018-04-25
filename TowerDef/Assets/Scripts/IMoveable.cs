
using UnityEngine;

public interface IMoveable 
{
    AgentController Controller { get; set;}
    float MovementSpeed { get; set;}
    void MoveTo( Vector3 position );
}