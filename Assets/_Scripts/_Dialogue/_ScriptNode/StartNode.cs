using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class StartNode : NodeBase, INode
{
    [Output] public int exit;
    public override string GetString()
    {
        return "Start";
    }
    public void Execute(NodeParser parser)
    {
        parser.NextNode("exit");
    }
}