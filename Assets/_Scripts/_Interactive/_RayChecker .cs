using Project_TR.Events;
using UnityEngine;

public class _RayChecker : MonoBehaviour
{
    [SerializeField] private Camera playerCamera; // Camera của người chơi
    [SerializeField] private float raycastDistance = 10f; // Khoảng cách raycast
    [SerializeField] private LayerMask interactableLayer; // Layer của các đối tượng tương tác
    

    public Camera PlayerCamera => playerCamera; // Public property để truy cập camera

    private void Awake()
    {
        if (playerCamera == null)
        {
            Debug.LogError("Player Camera is not assigned in _RayChecker.");
        }
        
    }
    private void FixedUpdate()
    {
        Contact();
    }
    /// <summary>
    /// Kiểm tra raycast và trả về thông tin va chạm nếu có.
    /// </summary>
    public bool TryGetRaycastHit(out RaycastHit hit)
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        return Physics.Raycast(ray, out hit, raycastDistance, interactableLayer);
    }

    public void Contact()
    {
        // Perform raycast and check for hit
        if (TryGetRaycastHit(out RaycastHit hit))
        {
            // Check if the hit object has the "CheckItem" tag and the "E" key is pressed
            if (hit.collider.CompareTag("CheckItem") && Input.GetKeyDown(KeyCode.E))
            {
                // Raise the interaction event
               
            }
        }
    }

    /// <summary>
    /// Kiểm tra xem camera có đang nhìn vào đối tượng hay không.
    /// </summary>
    public bool IsLookingAtObject(Transform target, float viewThreshold = 0.8f)
    {
        Vector3 toTarget = (target.position - playerCamera.transform.position).normalized;
        float dot = Vector3.Dot(playerCamera.transform.forward, toTarget);
        return dot >= viewThreshold;
    }

    private void OnDrawGizmos()
    {
        if (playerCamera != null)
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            Gizmos.color = Color.red;
            Gizmos.DrawRay(ray.origin, ray.direction * raycastDistance);
        }
    }
}