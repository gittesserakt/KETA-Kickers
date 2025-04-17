using UnityEngine;
using System.Collections.Generic;

public class DungeonGraphManager : MonoBehaviour
{
    private DungeonGraph<DungeonRoom> graph;

    void Start()
    {
        graph = new DungeonGraph<DungeonRoom>();

        DungeonRoom roomA = new DungeonRoom("A", new Vector3(0, 0, 0));
        DungeonRoom roomB = new DungeonRoom("B", new Vector3(5, 0, 0));
        DungeonRoom roomC = new DungeonRoom("C", new Vector3(0, 0, 5));
        DungeonRoom roomD = new DungeonRoom("D", new Vector3(5, 0, 5));

        graph.AddVertex(roomA);
        graph.AddVertex(roomB);
        graph.AddVertex(roomC);
        graph.AddVertex(roomD);

        graph.AddEdge(roomA, roomB);
        graph.AddEdge(roomA, roomC);
        graph.AddEdge(roomB, roomD);

        PrintGraph();
    }

    void PrintGraph()
    {
        foreach (var room in new List<DungeonRoom>(graph.GetAllVertices()))
        {
            string neighbours = string.Join(", ", graph.GetNeighbours(room));
            Debug.Log($"{room} is connected to: {neighbours}");
        }
    }
}
