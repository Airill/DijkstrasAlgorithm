using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GraphController : MonoBehaviour
{
    public Node startNode;
    public Node endNode;
    public List<Node> inputNodes;
    List<Node> visitedNodes;
    float dist;
    public LineRendererScript pathLineRenderer;
    public LineRendererScript bridgesLineRenderer;

    private Path finalPath;

    private void Start() {
        GetInputNodes();

       // DrawAllBridges(inputNodes);

        finalPath = GetPath(startNode, endNode);
        Debug.Log(finalPath.GetPath());
        pathLineRenderer.DrawPath(finalPath.nodes);
    }

    void GetInputNodes() {
        inputNodes = gameObject.GetComponentsInChildren<Node>().ToList();
    }

    public Path GetPath(Node start, Node end) {

        if (start == null || end == null) {
            throw new ArgumentNullException();
        }

       Path path = new Path();

        if (start == end) {
            path.nodes.Add(start);

            return path;
        }

        List<Node> unvisited = new List<Node>();

        Dictionary<Node, Node> previous = new Dictionary<Node, Node>();

         Dictionary<Node, float> distances = new Dictionary<Node, float>();

        for (int i = 0; i < inputNodes.Count; i++) {
            Node node = inputNodes[i];
            unvisited.Add(node);
            distances.Add(node, float.MaxValue);
        }

        distances[start] = 0f;
        while (unvisited.Count != 0) {

            unvisited = unvisited.OrderBy(node => distances[node]).ToList();

            Node current = unvisited[0];

            unvisited.Remove(current);

            if (current == end) {
                while (previous.ContainsKey(current)) {
                    path.nodes.Insert(0, current);
                    current = previous[current];
                }
                path.nodes.Insert(0, current);
                break;
            }

            for (int i = 0; i < current.connections.Count; i++) {
                Node neighbor = current.connections[i];
                float length = Vector3.Distance(current.transform.position, neighbor.transform.position);
                float alt = distances[current] + length;
                if (alt < distances[neighbor]) {
                    distances[neighbor] = alt;
                    previous[neighbor] = current;
                }
            }
        }
        path.SetPathLength();
        return path;
    }

    void DrawAllBridges(List<Node> nodes) {
        //  List<Vector3> points = new List<Vector3>();
        List<Node> points = new List<Node>();
        points.Add(startNode);

        foreach (Node n in nodes) {
            List<Node> con = n.connections;
            foreach ( Node c in con) {
                points.Add(c);
            }
            points.Add(endNode);
            bridgesLineRenderer.DrawBridges(points);
        }
    }
}


