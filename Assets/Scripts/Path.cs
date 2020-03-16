using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path
{

    public List<Node> nodes = new List<Node>();
    float length = 0f;

    public void SetPathLength() {
        List<Node> calculated = new List<Node>();
        length = 0f;
        for (int i = 0; i < nodes.Count; i++) {
            Node node = nodes[i];
            for (int j = 0; j < node.connections.Count; j++) {
                Node connection = node.connections[j];
                if (nodes.Contains(connection) && !calculated.Contains(connection)) {
                    length += Vector3.Distance(node.transform.position, connection.transform.position);
                }
            }
            calculated.Add(node);
        }
    }

    public string GetPath() {
        return string.Format(
           "Nodes: {0}\nLength: {1}",
           string.Join(
               ", ",
               nodes.Select(node => node.name).ToArray()),
           length);
    }

    
}