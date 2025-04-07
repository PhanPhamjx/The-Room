using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _CameraRaycasting : MonoBehaviour
{
    [SerializeField] private float range;
    private IInteractable currentTarget;
    public LayerMask layerMask;
    public Camera mainCamera;
    public RaycastHit WhatIHit;

    [Header("UI Border Settings")]
    [SerializeField] private Image uiBorderImage; // Tham chiếu đến Image của UI Border
    [SerializeField] private float borderPadding = 0.1f; // Padding thêm vào để border to hơn vật thể
    [SerializeField] private LayerMask checkerLayer; // Layer "Checker" để kiểm tra

    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        if(uiBorderImage != null)
        {
           uiBorderImage = GameObject.Find("BorderUI").GetComponent<Image>();   
        }
    }

    private void Update()
    {
        RaycastForInteractable();
        HandleUIBorder();
        if (Input.GetKeyDown(KeyCode.E) && currentTarget != null)
        {
            currentTarget.OnInteract();
        }
    }

    private void RaycastForInteractable()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out WhatIHit, range))
        {
            IInteractable interactable = WhatIHit.collider.GetComponent<IInteractable>();
            if (interactable != null && WhatIHit.distance <= interactable.MaxRange)
            {
                if (interactable != currentTarget)
                {
                    if (currentTarget != null)
                    {
                        currentTarget.OnEndHover();
                    }
                    currentTarget = interactable;
                    currentTarget.OnStartHover();
                }
            }
            else if (currentTarget != null)
            {
                currentTarget.OnEndHover();
                currentTarget = null;
            }
        }
        else if (currentTarget != null)
        {
            currentTarget.OnEndHover();
            currentTarget = null;
        }
    }

    private void HandleUIBorder()
    {
        if (WhatIHit.collider != null && (checkerLayer.value & (1 << WhatIHit.collider.gameObject.layer)) != 0)
        {
            ShowUIBorder(WhatIHit.collider);
        }
        else
        {
            HideUIBorder();
        }
    }

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

    private void HideUIBorder()
    {
        if (uiBorderImage != null)
        {
            uiBorderImage.gameObject.SetActive(false);
        }
    }

    private Vector2 GetScreenCenter(Bounds bounds)
    {
        Vector3 worldCenter = bounds.center;
        return mainCamera.WorldToScreenPoint(worldCenter);
    }

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
        return Mathf.Max(size.x, size.y) * (1 + borderPadding);
    }

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

    private Vector2[] ConvertToScreenSpace(Vector3[] worldCorners)
    {
        Vector2[] screenCorners = new Vector2[worldCorners.Length];
        for (int i = 0; i < worldCorners.Length; i++)
        {
            screenCorners[i] = mainCamera.WorldToScreenPoint(worldCorners[i]);
        }
        return screenCorners;
    }
}