using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UICurveLine))]
public class UICurveLineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        UICurveLine curve = (UICurveLine)target;

        if (GUILayout.Button("Fit RectTransform to Curve"))
        {
            curve.UpdateRectTransformToFit();
        }
    }
}