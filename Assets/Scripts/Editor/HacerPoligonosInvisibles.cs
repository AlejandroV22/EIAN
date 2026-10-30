#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public static class HacerPoligonosInvisibles
{
    [MenuItem("Tools/Zonas/Hacer todos los UIPolygon invisibles")]
    static void HacerInvisibles()
    {
        CambiarVisibilidad(true);
    }

    [MenuItem("Tools/Zonas/Hacer todos los UIPolygon visibles")]
    static void HacerVisibles()
    {
        CambiarVisibilidad(false);
    }

    static void CambiarVisibilidad(bool invisible)
    {
        UIPolygon[] poligonos = Object.FindObjectsByType<UIPolygon>(FindObjectsInactive.Include);
        int count = 0;

        foreach (var p in poligonos)
        {
            if (p.invisible != invisible)
            {
                Undo.RecordObject(p, invisible ? "Hacer polígono invisible" : "Hacer polígono visible");
                p.invisible = invisible;
                p.SetVerticesDirty();
                EditorUtility.SetDirty(p);
                count++;
            }
        }

        string estado = invisible ? "invisibles" : "visibles";
        Debug.Log($"[HacerPoligonosInvisibles] {count} polígonos ahora {estado} en la escena activa.");
    }
}
#endif