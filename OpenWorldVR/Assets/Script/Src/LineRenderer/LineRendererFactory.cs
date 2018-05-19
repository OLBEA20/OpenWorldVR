using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererFactory {

    public LineRenderer[] CreateLineRenderers(int numberOfLineRenderers, Material linesMaterial = null, float startWidth = 1f, float endWidth = 1f, bool enabled = false) {
        LineRenderer[] lineRenderers = new LineRenderer[numberOfLineRenderers];
        for(int i = 0; i < numberOfLineRenderers; i++)
        {
            lineRenderers[i] = CreateLineRenderer(linesMaterial, startWidth, endWidth, enabled); 
        }

        return lineRenderers;
    }

    public LineRenderer CreateLineRenderer(Material lineMaterial = null, float startWidth = 1f, float endWidth = 1f, bool enabled = false)
    {
        GameObject bufferGameObject = new GameObject();
        var lineRenderer = bufferGameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
        lineRenderer.enabled = enabled;
        lineRenderer.material = lineMaterial;

        return lineRenderer;
    }
}
