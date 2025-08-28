using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class LineDrawer2D : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private LineRenderer currentLine;
    private Vector3 startPos;
    private bool isDrawing = false;

    [Header("Pareja correcta")]
    public Button parejaCorrecta;

    [Header("Opciones")]
    public Color colorCorrecto = Color.green;
    public Color colorIncorrecto = Color.red;

    private bool yaConectado = false;

    [Header("Orden de renderizado")]
    public string sortingLayerName = "UI";
    public int sortingOrder = 100;

    // 🔹 Lista global para poder borrar todas las líneas
    public static List<LineRenderer> lineasActivas = new List<LineRenderer>();

    void Update()
    {
        if (isDrawing && currentLine != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            currentLine.SetPosition(1, mousePos);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (yaConectado) return;

        // ✅ Tomamos la posición de mundo del botón tal cual
        startPos = transform.position;
        startPos.z = 0;

        GameObject lineObj = new GameObject("LineaTemp");
        currentLine = lineObj.AddComponent<LineRenderer>();

        currentLine.startWidth = 0.05f;
        currentLine.endWidth = 0.05f;
        currentLine.positionCount = 2;
        currentLine.material = new Material(Shader.Find("Sprites/Default"));
        currentLine.startColor = Color.white;
        currentLine.endColor = Color.white;

        currentLine.sortingLayerName = sortingLayerName;
        currentLine.sortingOrder = sortingOrder;

        currentLine.SetPosition(0, startPos);
        currentLine.SetPosition(1, startPos);

        isDrawing = true;

        // Guardar en lista global
        lineasActivas.Add(currentLine);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (yaConectado) return;

        isDrawing = false;
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;

        if (obj != null)
        {
            Button botonPadre = obj.GetComponentInParent<Button>();

            if (botonPadre != null)
            {
                bool esCorrecto = botonPadre == parejaCorrecta;

                if (esCorrecto)
                {
                    currentLine.startColor = colorCorrecto;
                    currentLine.endColor = colorCorrecto;

                    yaConectado = true;
                    GameManagerParejas.Instance.ParejaCorrectaEncontrada();
                }
                else
                {
                    currentLine.startColor = colorIncorrecto;
                    currentLine.endColor = colorIncorrecto;
                    Destroy(currentLine.gameObject, 0.5f);
                    lineasActivas.Remove(currentLine);
                }
            }
            else
            {
                Destroy(currentLine.gameObject);
                lineasActivas.Remove(currentLine);
            }
        }
        else
        {
            Destroy(currentLine.gameObject);
            lineasActivas.Remove(currentLine);
        }

        currentLine = null;
    }

    // 🔹 Llamar este método desde el GameManager cuando se muestre el panel de victoria
    public static void BorrarTodasLasLineas()
    {
        foreach (var linea in lineasActivas)
        {
            if (linea != null)
                Destroy(linea.gameObject);
        }
        lineasActivas.Clear();
    }
}