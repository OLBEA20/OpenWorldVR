
using UnityEngine;

namespace Assets.Script.Src.LineRenderer
{
    public interface ILineRenderer
    {
        void ShowLine();
        void HideLine();    
        void UpdatePositions(Vector3 startPosition, Vector3 endPosition);
    }
}