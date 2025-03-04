using UnityEngine;
using Project_TR.Events;    
public class SpawnOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;  // Vật thể cần xuất hiện
    [SerializeField] private Transform spawnPosition;   // Vị trí xuất hiện

    // Hàm này sẽ được gọi bởi TriggerListener khi sự kiện được kích hoạt
    public void OnTriggerEvent()
    {
        if (objectToSpawn != null && spawnPosition != null)
        {
            Instantiate(objectToSpawn, spawnPosition.position, spawnPosition.rotation);
        }
        else
        {
            Debug.LogWarning("Object to spawn or spawn position is not set.");
        }
    }
}
