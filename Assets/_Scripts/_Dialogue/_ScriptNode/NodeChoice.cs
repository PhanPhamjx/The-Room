using UnityEngine;
using XNode;

public class NodeChoice : NodeBase,INode
{
    [Input] public int entry;
    [Output] public int exit;
    [TextArea(10, 5)] public string choiceLine;
    public override string GetString()
    {
        return choiceLine;
    }
    public void Execute(NodeParser parser)
    {
        parser.SetChoice(choiceLine);
        parser.WaitForInput(() => parser.NextNode("exit"));
    }
}