using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LineDrawer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private LineRenderer currentLine;
    private Vector3 startPos;
    private bool isDrawing = false;

    [Header("Pareja correcta")]
    public Button parejaCorrecta;

    [Header("Opciones")]
    public Color colorCorrecto = Color.green;
    public Color colorIncorrecto = Color.red;

    private bool yaConectado = false; // 🔹 Bloquea múltiples intentos en este botón

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
        if (yaConectado) return; // 🔹 No iniciar si ya está conectado

        // Inicio de la línea en este botón izquierdo
        startPos = Camera.main.ScreenToWorldPoint(transform.position);
        startPos.z = 0;

        GameObject lineObj = new GameObject("LineaTemp");
        currentLine = lineObj.AddComponent<LineRenderer>();
        currentLine.startWidth = 0.05f;
        currentLine.endWidth = 0.05f;
        currentLine.positionCount = 2;
        currentLine.material = new Material(Shader.Find("Sprites/Default"));
        currentLine.startColor = Color.white;
        currentLine.endColor = Color.white;

        currentLine.SetPosition(0, startPos);
        currentLine.SetPosition(1, startPos);

        isDrawing = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (yaConectado) return; // 🔹 No verificar de nuevo si ya se conectó

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

                    yaConectado = true; // 🔹 Bloquea nuevas líneas para este botón

                    // Avisar al GameManager
                    GameManagerParejas.Instance.ParejaCorrectaEncontrada();
                }
                else
                {
                    currentLine.startColor = colorIncorrecto;
                    currentLine.endColor = colorIncorrecto;
                    Destroy(currentLine.gameObject, 0.5f);
                }
            }
            else
            {
                Destroy(currentLine.gameObject);
            }
        }
        else
        {
            Destroy(currentLine.gameObject);
        }

        currentLine = null;
    }
}
