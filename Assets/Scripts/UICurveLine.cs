using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[ExecuteAlways]
[RequireComponent(typeof(CanvasRenderer))]
public class UICurveLine : Graphic
{
    public Vector2 startPoint = new Vector2(-100, 0);
    public Vector2 controlPoint = new Vector2(0, 100);
    public Vector2 endPoint = new Vector2(100, 0);

    public float thickness = 12f;
    public int segments = 30;

    private List<Vector2> curvePoints = new List<Vector2>();

    public float raycastPadding = 6f;


    public override bool Raycast(Vector2 sp, Camera eventCamera)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            sp,
            eventCamera,
            out Vector2 localPoint
        );

        if (curvePoints == null || curvePoints.Count < 2)
            return false;

        float maxDist = thickness * 0.5f + raycastPadding;

        for (int i = 0; i < curvePoints.Count - 1; i++)
        {
            float dist = DistancePointToSegment(localPoint, curvePoints[i], curvePoints[i + 1]);
            if (dist <= maxDist)
                return true;
        }

        return false;
    }
    float DistancePointToSegment(Vector2 p, Vector2 a, Vector2 b)
        {
            Vector2 ab = b - a;
            float t = Vector2.Dot(p - a, ab) / ab.sqrMagnitude;
            t = Mathf.Clamp01(t);
            Vector2 closest = a + ab * t;
            return Vector2.Distance(p, closest);
        }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        GenerateCurve();

        if (curvePoints.Count < 2)
            return;

        float half = thickness * 0.5f;

        for (int i = 0; i < curvePoints.Count; i++)
        {
            Vector2 dir;

            if (i == curvePoints.Count - 1)
                dir = (curvePoints[i] - curvePoints[i - 1]).normalized;
            else
                dir = (curvePoints[i + 1] - curvePoints[i]).normalized;

            Vector2 normal = new Vector2(-dir.y, dir.x) * half;

            vh.AddVert(curvePoints[i] + normal, color, Vector2.zero);
            vh.AddVert(curvePoints[i] - normal, color, Vector2.zero);
        }

        for (int i = 0; i < curvePoints.Count - 1; i++)
        {
            int index = i * 2;

            vh.AddTriangle(index, index + 1, index + 2);
            vh.AddTriangle(index + 1, index + 3, index + 2);
        }
    }

    void GenerateCurve()
    {
        curvePoints.Clear();

        for (int i = 0; i <= segments; i++)
        {
            float t = i / (float)segments;

            Vector2 p =
                Mathf.Pow(1 - t, 2) * startPoint +
                2 * (1 - t) * t * controlPoint +
                Mathf.Pow(t, 2) * endPoint;

            curvePoints.Add(p);
        }
    }
    public void UpdateRectTransformToFit()
    {
        GenerateCurve();

        if (curvePoints == null || curvePoints.Count == 0) 
            return;

        float minX = float.MaxValue;
        float maxX = float.MinValue;
        float minY = float.MaxValue;
        float maxY = float.MinValue;

        foreach (var p in curvePoints)
        {
            if (p.x < minX) minX = p.x;
            if (p.x > maxX) maxX = p.x;
            if (p.y < minY) minY = p.y;
            if (p.y > maxY) maxY = p.y;
        }

        float halfThickness = thickness * 0.5f;

        // Expandimos el bounding box para incluir el grosor de la línea
        minX -= halfThickness;
        maxX += halfThickness;
        minY -= halfThickness;
        maxY += halfThickness;

        float width = maxX - minX;
        float height = maxY - minY;

        RectTransform rt = rectTransform;

    #if UNITY_EDITOR
        UnityEditor.Undo.RecordObject(rt, "Fit Curve RectTransform");
    #endif

        Vector2 offset = new Vector2(minX + width * 0.5f, minY + height * 0.5f);

        rt.sizeDelta = new Vector2(width, height);
        rt.anchoredPosition += offset;

        startPoint -= offset;
        controlPoint -= offset;
        endPoint -= offset;

        GenerateCurve();
        SetVerticesDirty();
    }
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 s = transform.TransformPoint(startPoint);
        Vector3 c = transform.TransformPoint(controlPoint);
        Vector3 e = transform.TransformPoint(endPoint);

        Gizmos.DrawSphere(s, 4);
        Gizmos.DrawSphere(c, 4);
        Gizmos.DrawSphere(e, 4);

        Gizmos.DrawLine(s, c);
        Gizmos.DrawLine(c, e);
    }
#endif
}