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
    public List<TNode> Nodes { get; }

    /// <summary>
    /// Number of the chain.
    /// </summary>
    public int Number { get; }

    /// <summary>
    /// Whether it was created from a face or from an acyclic component.
    /// </summary>
    public bool IsFromFace { get; set; }

    public Chain(List<TNode> nodes, int number)
    {
        this.Nodes = nodes;
        this.Number = number;
    }

    #region Equals

    protected bool Equals(Chain<TNode> other)
    {
        return Nodes.SequenceEqual(other.Nodes) && Number == other.Number;
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
            return ((Nodes != null ? Nodes.GetHashCode() : 0) * 397) ^ Number;
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
