using Edgar.Graphs;
using System;
using System.Collections.Generic;

[Serializable]
public class DungeonGraph<T> : IGraph<T>
{
    private Dictionary<T, HashSet<T>> adjacencyList;

    public bool isDirected => false;

    public IEnumerable<T> vertices => adjacencyList.Keys;

    public IEnumerable<IEdge<T>> edges => GetEdges();

    public int verticesCount => throw new NotImplementedException();

    public DungeonGraph()
    {
        adjacencyList = new Dictionary<T, HashSet<T>>();
    }

    public bool AddVertex(T vertex)
    {
        if (adjacencyList.ContainsKey(vertex))
        {
            return false;
        }
        adjacencyList[vertex] = new HashSet<T>();
        return true;
    }

    public bool RemoveVertex(T vertex)
    {
        if (!adjacencyList.ContainsKey(vertex))

        {
            return false;
        }

        foreach (var neighbor in adjacencyList[vertex])
        {
            adjacencyList[neighbor].Remove(vertex);
        }

        adjacencyList.Remove(vertex);
        return true;
    }

    public bool AddEdge(T from, T to)
    {
        if (!ContainsVertex(from))
        {
            throw new KeyNotFoundException("From-Vertex does not exist");
        }
        if (!ContainsVertex(to))
        {
            throw new KeyNotFoundException("To-Vertex does not exist");
        }
        bool added = adjacencyList[from].Add(to);
        adjacencyList[to].Add(from);
        return added;
    }

    public bool RemoveEdge(T from, T to)
    {
        if (!adjacencyList.ContainsKey(from) || !adjacencyList.ContainsKey(to))
        {
            return false;
        }

        bool removed = adjacencyList[from].Remove(to);
        adjacencyList[to].Remove(from);
        return removed;
    }

    public IEnumerable<T> GetNeighbours(T vertex)
    {
        if (!adjacencyList.ContainsKey(vertex))
        {
            throw new KeyNotFoundException("Vertex not part of the graph");
        }
        return adjacencyList[vertex];
    }

    public bool HasEdge(T from, T to)
    {
        return adjacencyList.ContainsKey(from) && adjacencyList[from].Contains(to);
    }

    public IEnumerable<T> GetAllVertices()
    {
        return adjacencyList.Keys;
    }

    public bool ContainsVertex(T vertex)
    {
        return adjacencyList.ContainsKey(vertex);
    }

    public void Clear()
    {
        adjacencyList.Clear();
    }

    private IEnumerable<IEdge<T>> GetEdges()
    {
        var usedEdges = new HashSet<Tuple<T, T>>();
        var edges = new List<IEdge<T>>();

        foreach (var pair in adjacencyList)
        {
            var vertex = pair.Key;
            var neighbours = pair.Value;

            foreach (var neighbour in neighbours)
            {
                if (usedEdges.Contains(Tuple.Create(vertex, neighbour)) || usedEdges.Contains(Tuple.Create(neighbour, vertex)))
                    continue;

                edges.Add(new Edge<T>(vertex, neighbour));
                usedEdges.Add(Tuple.Create(vertex, neighbour));
            }
        }

        return edges;
    }
}
