using UnityEngine;

using UnityEditor;
using System.Collections.Generic;
using System.Linq;


[System.Serializable]
public class VisualVertex<TNode>
{
    public TNode node;
    public Rect Rect;
    public bool IsDragging;

    public VisualVertex(TNode node, Vector2 position)
    {
        this.node = node;
        Rect = new Rect(position.x, position.y, 100, 40);
        IsDragging = false;
    }
}

public class GraphBuilderWindow : EditorWindow{
    private List<VisualVertex<int>> vertices = new List<VisualVertex<int>>();
    private List<(int from, int to)> edges = new List<(int, int)>();

    private Vector2 mouseOffset;
    private VisualVertex<int> selectedVertex;
    private VisualVertex<int> dragStartVertex;
    private bool isDraggingEdge = false;

    private int nextVertextCount = 0;

    [MenuItem("Tools/Graph Builder")]
    public static void ShowWindow()
    {
        GetWindow<GraphBuilderWindow>("Graph Builder");
    }

    private void OnGUI()
    {
        DrawToolbar();
        DrawEdges();
        ProcessEvents(Event.current);
        DrawVertices();

        if (GUI.changed)
            Repaint();
    }

    private void DrawToolbar()
    {
        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        if (GUILayout.Button("Add Vertex", EditorStyles.toolbarButton))
        {
            AddVertexAtPosition(new Vector2(100, 100));
        }

        if (GUILayout.Button("Clear", EditorStyles.toolbarButton))
        {
            ClearGraph();
        }

        if (GUILayout.Button("Create graph (and run BFS)", EditorStyles.toolbarButton))
        {
            CreateGraph();
        }

        GUILayout.EndHorizontal();
    }

    private void CreateGraph()
    {
        DungeonGraph<int> graph = new DungeonGraph<int>();
        foreach (VisualVertex<int> vertex in vertices)
        {
            graph.AddVertex(vertex.node);
        }
        foreach ((int from, int to) edge in edges)
        {
            graph.AddEdge(edge.from, edge.to);
        }
        ChainDecompositionConfiguration configuration = new ChainDecompositionConfiguration();
        BreadthFirstChainDecomposition<int> decomposer = new BreadthFirstChainDecomposition<int>(configuration);
        decomposer.GetChains(graph);
    }

    private void ClearGraph()
    {
        vertices.Clear();
        edges.Clear();
        nextVertextCount = 0;
    }

    private void RemoveVertex(VisualVertex<int> vertex)
    {
        vertices.Remove(vertex);
        edges.RemoveAll(e => e.from == vertex.node || e.to == vertex.node);
    }

    private void DrawVertices()
    {
        foreach (var v in vertices)
        {
            GUI.Box(v.Rect, "V" + v.node.ToString(), EditorStyles.helpBox);
        }
    }

    private void DrawEdges()
    {
        foreach (var edge in edges)
        {
            var from = vertices.FirstOrDefault(v => v.node == edge.from);
            var to = vertices.FirstOrDefault(v => v.node == edge.to);
            if (from != null && to != null)
            {
                Handles.DrawLine(from.Rect.center, to.Rect.center);
            }
        }

        if (isDraggingEdge && dragStartVertex != null)
        {
            Handles.DrawLine(dragStartVertex.Rect.center, Event.current.mousePosition);
        }
    }

    private void ShowVertexContextMenu(VisualVertex<int> vertex)
    {
        GenericMenu menu = new GenericMenu();

        menu.AddItem(new GUIContent("Delete Vertex"), false, () =>
        {
            RemoveVertex(vertex);
        });

        menu.AddItem(new GUIContent("Start Connecting Edge"), false, () =>
        {
            dragStartVertex = vertex;
            isDraggingEdge = true;
        });

        menu.ShowAsContext();
    }



    private void ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                selectedVertex = GetVertexAtPoint(e.mousePosition);

                if (e.button == 1 && selectedVertex == null)
                {
                    ShowBackgroundContextMenu(e.mousePosition);
                    e.Use();
                }
                else if (selectedVertex != null)
                {
                    if (e.button == 0)
                    {
                        selectedVertex.IsDragging = true;
                        mouseOffset = e.mousePosition - selectedVertex.Rect.position;
                    }

                    if (e.button == 1)
                    {
                        ShowVertexContextMenu(selectedVertex);
                        e.Use();
                    }
                }
                break;

            case EventType.MouseDrag:
                if (selectedVertex != null && selectedVertex.IsDragging)
                {
                    selectedVertex.Rect.position = e.mousePosition - mouseOffset;
                    GUI.changed = true;
                }
                break;

            case EventType.MouseUp:
                if (selectedVertex != null)
                {
                    selectedVertex.IsDragging = false;
                }

                if (isDraggingEdge)
                {
                    var targetVertex = GetVertexAtPoint(e.mousePosition);
                    if (targetVertex != null && targetVertex != dragStartVertex)
                    {
                        if (!edges.Contains((targetVertex.node, dragStartVertex.node))
                            || !edges.Contains((targetVertex.node, dragStartVertex.node)))
                        {
                            edges.Add((dragStartVertex.node, targetVertex.node));
                        }
                    }

                    isDraggingEdge = false;
                    dragStartVertex = null;
                    GUI.changed = true;
                }
                break;
        }
    }

    private void ShowBackgroundContextMenu(Vector2 mousePosition)
    {
        GenericMenu menu = new GenericMenu();

        menu.AddItem(new GUIContent("Add Vertex"), false, () =>
        {
            AddVertexAtPosition(mousePosition);
        });

        menu.ShowAsContext();
    }

    private void AddVertexAtPosition(Vector2 position)
    {
        int node = nextVertextCount++;
        vertices.Add(new VisualVertex<int>(node, position));
    }

    private VisualVertex<int> GetVertexAtPoint(Vector2 point)
    {
        return vertices.FindLast(v => v.Rect.Contains(point));
    }
}