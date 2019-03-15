using UnityEngine;

public class Node : IHeapItem<Node> {
    public bool Walkable { get; private set; }
    public Vector3 WorldPos { get; private set; }
    public int GridX { get; private set; }
    public int GridZ { get; private set; }
    public int GCost { get; set; }
    public int HCost { get; set; }
    public int FCost {
        get {
            return GCost + HCost;
        }
    }
    public Node Parent { get; set; }

    private int heapIndex;
    public int HeapIndex {
        get {
            return heapIndex;
        }
        set {
            heapIndex = value;
        }
    }

    public Node(bool walkable, Vector3 worldPos, int gridX, int gridZ) {
        this.Walkable = walkable;
        this.WorldPos = worldPos;
        this.GridX = gridX;
        this.GridZ = gridZ;
    }

    public int CompareTo(Node nodeToCompare) {
        int compare = FCost.CompareTo(nodeToCompare.FCost);
        if (compare == 0) compare = HCost.CompareTo(nodeToCompare.HCost);
        return -compare;
    }
}
