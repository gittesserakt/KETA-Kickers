using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an algorithm that can decompose graph into disjunct chains covering all vertices.
/// </summary>
/// <typeparam name="TNode"></typeparam>
public interface IChainDecomposition<TNode>
{
    List<Chain<TNode>> GetChains(IGraph<TNode> graph);
}
