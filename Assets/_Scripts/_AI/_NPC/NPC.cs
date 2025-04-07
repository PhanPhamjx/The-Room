using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private float _interactionRange = 2f;
    [SerializeField] private DialogueManager _dialogueManager;

    public float MaxRange => _interactionRange;

    private void Awake()
    {
        if (_dialogueManager == null)
            _dialogueManager = GetComponent<DialogueManager>();

        if (_dialogueManager == null)
            Debug.LogError("Không tìm thấy DialogueManager!", this);
    }

    public void OnInteract()
    {
        if (!IsPlayerInRange()) return;

        _dialogueManager?.StartDialogue();
    }

    public void OnStartHover()
    {
        // Hiệu ứng khi hover (nếu cần)
    }

    public void OnEndHover()
    {
        // Hiệu ứng khi kết thúc hover (nếu cần)
    }

    private bool IsPlayerInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, MaxRange);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Player"))
                return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, MaxRange);
    }
}