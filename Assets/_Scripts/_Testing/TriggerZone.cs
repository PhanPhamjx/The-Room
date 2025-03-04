using UnityEngine;
using Project_TR.Events;

[RequireComponent(typeof(Collider))]
public class TriggerZone : MonoBehaviour
{
    [SerializeField] private TriggerEvent triggerEvent;
    [SerializeField] private bool isOneTimeTrigger = true;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered && isOneTimeTrigger) return;

        if (other.CompareTag("Player"))
        {
            triggerEvent.Raise(new Void());  // Kích hoạt sự kiện
            hasTriggered = true;
            
        }
    }
}
