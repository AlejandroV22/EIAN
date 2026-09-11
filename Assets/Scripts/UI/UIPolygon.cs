using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
[RequireComponent(typeof(CanvasRenderer))]
public class UIPolygon : Graphic
{
    
    public Color vectorColor = new Color(1f, 0f, 0f, 1f);
    [SerializeField]
    private List<Vector2> points = new List<Vector2>()
    {
        new Vector2(-50, -50),
        new Vector2(50, -50),
        new Vector2(0, 50)
    };
    
    public List<Vector2> Points => points;
    

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        if (points == null || points.Count < 3) return;

        for (int i = 0; i < points.Count; i++)
        {
            vh.AddVert(points[i], color, Vector2.zero);
        }

        for (int i = 1; i < points.Count - 1; i++)
        {
            vh.AddTriangle(0, i, i + 1);
        }
    }

    public void MakeOval(float width, float height, int segments = 64)
    {
        points.Clear();
        for (int i = 0; i < segments; i++)
        {
            float angle = 2 * Mathf.PI * i / segments;
            float x = Mathf.Cos(angle) * width / 2f;
            float y = Mathf.Sin(angle) * height / 2f;
            points.Add(new Vector2(x, y));
        }
        SetVerticesDirty();
    }


    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (points == null || points.Count < 2) return;
        Gizmos.color = vectorColor;
        Vector3 offset = transform.position;
        for (int i = 0; i < points.Count; i++)
        {
            Vector3 p1 = transform.TransformPoint(points[i]);
            Vector3 p2 = transform.TransformPoint(points[(i + 1) % points.Count]);
            Gizmos.DrawLine(p1, p2);
        }
    }
    #endif
    
        public void UpdateRectTransformToFit()
    {
        if (points == null || points.Count == 0) return;

        // 1. Encontrar los límites en el espacio local actual
        float minX = float.MaxValue, maxX = float.MinValue;
        float minY = float.MaxValue, maxY = float.MinValue;

        foreach (var p in points)
        {
            if (p.x < minX) minX = p.x;
            if (p.x > maxX) maxX = p.x;
            if (p.y < minY) minY = p.y;
            if (p.y > maxY) maxY = p.y;
        }

        float width = maxX - minX;
        float height = maxY - minY;

        // Si el bounding box es casi nulo, evitar divisiones por cero
        if (width <= 0.001f || height <= 0.001f) return;

        RectTransform rt = rectTransform; // Graphic ya tiene la propiedad rectTransform optimizada

    #if UNITY_EDITOR
        Undo.RecordObjects(new Object[] { rt, this }, "Fit Polygon to RectTransform");
    #endif

        // 2. Determinar la posición central actual de la forma respecto al RectTransform
        Vector2 centerOffset = new Vector2(minX + width * 0.5f, minY + height * 0.5f);

        // 3. Mover el RectTransform a esa nueva posición central
        rt.anchoredPosition += centerOffset;
        rt.sizeDelta = new Vector2(width, height);

        // 4. Re-centrar los vértices en (0,0) local
        for (int i = 0; i < points.Count; i++)
        {
            points[i] -= centerOffset;
        }

        SetVerticesDirty();

    #if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        EditorUtility.SetDirty(rt);
    #endif
    }

}
