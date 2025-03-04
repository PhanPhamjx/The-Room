using UnityEngine;

namespace Project_TR.Events
{
    // using for door object
    [CreateAssetMenu(fileName = "New DoorEvent", menuName = "Game Events/DoorEvent")]
    public class DoorEvent : BaseGameEvent<Void>
    {
        public void Raise() => Raise(new Void());
    }

}