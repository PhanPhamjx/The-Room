using UnityEngine;

public class _CrosshairManager : MonoBehaviour
{
    [SerializeField] private Texture2D crosshairTexture; // Hình ảnh tâm
    [SerializeField] private float crosshairSize = 20f;  // Kích thước tâm

    private void OnGUI()
    {
        if (crosshairTexture != null)
        {
            DrawCrosshair();
        }
    }

    // Hàm để vẽ tâm lên màn hình
    private void DrawCrosshair()
    {
        Rect crosshairRect = CalculateCrosshairRect();
        GUI.DrawTexture(crosshairRect, crosshairTexture);
    }

    // Hàm tính toán vị trí và kích thước của tâm
    private Rect CalculateCrosshairRect()
    {
        float x = (Screen.width - crosshairSize) / 2;
        float y = (Screen.height - crosshairSize) / 2;
        return new Rect(x, y, crosshairSize, crosshairSize);
    }
}
