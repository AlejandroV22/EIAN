#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(UIPolygon))]
public class UIPolygonEditor : Editor
{
    private UIPolygon polygon;
    private bool editMode = true;

    // 🔸 Lista de puntos seleccionados
    private HashSet<int> selectedPoints = new HashSet<int>();

    private void OnEnable()
    {
        polygon = (UIPolygon)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);
        if (GUILayout.Button("Add Point"))
        {
            Undo.RecordObject(polygon, "Add Polygon Point");
            polygon.Points.Add(Vector2.zero);
            polygon.SetVerticesDirty();
        }

        if (GUILayout.Button("Remove Last Point") && polygon.Points.Count > 3)
        {
            Undo.RecordObject(polygon, "Remove Polygon Point");
            polygon.Points.RemoveAt(polygon.Points.Count - 1);
            polygon.SetVerticesDirty();
        }

        GUILayout.Space(10);
        editMode = GUILayout.Toggle(editMode, "Edit Points in Scene View", "Button");

        if (editMode)
        {
            GUILayout.Label("Scene Shortcuts:", EditorStyles.helpBox);
            GUILayout.Label("🖱 Click: seleccionar punto", EditorStyles.miniLabel);
            GUILayout.Label("⇧ Shift + Click: agregar/quitar de la selección", EditorStyles.miniLabel);
            GUILayout.Label("Mueve el gizmo gris para arrastrar todos los seleccionados", EditorStyles.miniLabel);
        }
        GUILayout.Space(10);
        if (GUILayout.Button("Adjust RectTransform to Fit Polygon"))
        {
            polygon.UpdateRectTransformToFit();
        }
        GUILayout.Space(10);
        GUILayout.Label("Generate Shapes", EditorStyles.boldLabel);

        if (GUILayout.Button("Generate Circle"))
        {
            Undo.RecordObject(polygon, "Generate Circle");
            polygon.MakeOval(100, 100, 64);
        }

        if (GUILayout.Button("Generate Oval (2:1)"))
        {
            Undo.RecordObject(polygon, "Generate Oval");
            polygon.MakeOval(200, 100, 64);
        }

    }

    private void OnSceneGUI()
    {
        if (!editMode) return;

        Event e = Event.current;
        Transform t = polygon.transform;

        Handles.color = Color.green;
        for (int i = 0; i < polygon.Points.Count; i++)
        {
            Vector3 worldPos = t.TransformPoint(polygon.Points[i]);
            float handleSize = HandleUtility.GetHandleSize(worldPos) * 0.05f;

            // 🔸 Dibuja el punto (azul si seleccionado)
            Handles.color = selectedPoints.Contains(i) ? Color.cyan : Color.green;
            if (Handles.Button(worldPos, Quaternion.identity, handleSize, handleSize, Handles.DotHandleCap))
            {
                if (e.shift)
                {
                    // Shift = añadir o quitar de la selección
                    if (selectedPoints.Contains(i)) selectedPoints.Remove(i);
                    else selectedPoints.Add(i);
                }
                else
                {
                    // Click normal = seleccionar solo ese
                    selectedPoints.Clear();
                    selectedPoints.Add(i);
                }
                e.Use();
            }
        }

        // 🔸 Si hay puntos seleccionados, muestra un gizmo común
        if (selectedPoints.Count > 0)
        {
            Vector3 center = Vector3.zero;
            foreach (int i in selectedPoints)
                center += t.TransformPoint(polygon.Points[i]);
            center /= selectedPoints.Count;

            EditorGUI.BeginChangeCheck();
            Vector3 newCenter = Handles.PositionHandle(center, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(polygon, "Move Polygon Points");

                Vector3 delta = t.InverseTransformVector(newCenter - center);
                foreach (int i in selectedPoints)
                    polygon.Points[i] += (Vector2)delta;

                polygon.SetVerticesDirty();
                EditorUtility.SetDirty(polygon);
            }
        }
    }
}
#endif
