using System.Collections.Generic;
using UnityEngine;

public interface ILayout<TNode, TConfiguration>
{
    IGraph<TNode> Graph { get; }
    bool HasConfiguration(TNode node, out TConfiguration configuration);

    void SetConfiguration(TNode node, TConfiguration configuration);

    void RemoveConfiguration(TNode node);

    IEnumerable<TConfiguration> GetAllConfigurations();
}
