using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum visited_State
{   none_visited,
    hadvisited
}
public enum Pass_State
{
    CanPass,
    CannotPass


}

public class MapListNode
{
    public GameObject charactor;
    public int x;
    public int z;
    public Vector3 mapPoint_pos;
    public MapType path_Element;
    public visited_State isvisited;
    public Pass_State Pass_State;
    public int pathcount;
    public MapListNode Nodeup;
    public MapListNode NodeDown;
    public MapListNode NodeLeft;
    public MapListNode NodeRight;
    public MapListNode fatherNode;
    public MapListNode(int z,int x, Vector3 mapPoint_pos, MapType path_Element, int pathcount)
    {
        this.z = z;
        this.x = x;
        this.pathcount = pathcount;
        this.mapPoint_pos = mapPoint_pos;
        this.mapPoint_pos.y = 0.3f;
        this.path_Element = path_Element;
        isvisited = visited_State.none_visited;
        Nodeup = null;
        NodeDown = null;
        NodeLeft = null;
        NodeRight = null;
        fatherNode = null;
        charactor = null;
    }

    public MapListNode theNodeNearby_up()
    {
        if (Nodeup != null)
        {
            return Nodeup;
        }
        return null;
    }

    public MapListNode theNodeNearby_down()
    {
        if (NodeDown!= null)
        {
            return NodeDown;
        }
        return null;
    }

    public MapListNode theNodeNearby_Left()
    {
        if (NodeLeft != null)
        {
            return NodeLeft;
        }
        return null;
    }

    public MapListNode theNodeNearby_Right()
    {
        if (NodeRight != null)
        {
            return NodeRight;
        }
        return null;
    }

 
}
