using UnityEngine;

public class DestroyObject : MonoBehaviour, IInteractable
{
    private bool isDestroyed = false; // Đánh dấu trạng thái đã bị hủy

    float IInteractable.MaxRange => 2.0f;

    public void DestroyObjects()
    {
        if (!isDestroyed)
        {
            isDestroyed = true;
            Destroy(gameObject);
        }
    }

    public void OnEndHover()
    {
        if (!isDestroyed)
        {
            Debug.Log("dang nhin vao " + gameObject.name);
        }
    }

    public void OnInteract()
    {
        DestroyObjects();
    }

    public void OnStartHover()
    {
        if (!isDestroyed)
        {
            Debug.Log("khong nhin vao " + gameObject.name);
        }
    }
}
