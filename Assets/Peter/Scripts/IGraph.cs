using Edgar.Graphs;
using System.Collections.Generic;
using UnityEngine;

public interface IGraph<T>
{
    public bool isDirected { get; }

    public IEnumerable<T> vertices { get; }

    public IEnumerable<IEdge<T>> edges { get; }

    public int verticesCount { get; }

    public bool AddVertex(T vertex);

    public bool AddEdge(T from, T to);

    public void Clear();



    public IEnumerable<T> GetNeighbours(T vertex);

    public bool HasEdge(T from, T to);
}
