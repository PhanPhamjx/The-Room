using XNode;
using UnityEngine;

[NodeWidth(300)]
public class DialogueNode : NodeBase,INode
{
    [Input] public int entry;
    [Output] public int exit;
    public string speakerName;
    [TextArea(10, 5)] public string dialogueLine;

    public override string GetString()
    {
        return "Dialogue Node|" + speakerName + "|" + dialogueLine;
    }

    public void Execute(NodeParser parser)
    {
        parser.SetDialogue(speakerName, dialogueLine);
        parser.WaitForInput(() => parser.NextNode("exit"));
    }
}