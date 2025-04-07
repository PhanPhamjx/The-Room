using UnityEngine;

public class CameraRaycasting : MonoBehaviour
{
    [SerializeField] private float range;
    private IInteractable currentTarget;
    public Camera mainCamera;
    public RaycastHit WhatIHit;
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        RaycastForInteractable();
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(currentTarget != null)
            {
                currentTarget.OnInteract();
            }
        }
    }
    private void RaycastForInteractable()
    {
       
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out WhatIHit, range))
        {
            IInteractable interactable = WhatIHit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                if (WhatIHit.distance <= interactable.MaxRange)
                {
                    if (interactable == currentTarget)
                    {
                        return;
                    }
                    else if (currentTarget != null)
                    {
                        currentTarget.OnEndHover();
                        currentTarget = interactable;
                        currentTarget.OnStartHover();
                        return;
                    }else
                    {
                        currentTarget = interactable;
                        currentTarget.OnStartHover();   
                    }
                }else
                {
                    if(currentTarget != null)
                    {
                        currentTarget.OnEndHover();
                        currentTarget = null;
                        return;
                    }
                }
            }else
            {
                if(currentTarget != null)
                {
                    currentTarget.OnEndHover();
                    currentTarget = null;
                    return;
                }
            }
        }


    }
}