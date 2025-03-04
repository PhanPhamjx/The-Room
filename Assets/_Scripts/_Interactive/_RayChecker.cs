using UnityEngine;

public class RayChecker : MonoBehaviour
{
    [SerializeField] private Camera playerCamera; // Camera của người chơi
    [SerializeField] private float raycastDistance = 10f; // Khoảng cách raycast
    [SerializeField] private LayerMask interactableLayer; // Layer của các đối tượng tương tác

    /// <summary>
    /// Kiểm tra raycast và trả về thông tin va chạm nếu có.
    /// </summary>
    public bool TryGetRaycastHit(out RaycastHit hit)
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        return Physics.Raycast(ray, out hit, raycastDistance, interactableLayer);
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
}