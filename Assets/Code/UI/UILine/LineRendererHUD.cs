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

        
            float angle = 0;
            for (int i = 0; i < points.Count - 1; i++) {

                Vector2 point = points[i];
                Vector2 point2 = points[i+1];

                if (i < points.Count - 1) {
                    angle = GetAngle(points[i], points[i + 1]) + 90f;
                }

                DrawVerticesForPoint(point, point2, angle, vh);
            }

            for (int i = 0; i < points.Count - 1; i++) {
                int index = i * 4;
                vh.AddTriangle(index + 0, index + 1, index + 2);
                vh.AddTriangle(index + 1, index + 2, index + 3);
            }
        
        }

        public void SetGridRenderer(UIGridRenderer grid)
        {
            _grid = grid;
        }

        public void SetPosition(int index,Vector2 p)
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
        
        private float GetAngle(Vector2 me, Vector2 target) {
            return (float)(Mathf.Atan2(9f*(target.y - me.y), 16f*(target.x - me.x)) * Mathf.Rad2Deg);
        }
        
        private void DrawVerticesForPoint(Vector2 point, Vector2 point2, float angle, VertexHelper vh) {
            UIVertex vertex = UIVertex.simpleVert;
            vertex.color = color;

            vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(-thickness / 2, 0);
            vertex.position += new Vector3(unitWidth * point.x, unitHeight * point.y);
            vh.AddVert(vertex);

            vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(thickness / 2, 0);
            vertex.position += new Vector3(unitWidth * point.x, unitHeight * point.y);
            vh.AddVert(vertex);

            vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(-thickness / 2, 0);
            vertex.position += new Vector3(unitWidth * point2.x, unitHeight * point2.y);
            vh.AddVert(vertex);

            vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(thickness / 2, 0);
            vertex.position += new Vector3(unitWidth * point2.x, unitHeight * point2.y);
            vh.AddVert(vertex);
        }
    }
}