using Edgar.Graphs;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DungeonLayoutGraph<T>
{
    public bool IsDirected { get; } = false;

    public IEnumerable<T> Vertices => adjacencyLists.Keys;

    public IEnumerable<IEdge<T>> Edges => GetEdges();

    public int VerticesCount => adjacencyLists.Count;

    private readonly Dictionary<T, List<T>> adjacencyLists = new Dictionary<T, List<T>>();

    public void AddVertex(T vertex)
    {
        if (adjacencyLists.ContainsKey(vertex))
            throw new ArgumentException("Vertex is already part of the graph");

        adjacencyLists[vertex] = new List<T>();
    }
    public void AddEdge(T from, T to)
    {
        if (!adjacencyLists.TryGetValue(from, out var fromList) || !adjacencyLists.TryGetValue(to, out var toList))
            throw new ArgumentException("One of the vertices does not exist");

        if (fromList.Contains(to))
            throw new ArgumentException("The edge was already added");

        fromList.Add(to);
        toList.Add(from);
    }

    public IEnumerable<T> GetNeighbours(T vertex)
    {
        if (!adjacencyLists.TryGetValue(vertex, out var neighbours))
            throw new ArgumentException("The vertex does not exist");

        return neighbours;
    }

    public bool HasEdge(T from, T to)
    {
        foreach (var neighbour in GetNeighbours(from))
        {
            if (neighbour.Equals(to))
                return true;
        }

        return false;
    }

    private IEnumerable<IEdge<T>> GetEdges()
    {
        var usedEdges = new HashSet<Tuple<T, T>>();
        var edges = new List<IEdge<T>>();

        foreach (var pair in adjacencyLists)
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
