using System.Collections.Generic;
using UnityEngine;

public class Layout<TConfiguration> : ILayout<int, TConfiguration>, ISmartCloneable<Layout<TConfiguration>>
    where TConfiguration : ISmartCloneable<TConfiguration>
{
    private readonly TConfiguration[] vertices;
    private readonly bool[] hasValue;

    public IGraph<int> Graph { get; }


    public Layout(IGraph<int> graph)
    {
        Graph = graph;
        vertices = new TConfiguration[Graph.VerticesCount];
        hasValue = new bool[Graph.VerticesCount];
    }

    public bool HasConfiguration(int node, out TConfiguration configuration)
    {
        if (hasValue[node])
        {
            configuration = vertices[node];
            return true;
        }

        configuration = default(TConfiguration);
        return false;
    }

    public void SetConfiguration(int node, TConfiguration configuration)
    {
        vertices[node] = configuration;
        hasValue[node] = true;
    }

    public void RemoveConfiguration(int node)
    {
        vertices[node] = default(TConfiguration);
        hasValue[node] = false;
    }

    public IEnumerable<TConfiguration> GetAllConfigurations()
    {
        for (var i = 0; i < vertices.Length; i++)
        {
            if (hasValue[i])
            {
                yield return vertices[i];
            }
        }
    }

    public Layout<TConfiguration> SmartClone()
    {
        var layout = new Layout<TConfiguration>(Graph);

        for (var i = 0; i < vertices.Length; i++)
        {
            var configuration = vertices[i];

            if (hasValue[i])
            {
                layout.vertices[i] = configuration.SmartClone();
                layout.hasValue[i] = true;
            }
        }

        return layout;
    }
}
