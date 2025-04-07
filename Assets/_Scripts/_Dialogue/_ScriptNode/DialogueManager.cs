using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public bool DialogueMode { get; private set; }

    [SerializeField] private NodeParser _nodeParser;
    [SerializeField] private GameObject _dialogueUI;

    [Header("Events")]
    public UnityEvent OnDialogueStart;
    public UnityEvent OnDialogueEnd;

    private bool _isInteractable = true;

    private void Start()
    {
        if (_dialogueUI != null)
            _dialogueUI.SetActive(false);
    }

    public void StartDialogue()
    {
        if (!_isInteractable || DialogueMode) return;
        if (_nodeParser == null || _nodeParser.graph == null)
        {
            Debug.LogError("NodeParser chưa được thiết lập!", this);
            return;
        }

        DialogueMode = true;
        _isInteractable = false;

        PlayerEvents.TriggerDialogueToggle(true);

        if (_dialogueUI != null)
            _dialogueUI.SetActive(true);

        OnDialogueStart?.Invoke();
        _nodeParser.StartDialogue();
    }

    public void EndDialogue()
    {
        DialogueMode = false;
        _isInteractable = true;

        PlayerEvents.TriggerDialogueToggle(false);

        if (_dialogueUI != null)
            _dialogueUI.SetActive(false);

        OnDialogueEnd?.Invoke();
    }
}