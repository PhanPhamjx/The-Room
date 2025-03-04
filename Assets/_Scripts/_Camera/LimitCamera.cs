using UnityEngine;

public class LimitCamera : MonoBehaviour
{
    public float minX = -75f;  // Giới hạn góc thấp nhất trên trục X
    public float maxX = 75f;   // Giới hạn góc cao nhất trên trục X
    private float currentXRotation = 0f;

    void Start()
    {
        currentXRotation = transform.localEulerAngles.x;
    }

    void LateUpdate()
    {
        float mouseY = Input.GetAxis("Mouse Y");  // Lấy input di chuyển chuột theo trục Y
        currentXRotation -= mouseY;
        currentXRotation = Mathf.Clamp(currentXRotation, minX, maxX);  // Giới hạn góc quay

        this.transform.localEulerAngles = new Vector3(currentXRotation, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}
