using Edgar.Graphs;
using System.Collections.Generic;
using UnityEngine;

public interface IGraph<T>
{
    public bool IsDirected { get; }

    public IEnumerable<T> Vertices { get; }

    public IEnumerable<IEdge<T>> Edges { get; }

    public int VerticesCount { get; }

    public bool AddVertex(T vertex);

    public bool AddEdge(T from, T to);

    public void Clear();



    public IEnumerable<T> GetNeighbours(T vertex);

    public bool HasEdge(T from, T to);
}
