using UnityEngine;

public class TestChainDecomposition : MonoBehaviour
{
    DungeonGraph<int> dungeonGraph = new DungeonGraph<int>();
    BreadthFirstChainDecomposition<int> decomposer;
    ChainDecompositionConfiguration configuration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        configuration = new ChainDecompositionConfiguration();
        decomposer = new BreadthFirstChainDecomposition<int>(configuration);
        for (int i = 0; i < 5; i++)
        {
            dungeonGraph.AddVertex(i);
        }
        dungeonGraph.AddEdge(0, 1);
        dungeonGraph.AddEdge(1,2);
        dungeonGraph.AddEdge(2, 3);
        dungeonGraph.AddEdge(3, 0);
        dungeonGraph.AddEdge(4, 0);

        var chains = decomposer.GetChains(dungeonGraph);
        foreach (var ch in chains)
        {
            Debug.Log(ch.ToString());
        }

    }
}
