using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public abstract class BTNode
{
    public abstract bool Execute();
}

public class Selector : BTNode
{
    private List<BTNode> _children = new List<BTNode>();

    public Selector(List<BTNode> children) => _children = children;

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

public class Sequence : BTNode
{
    private List<BTNode> _children = new List<BTNode>();

    public Sequence(List<BTNode> children) => _children = children;

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

public class CheckStrengthNode : BTNode
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

public class FollowWaypointsNode : BTNode
{
    NPCController _controller;
    public FollowWaypointsNode(NPCController controller)
    {
        this._controller = controller;
    }
    
    public override bool Execute()
    {
        _controller.isPatrol = true;
        Debug.Log("Patrol Mode: ON");
        return true;
    }
}

public class StayAlertNode : BTNode
{
    NPCController _controller;
    public StayAlertNode(NPCController controller)
    {
        this._controller = controller;
    }

    public override bool Execute()
    {
        _controller.isAlert = true;
        Debug.Log("Dectection Mode: ON");
        return true;
    }
}

public class GoPlayerNode : BTNode
{
    NPCController _controller;
    public GoPlayerNode(NPCController controller)
    {
        this._controller = controller;
    }

    public override bool Execute()
    {
        _controller.isAttack = true;
        return true;
    }
}

