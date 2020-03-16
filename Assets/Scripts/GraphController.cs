﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GraphController : MonoBehaviour
{
    public Node startNode;
    public Node endNode;
    public List<Node> m_Nodes;
    List<Node> visitedNodes;
    float dist;
    public Linerenderer lineRenderer;
    private Path finalPath;

    private void Start() {
        finalPath = GetPath(startNode, endNode);
        Debug.Log(finalPath.GetPath());
        lineRenderer.Drawline(finalPath.nodes);
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

        for (int i = 0; i < m_Nodes.Count; i++) {
            Node node = m_Nodes[i];
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

}
