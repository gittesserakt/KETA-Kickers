using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Represents a chain of nodes in a chain decomposition.
/// </summary>
/// <typeparam name="TNode"></typeparam>
public class Chain<TNode>
{
    /// <summary>
    /// Nodes in the chain.
    /// </summary>
    public List<TNode> nodes { get; }

    /// <summary>
    /// Number of the chain.
    /// </summary>
    public int number { get; }

    /// <summary>
    /// Whether it was created from a face or from an acyclic component.
    /// </summary>
    public bool isFromFace { get; set; }

    public Chain(List<TNode> nodes, int number)
    {
        this.nodes = nodes;
        this.number = number;
    }

    #region Equals

    protected bool Equals(Chain<TNode> other)
    {
        return nodes.SequenceEqual(other.nodes) && number == other.number;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }
        if (ReferenceEquals(this, obj))
        {
            return true;
        }
        if (obj.GetType() != this.GetType())
        {
            return false;
        }
        return Equals((Chain<TNode>)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return ((nodes != null ? nodes.GetHashCode() : 0) * 397) ^ number;
        }
    }

    public static bool operator ==(Chain<TNode> left, Chain<TNode> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Chain<TNode> left, Chain<TNode> right)
    {
        return !Equals(left, right);
    }

    #endregion
}
