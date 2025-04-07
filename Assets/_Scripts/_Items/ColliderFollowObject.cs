using UnityEngine;

public class ColliderFollowObject : MonoBehaviour
{
    [SerializeField] private Transform _target; // Đối tượng mà collider sẽ theo
  

    private Collider _collider;

    void Start()
    {
        // Lấy component Collider của đối tượng này
        _collider = this.GetComponent<Collider>();
    }

    void Update()
    {
        // Cập nhật vị trí của collider dựa trên vị trí của target và offset
        if (_target != null && _collider != null)
        {
            _collider.transform.rotation = _target.rotation;
        }
    }
}