using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public abstract class Node
{
    public abstract bool Execute();
}

public class Selector : Node
{
    private List<Node> _children = new List<Node>();

    public Selector(List<Node> children) => _children = children;

    public override bool Execute()
    {
        foreach (var child in _children)
        {
            if (child.Execute())
            {
                return true;
            }
        }
        return false;
    }
}

public class Sequence : Node
{
    private List<Node> _children = new List<Node>();

    public Sequence(List<Node> children) => _children = children;

    public override bool Execute()
    {
        foreach (var child in _children)
        {
            if (!child.Execute())
            {
                return false;
            }
        }
        return true;
    }
}

public class CheckStrengthNode : Node
{
    string _type;
    float _threshold;
    float _strength;
    public CheckStrengthNode(string type, float strength, float threshold)
    {
        _type = type;
        _strength = strength;
        _threshold = threshold;
    }

    public override bool Execute()
    {
        if (_strength > _threshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class FollowWaypointsNode : Node
{
    NPCController _controller;
    public FollowWaypointsNode(NPCController controller)
    {
        this._controller = controller;
    }
    
    public override bool Execute()
    {
        _controller.isPatrol = true;
        return true;
    }
}

public class stayAlertNode : Node
{
    NPCController _controller;
    public stayAlertNode(NPCController controller)
    {
        this._controller = controller;
    }

    public override bool Execute()
    {
        _controller.isAlert = true;
        return true;
    }
}

