using UnityEngine;

public class _DirectionDoor : _Door, IInteractable
{
    [SerializeField] private Transform _HandleDoor;
    [SerializeField] private Transform _DoorOpen;
    [SerializeField] float IInteractable.MaxRange => 2f;
    [SerializeField] private bool isUnlocked;

    private Quaternion _initialHandleRotation;
    private Quaternion _initialDoorRotation;
    private Quaternion _targetHandleRotation;
    private Quaternion _targetDoorRotation;
    private float _rotationSpeed = 2f;
    private bool _isMoving = false;
    private float _handleRotationTime = 0.2f;
   // private float _doorDelayTime = 0.1f;
    //private bool _isHandleReturning = false;

    private void Start()
    {
        _initialHandleRotation = _HandleDoor.localRotation;
        _initialDoorRotation = _DoorOpen.rotation;
        ResetTargetRotations();
    }

    private void FixedUpdate()
    {
        RotateHandle();
        RotateDoor();
    }

    void IInteractable.OnEndHover()
    {
        return;
    }
    void IInteractable.OnStartHover()
    {
        return;
    }

    void IInteractable.OnInteract()
    {
        HandleRotate(8);
        Invoke(nameof(ResetHandleRotation), _handleRotationTime);

        if (isUnlocked)
        {
            HandleRotate(20);
            Invoke(nameof(ResetHandleRotation), _handleRotationTime);
            base.Toggle();
            
        }
        else
        {
            CheckDoorUnlockCondition();
        }
    }

    public override void Open()
    {
        if (!isUnlocked) return;
        base.Open();
        SetDoorRotation(90);
        HandleRotate(20);
        Debug.Log("Đang mở cửa");
    }

    public override void Close()
    {
        if (!isUnlocked) return;
        base.Close();
        SetDoorRotation(0);
        Debug.Log("Đang đóng cửa");
    }

    public override void Toggle()
    {
        if (!isUnlocked)
        {
            Debug.Log("Cửa đang bị khóa, không thể thay đổi trạng thái.");
            return;
        }
        base.Toggle();
        Debug.Log("Cửa hoạt động bình thường");
    }

    private void HandleRotate(float angle)
    {
        _targetHandleRotation = _initialHandleRotation * Quaternion.Euler(0, 0, angle);
    }

    private void RotateHandle()
    {
        _HandleDoor.localRotation = Quaternion.Slerp(_HandleDoor.localRotation, _targetHandleRotation, Time.deltaTime / _handleRotationTime);
    }

    private void ResetHandleRotation()
    {
        _targetHandleRotation = _initialHandleRotation;
    }

    private void SetDoorRotation(float angle)
    {
        _targetDoorRotation = _initialDoorRotation * Quaternion.Euler(0, angle, 0);
        _isMoving = true;
    }

    private void RotateDoor()
    {
        if (!_isMoving) return;
        _DoorOpen.rotation = Quaternion.Slerp(_DoorOpen.rotation, _targetDoorRotation, Time.deltaTime * _rotationSpeed);
        if (Quaternion.Angle(_DoorOpen.rotation, _targetDoorRotation) < 0.1f)
        {
            _DoorOpen.rotation = _targetDoorRotation;
            _isMoving = false;
        }
    }

    private void CheckDoorUnlockCondition()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        if (Vector3.Dot(directionToPlayer, transform.forward) > 0.3f)
        {
            isUnlocked = true;
            Debug.Log("Cửa đã được mở khóa.");
            HandleRotate(60);
            Invoke(nameof(ResetHandleRotation), _handleRotationTime);
        }
        else
        {
            Debug.Log("Cửa đang bị khóa, cần đứng đúng hướng để mở khóa.");
        }
    }

    private void ResetTargetRotations()
    {
        _targetHandleRotation = _initialHandleRotation;
        _targetDoorRotation = _initialDoorRotation;
    }
}