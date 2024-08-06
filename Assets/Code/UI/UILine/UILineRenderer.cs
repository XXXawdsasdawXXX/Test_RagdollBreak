using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.UILine
{
    [RequireComponent(typeof(LineRenderer))]
    public class UILineRenderer : Graphic
    {
        private LineRenderer lineRenderer;

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            if (lineRenderer == null)
            {
                lineRenderer = GetComponent<LineRenderer>();
                if (lineRenderer == null || lineRenderer.positionCount < 2)
                    return;
            }

            for (int i = 0; i < lineRenderer.positionCount - 1; i++)
            {
                Vector3 startPos = lineRenderer.GetPosition(i);
                Vector3 endPos = lineRenderer.GetPosition(i + 1);

                AddVertex(vh, new Vector3(startPos.x, startPos.y), color);
                AddVertex(vh, new Vector3(endPos.x, endPos.y), color);

                vh.AddTriangle(i * 2, i * 2 + 1, i * 2 + 2);
                vh.AddTriangle(i * 2 + 2, i * 2 + 1, i * 2 + 3);
            }
        }

        void AddVertex(VertexHelper vh, Vector3 position, Color color)
        {
            UIVertex vertex = UIVertex.simpleVert;
            vertex.position = position;
            vertex.color = color;
            vh.AddVert(vertex);
        }
    }
}