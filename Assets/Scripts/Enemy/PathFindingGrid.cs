using System.Collections.Generic;
using UnityEngine;

public class PathFindingGrid : MonoBehaviour {
    [SerializeField] private LayerMask obstacleMask = 1<<0;
    [SerializeField] private float nodeRadius = .5f;
    [SerializeField] private GameObject plane = null;

    private Node[,] grid;
    private int gridX, gridZ;
    private Vector3 gridSize;

    public int MaxSize {
        get {
            return gridX * gridZ;
        }
    }

    private void Awake() {
        gridSize = new Vector3(
            Mathf.FloorToInt(plane.transform.localScale.x),
            1,
            Mathf.FloorToInt(plane.transform.localScale.z)
        ) * 10;

        gridX = Mathf.RoundToInt(gridSize.x / (nodeRadius * 2));
        gridZ = Mathf.RoundToInt(gridSize.z / (nodeRadius * 2));

        Vector3 bottomLeft = transform.position - Vector3.right * gridSize.x / 2 - Vector3.forward * gridSize.z / 2;

        grid = new Node[gridX, gridZ];
        for (int x = 0; x < gridX; x++) {
            for (int z = 0; z < gridZ; z++) {
                Vector3 point = bottomLeft + Vector3.right * (x * (nodeRadius * 2) + nodeRadius) +
                                            Vector3.forward * (z * (nodeRadius * 2) + nodeRadius);
                bool walkable = !Physics.CheckSphere(point, nodeRadius, obstacleMask);
                grid[x, z] = new Node(walkable, point, x, z);
            }
        }

    }

    public Node GetNode(Vector3 pos) {
        float percentX = Mathf.Clamp01((pos.x + gridSize.x / 2) / gridSize.x);
        float percentZ = Mathf.Clamp01((pos.z + gridSize.z / 2) / gridSize.z);
        int x = Mathf.RoundToInt((gridX - 1) * percentX);
        int z = Mathf.RoundToInt((gridZ - 1) * percentZ);
        return grid[x, z];
    }

    public List<Node> GetNeighbours(Node node) {
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x < 2; x++) {
            for (int z = -1; z < 2; z++) {
                int checkX = node.GridX + x;
                int checkZ = node.GridZ + z;
                if (checkX > -1 && checkX < gridX && checkZ > -1 && checkZ < gridZ) {
                    neighbours.Add(grid[checkX, checkZ]);
                }
            }
        }
        return neighbours;
    }
    
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, gridSize);
        /*if (grid != null) {
            foreach (var node in grid) {
                    Gizmos.color = node.Walkable ? Color.white : Color.red;
                    Gizmos.DrawCube(node.WorldPos, Vector3.one * ((nodeRadius * 2) - .1f));
                }
        } */
    }
}
