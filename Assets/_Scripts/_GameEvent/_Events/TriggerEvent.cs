using UnityEngine;

namespace Project_TR.Events
{
    [CreateAssetMenu(fileName = "New Trigger Event", menuName = "Game Events/Trigger Event")]
    public class TriggerEvent : BaseGameEvent<Void>
    {
        public void Raise() => Raise(new Void());   
    }

}