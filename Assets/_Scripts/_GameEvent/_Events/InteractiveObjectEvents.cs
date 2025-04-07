using UnityEngine;

namespace Project_TR.Events
{
    // using for door object
    [CreateAssetMenu(fileName = "New Interactive Object Events", menuName = "Game Events/Interactive Object Events")]
    public class InteractiveObjectEvents : BaseGameEvent<Void>
    {
        public void Raise() => Raise(new Void());
    }

}