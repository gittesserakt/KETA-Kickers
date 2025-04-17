using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[System.Serializable]
public class GraphNode
{
    public Vector2 position;
    public string title;
}

[System.Serializable]
public class Graph
{
    public List<GraphNode> nodes = new List<GraphNode>();
}

public class GraphEditorWindow : EditorWindow
{
    private List<GraphNode> nodes = new List<GraphNode>();
    private GraphNode selectedNode = null;
    private Vector2 offset;
    private Vector2 drag;

    [MenuItem("Tools/Graph Dungeon Editor")]
    public static void ShowWindow()
    {
        GetWindow<GraphEditorWindow>("Graph Editor");
    }


    private void OnGUI()
    {
        DrawGrid(20, 0.2f, Color.gray);
        DrawGrid(100, 0.4f, Color.gray);

        DrawNodes();
        ProcessNodeEvents(Event.current);

        if (GUI.changed) Repaint();

        if (GUILayout.Button("Add Node"))
        {
            nodes.Add(new GraphNode
            {
                position = new Vector2(200, 200),
                title = "New Node"
            });
        }
    }

    private void DrawNodes()
    {
        foreach (var node in nodes)
        {
            Rect rect = new Rect(node.position, new Vector2(100, 50));
            GUILayout.BeginArea(rect, GUI.skin.box);
            node.title = EditorGUILayout.TextField(node.title);
            GUILayout.EndArea();
        }
    }

    private void ProcessEvents(Event e)
    {
        drag = Vector2.zero;

        switch (e.type)
        {
            case EventType.MouseDrag:
                if (e.button == 2)
                {
                    drag = e.delta;
                    for (int i = 0; i < nodes.Count; i++)
                    {
                        nodes[i].position += drag;
                    }
                    GUI.changed = true;
                }
                break;
        }
    }

    private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
    {
        int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
        int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

        Handles.BeginGUI();
        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

        for (int i = 0; i < widthDivs; i++)
        {
            Handles.DrawLine(new Vector3(gridSpacing * i, 0, 0), new Vector3(gridSpacing * i, position.height, 0));
        }

        for (int j = 0; j < heightDivs; j++)
        {
            Handles.DrawLine(new Vector3(0, gridSpacing * j, 0), new Vector3(position.width, gridSpacing * j, 0));
        }

        Handles.color = Color.white;
        Handles.EndGUI();
    }

    private void DrawConnection(Vector2 start, Vector2 end)
    {
        Handles.DrawBezier(
            start, end,
            start + Vector2.left * 50f,
            end - Vector2.left * 50f,
            Color.white,
            null,
            2f
        );
    }


    private void ProcessNodeEvents(Event e)
    {
        for (int i = nodes.Count - 1; i >= 0; i--) // Reverse for correct selection
        {
            var node = nodes[i];
            Rect rect = new Rect(node.position, new Vector2(100, 50));

            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0 && rect.Contains(e.mousePosition))
                    {
                        selectedNode = node;
                        offset = node.position - e.mousePosition;
                        GUI.changed = true;
                    }
                    break;

                case EventType.MouseDrag:
                    if (e.button == 0 && selectedNode == node)
                    {
                        node.position = e.mousePosition + offset;
                        e.Use(); // Mark event as used
                        GUI.changed = true;
                    }
                    break;

                case EventType.MouseUp:
                    if (e.button == 0 && selectedNode == node)
                    {
                        selectedNode = null;
                    }
                    break;
            }
        }
    }
}
