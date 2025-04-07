using System.Collections;
using TMPro;
using UnityEngine;
using XNode;

public class NodeParser : MonoBehaviour
{
    public DialogueGraph graph;
    private Coroutine _parser;
    public TextMeshProUGUI speaker;
    public TextMeshProUGUI dialogue;
    public TextMeshProUGUI txt_Choice;

    private DialogueManager _dialogueManager;
    private bool _isTextAnimating;
    private float _textSpeed = 0.05f; // Adjust text speed as needed
    private string _currentText;

    private void Start()
    {
        _dialogueManager = GetComponent<DialogueManager>();
        if (_dialogueManager == null)
        {
            Debug.LogError("DialogueManager not found on the same GameObject!");
        }
    }

    public void StartDialogue()
    {
        foreach (NodeBase node in graph.nodes)
        {
            if (node is StartNode startNode)
            {
                graph.current = startNode;
                Debug.Log("Start Node found and set as current.");
                break;
            }
        }

        if (graph.current != null)
        {
            _parser = StartCoroutine(ParseNode(graph.current as INode));
        }
        else
        {
            Debug.LogError("Start Node not found in graph!");
            _dialogueManager.EndDialogue();
        }
    }

    private IEnumerator ParseNode(INode node)
    {
        node.Execute(this);
        yield return null;
    }

    public void SetDialogue(string speakerName, string dialogueLine)
    {
        speaker.text = speakerName;
        _currentText = dialogueLine;
        StartCoroutine(AnimateText(_currentText));
    }

    private IEnumerator AnimateText(string text)
    {
        _isTextAnimating = true;
        dialogue.text = "";

        foreach (char c in text)
        {
            dialogue.text += c;

            // Check for left mouse click to skip animation
            if (Input.GetMouseButton(0))
            {
                dialogue.text = text;
                break;
            }

            yield return new WaitForSeconds(_textSpeed);
        }

        _isTextAnimating = false;
    }

    public void SetChoice(string choiceLine)
    {
        txt_Choice.text = choiceLine;
    }

    public void WaitForInput(System.Action onInput)
    {
        StartCoroutine(WaitForInputCoroutine(onInput));
    }

    private IEnumerator WaitForInputCoroutine(System.Action onInput)
    {
        // Wait for text animation to complete if it's animating
        while (_isTextAnimating)
        {
            if (Input.GetMouseButtonDown(0))
            {
                dialogue.text = _currentText;
                _isTextAnimating = false;
                break;
            }
            yield return null;
        }

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        onInput?.Invoke();
    }

    public void NextNode(string fieldName)
    {
        if (_parser != null)
        {
            StopCoroutine(_parser);
            _parser = null;
        }

        bool foundConnection = false;

        foreach (NodePort port in graph.current.Ports)
        {
            if (port.fieldName == fieldName && port.Connection != null)
            {
                graph.current = port.Connection.node as NodeBase;
               // Debug.Log("Next Node: " + graph.current.GetString());
                foundConnection = true;
                break;
            }
        }

        if (!foundConnection)
        {
            Debug.LogError("No connection found for field: " + fieldName);
            _dialogueManager.EndDialogue();
            return;
        }

        if (graph.current != null)
        {
            _parser = StartCoroutine(ParseNode(graph.current as INode));
        }
        else
        {
            Debug.LogError("Next Node is null or not connected!");
            _dialogueManager.EndDialogue();
        }
    }

    public void EndDialogue()
    {
        if (_parser != null)
        {
            StopCoroutine(_parser);
            _parser = null;
        }

        _dialogueManager.EndDialogue();
    }
}