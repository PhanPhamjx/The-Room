using Project_TR.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class _BorderUI : MonoBehaviour
{
   
    [Header("UI Setup")]
    [SerializeField] private Image uiBorderImage; // Image của UI Border
    [SerializeField] private TextMeshProUGUI txt_Info;
    [SerializeField] private float borderPadding = 0.1f; // Padding thêm vào để border to hơn vật thể

    [Header("Raycast Setup")]
    [SerializeField] private CameraRaycasting rayChecker; // RayChecker để kiểm tra raycast

    private void Update()
    {
        
    }

    /// <summary>
    /// Quản lý raycast và hiển thị UI Border nếu phát hiện vật thể tương tác.
    /// </summary>
    //private void HandleRaycastAndBorder()
    //{
    //    if (rayChecker.TryGetRaycastHit(out RaycastHit hit))
    //    {
    //        if (IsInteractable(hit.collider) && rayChecker.IsLookingAtObject(hit.transform))
    //        {
                
    //            ShowUIBorder(hit.collider);
    //        }
    //        else
    //        {
    //            HideUIBorder();
    //        }
    //    }
    //    else
    //    {
    //        HideUIBorder();
    //    }
    //}

    /// <summary>
    /// Kiểm tra xem collider có phải là vật thể tương tác hay không.
    /// </summary>
    private bool IsInteractable(Collider collider)
    {
        return collider.CompareTag("CheckItem");
    }

    /// <summary>
    /// Hiển thị UI Border tại vị trí và kích thước tương ứng với vật thể.
    /// </summary>
    private void ShowUIBorder(Collider itemCollider)
    {
        if (uiBorderImage == null) return;

        uiBorderImage.gameObject.SetActive(true);
        RectTransform rt = uiBorderImage.rectTransform;

        // Tính toán vị trí và kích thước của Border
        Vector2 screenCenter = GetScreenCenter(itemCollider.bounds);
        float squareSize = CalculateSquareSize(itemCollider.bounds);

        // Cập nhật vị trí và kích thước của Border
        rt.position = screenCenter;
        rt.sizeDelta = new Vector2(squareSize, squareSize);
    }

    /// <summary>
    /// Ẩn UI Border.
    /// </summary>
    private void HideUIBorder()
    {
        if (uiBorderImage != null)
        {
            uiBorderImage.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Tính toán vị trí tâm của vật thể trong screen space.
    /// </summary>
    private Vector2 GetScreenCenter(Bounds bounds)
    {
        Vector3 worldCenter = bounds.center;
        return rayChecker.mainCamera.WorldToScreenPoint(worldCenter);
    }

    /// <summary>
    /// Tính toán kích thước hình vuông bao quanh vật thể trên màn hình.
    /// </summary>
    private float CalculateSquareSize(Bounds bounds)
    {
        Vector3[] worldCorners = GetWorldCorners(bounds);
        Vector2[] screenCorners = ConvertToScreenSpace(worldCorners);

        Vector2 minScreen = screenCorners[0];
        Vector2 maxScreen = screenCorners[0];

        for (int i = 1; i < screenCorners.Length; i++)
        {
            minScreen = Vector2.Min(minScreen, screenCorners[i]);
            maxScreen = Vector2.Max(maxScreen, screenCorners[i]);
        }

        Vector2 size = maxScreen - minScreen;
        float squareSize = Mathf.Max(size.x, size.y);
        return squareSize * (1 + borderPadding);
    }

    /// <summary>
    /// Lấy các góc của vật thể trong world space.
    /// </summary>
    private Vector3[] GetWorldCorners(Bounds bounds)
    {
        return new Vector3[]
        {
            new Vector3(bounds.min.x, bounds.min.y, bounds.min.z),
            new Vector3(bounds.min.x, bounds.min.y, bounds.max.z),
            new Vector3(bounds.max.x, bounds.min.y, bounds.min.z),
            new Vector3(bounds.max.x, bounds.min.y, bounds.max.z)
        };
    }

    /// <summary>
    /// Chuyển đổi các góc từ world space sang screen space.
    /// </summary>
    private Vector2[] ConvertToScreenSpace(Vector3[] worldCorners)
    {
        Vector2[] screenCorners = new Vector2[worldCorners.Length];
        for (int i = 0; i < worldCorners.Length; i++)
        {
            screenCorners[i] = rayChecker.mainCamera.WorldToScreenPoint(worldCorners[i]);
        }
        return screenCorners;
    }
}