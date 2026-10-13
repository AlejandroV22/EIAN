#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public static class HacerPoligonosInvisibles
{
    [MenuItem("Tools/Zonas/Hacer todos los UIPolygon invisibles")]
    static void AplicarInvisible()
    {
        UIPolygon[] poligonos = Object.FindObjectsByType<UIPolygon>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        int count = 0;

        foreach (var p in poligonos)
        {
            if (!p.invisible)
            {
                Undo.RecordObject(p, "Hacer polígono invisible");
                p.invisible = true;
                p.SetVerticesDirty();
                EditorUtility.SetDirty(p);
                count++;
            }
        }

        Debug.Log($"[HacerPoligonosInvisibles] {count} polígonos actualizados en la escena activa.");
    }
}
#endif