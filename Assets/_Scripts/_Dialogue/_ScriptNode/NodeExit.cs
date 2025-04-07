using UnityEngine;
using XNode;

public class NodeExit : NodeBase, INode
{
    [Input] public int entry;

    public override string GetString()
    {
        return "Exit";
    }
    public void Execute(NodeParser parser)
    {
        parser.EndDialogue();
    }
}