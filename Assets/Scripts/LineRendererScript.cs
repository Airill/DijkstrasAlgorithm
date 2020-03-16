using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererScript : MonoBehaviour
{
    Color c1 = Color.yellow;
    Color c2 = Color.red;
    int lengthOfLineRenderer;

    void Start() {
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.2f;
         lineRenderer.positionCount = lengthOfLineRenderer;

        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c2, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;
    }

    public void DrawPath(List<Node> path, Color color) {
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        var points = new Vector3[path.Count];
        lineRenderer.positionCount = points.Length;
        for (int i = 0; i < path.Count; i++) {
            lineRenderer.SetPosition(i, path[i].transform.position);
        }
    }

}
