using UnityEngine;

namespace Assets.Script.Src.LineRenderer
{
    public class LineRendererFactory {

        public ILineRenderer[] CreateUnityLineRenderers(int numberOfLineRenderers, Transform lineRenderersParent, Material linesMaterial = null, float width = 1f)
        {
            var lineRenderers = new ILineRenderer[numberOfLineRenderers];
            for(var i = 0; i < numberOfLineRenderers; i++)
            {
                lineRenderers[i] = new UnityLineRenderer(lineRenderersParent, width, linesMaterial); 
            }

            return lineRenderers;
        }

        public ILineRenderer CreateUnityLineRenderer(Transform lineRenderParent, Material lineMaterial = null, float width = 1f)
        {
            return new UnityLineRenderer(lineRenderParent, width, lineMaterial);
        }
    }
}
