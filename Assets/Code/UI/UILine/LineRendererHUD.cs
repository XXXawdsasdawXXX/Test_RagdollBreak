using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.UILine
{
    public class LineRendererHUD : Graphic 
    {
        [SerializeField] private float _scalerX = 1, _scalerY = 1;
        [SerializeField] private UIGridRenderer _grid;
        public float thickness;

        public List<Vector2> points;

        private Vector2Int gridSize = new Vector2Int(1,1);
        private float width;
        private float height;
        private float unitWidth;
        private float unitHeight;

        protected override void OnPopulateMesh(VertexHelper vh) 
        {
            vh.Clear();

            width = rectTransform.rect.width;
            height = rectTransform.rect.height;

            unitWidth = width / gridSize.x;
            unitHeight = height / gridSize.y;

            if (points.Count < 2) return;

            for (int i = 0; i < points.Count - 1; i++) 
            {
                Vector2 point = points[i];
                Vector2 point2 = points[i + 1];

                if (IsValidPoint(point) && IsValidPoint(point2))
                {
                    DrawVerticesForPoint(point, point2, vh);
                }
            }

            int vertexCount = vh.currentVertCount;

            for (int i = 0; i < points.Count - 1; i++) 
            {
                int index = i * 4;
                if (index + 3 < vertexCount)
                {
                    vh.AddTriangle(index + 0, index + 1, index + 2);
                    vh.AddTriangle(index + 1, index + 2, index + 3);
                }
            }
        }

        public void SetGridRenderer(UIGridRenderer grid)
        {
            _grid = grid;
        }

        public void SetPosition(int index, Vector2 p)
        {
            Vector2 newPoint = new Vector2(p.x / unitWidth, p.y / unitHeight);
            points[index] = newPoint;
            SetVerticesDirty();
        }

        public void AddPosition(Vector2 p)
        {
            Vector2 newPoint = new Vector2(p.x / _scalerX, p.y / _scalerY);
            points.Add(newPoint);
            SetVerticesDirty();
        }

        public void AddPosition(float currentYValue)
        {
            Vector2 newPoint = new Vector2(5 * points.Count / _scalerX, currentYValue / _scalerY);
            points.Add(newPoint);
            SetVerticesDirty();
        }

        public void ResetLine()
        {
            gridSize = _grid.Size;
            points.Clear();
            SetVerticesDirty();
        }

        public void SetColor(Color fruitDataColor)
        {
            color = fruitDataColor;
        }
        
        private float GetAngle(Vector2 me, Vector2 target) 
        {
            return Mathf.Atan2(target.y - me.y, target.x - me.x) * Mathf.Rad2Deg;
        }

        private void DrawVerticesForPoint(Vector2 point, Vector2 point2, VertexHelper vh) 
        {
            if(IsVectorNegativeInfinity(point) || IsVectorNegativeInfinity(point2))
            {
                return;   
            }
        
            UIVertex vertex = UIVertex.simpleVert;
            vertex.color = color;

            Vector3 pos1 = new Vector3(unitWidth * point.x, unitHeight * point.y);
            Vector3 pos2 = new Vector3(unitWidth * point2.x, unitHeight * point2.y);

            Vector3 direction = (pos2 - pos1).normalized;
            Vector3 perpendicular = new Vector3(-direction.y, direction.x) * (thickness / 2);

            vertex.position = pos1 + perpendicular;
            vh.AddVert(vertex);

            vertex.position = pos1 - perpendicular;
            vh.AddVert(vertex);

            vertex.position = pos2 + perpendicular;
            vh.AddVert(vertex);

            vertex.position = pos2 - perpendicular;
            vh.AddVert(vertex);
        }

        private bool IsValidPoint(Vector2 point)
        {
            return !float.IsNaN(point.x) && !float.IsNaN(point.y) &&
                   !float.IsInfinity(point.x) && !float.IsInfinity(point.y);
        }

        private bool IsVectorNegativeInfinity(Vector3 vector)
        {
            return float.IsNegativeInfinity(vector.x) &&
                   float.IsNegativeInfinity(vector.y) &&
                   float.IsNegativeInfinity(vector.z);
        }
    }
}
