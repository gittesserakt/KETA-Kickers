using System.Collections.Generic;
using System.Linq;

using System;

/// <inheritdoc />
/// <summary>
/// Base class for chain decomposer with some utility functions.
/// </summary>
public abstract class ChainDecompositionBase<TNode> : IChainDecomposition<TNode>
{
    protected IGraph<TNode> graph;
    protected Dictionary<TNode, int> coveredVertices;
    protected int chainsCounter;
    protected List<List<TNode>> faces;
    protected GraphUtils graphUtils = new GraphUtils();


    /// <inheritdoc />
    public abstract List<Chain<TNode>> GetChains(IGraph<TNode> graph);

    /// <summary>
    /// Setups graph and gets faces.
    /// </summary>
    /// <param name="graph"></param>
    protected void Initialize(IGraph<TNode> graph)
    {
        if (!graphUtils.IsConnected(graph))
            throw new ArgumentException("The graph must be connected", nameof(graph));

        this.graph = graph;
        faces = graphUtils.GetPlanarFaces(this.graph);
        coveredVertices = new Dictionary<TNode, int>();

        // Initialize all vertices to the -1 depth
        graph.vertices.ToList().ForEach(x => SetDepth(x, -1));
    }

    /// <summary>
    /// Returns the smallest depth among all neighbours that all already covered.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    protected int SmallestCoveredNeighbourDepth(TNode node)
    {
        var neighbours = graph.GetNeighbours(node);
        var smallestDepth = int.MaxValue;

        foreach (var neighbour in neighbours)
        {
            var depth = GetDepth(neighbour);

            if (depth != -1 && depth < smallestDepth)
            {
                smallestDepth = depth;
            }
        }

        return smallestDepth;
    }

    /// <summary>
    /// Gets the depth of a given node.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    protected int GetDepth(TNode node)
    {
        return coveredVertices[node];
    }

    /// <summary>
    /// Sets the depth of a given node.
    /// </summary>
    /// <param name="node"></param>
    /// <param name="depth"></param>
    protected void SetDepth(TNode node, int depth)
    {
        coveredVertices[node] = depth;
    }

    /// <summary>
    /// Checks whether a given node is already covered.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    protected bool IsCovered(TNode node)
    {
        return GetDepth(node) != -1;
    }

    /// <summary>
    /// Counts the number of uncovered neighbours of a given node.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    protected int UncoveredNeighboursCount(TNode node)
    {
        var neighbours = graph.GetNeighbours(node);

        return neighbours.Count(x => !IsCovered(x));
    }
}