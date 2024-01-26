using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Travancore.ROBY
{

    public enum NodeColors
    {
        Red, 
        Green, 
        Blue, 
        Orange, 
        Purple
    }


    [Serializable]
    public struct NodeData
    {
        public NodeColors Nodecolor;
        public int Node1Cell;
        public int Node2Cell;
    }

    [Serializable]
    public class Level
    {
        public int LevelNumber;
        public List<NodeData> Nodes = new List<NodeData>();
    }

    [Serializable]
    public class TotalLevels
    {
        public List<Level> Levels = new List<Level>();
    }
}
