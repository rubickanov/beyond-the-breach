using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKVA.Assets.Vince.Scripts.Astar
{
    public class NodeScript : IHeapItem<NodeScript>
    {
        public bool walkable;
        public Vector3 worldPosition;
        public int gridX;
        public int gridY;

        public int gCost;
        public int hCost;
        public NodeScript parent;
        int heapIndex;

        public NodeScript(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
        {
            walkable = _walkable;
            worldPosition = _worldPos;
            gridX = _gridX;
            gridY = _gridY;
        }

        public int fCost
        {
            get
            {
                return gCost + hCost;
            }
        }

        public int HeapIndex
        {
            get
            {
                return heapIndex;
            }
            set
            {
                heapIndex = value;
            }
        }

        public int CompareTo(NodeScript nodeToCompare)
        {
            int compare = fCost.CompareTo(nodeToCompare.fCost);
            if (compare == 0)
            {
                compare = hCost.CompareTo(nodeToCompare.hCost);
            }
            return -compare;
        }

        //public int CompareTo(object obj)
        //{
        //    Node2 otherNode = (Node)obj;
        //    if (Fcost < otherNode.Fcost)
        //    {
        //        return -1;
        //    }
        //    else if (Fcost > otherNode.Fcost)
        //    {
        //        return 1;
        //    }
        //    return 0;
        //}
    }
}