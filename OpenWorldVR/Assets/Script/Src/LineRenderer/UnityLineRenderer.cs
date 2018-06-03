using UnityEngine;

namespace Assets.Script.Src.LineRenderer
{
    public class UnityLineRenderer : ILineRenderer
    {
        private UnityEngine.LineRenderer _lineRenderer;

        public UnityLineRenderer(Transform parent, float width, Material material)
        {
            CreateLineRenderer(parent);
            _lineRenderer.startWidth = width;
            _lineRenderer.endWidth = width;
            _lineRenderer.material = material;
        }

        private void CreateLineRenderer(Transform parent)
        {
            var bufferGameObject = new GameObject();
            _lineRenderer = bufferGameObject.AddComponent<UnityEngine.LineRenderer>();
            bufferGameObject.transform.SetParent(parent);
        }

        public void ShowLine()
        {
            _lineRenderer.enabled = true;
        }

        public void HideLine()
        {
            _lineRenderer.enabled = false;
        }

        public void UpdatePositions(Vector3 startPosition, Vector3 endPosition)
        {
            _lineRenderer.SetPosition(0, startPosition);
            _lineRenderer.SetPosition(1, endPosition);
        }
    }
}
