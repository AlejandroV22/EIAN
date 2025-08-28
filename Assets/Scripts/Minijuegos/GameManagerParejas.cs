using UnityEngine;

public class GameManagerParejas : MonoBehaviour
{
    public static GameManagerParejas Instance;

    [Header("Configuración")]
    public int totalParejas = 4; // 🔹 Número total de parejas en la escena
    private int parejasCorrectas = 0;

    [Header("UI")]
    public GameObject panelVictoria; // 🔹 Asignar en Inspector

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ParejaCorrectaEncontrada()
    {
        parejasCorrectas++;
        Debug.Log("Parejas correctas: " + parejasCorrectas);

        if (parejasCorrectas >= totalParejas)
        {
            Victoria();
        }
    }

    private void Victoria()
    {
        Debug.Log("¡Has ganado!");
        if (panelVictoria != null)
            LineDrawer2D.BorrarTodasLasLineas();
            panelVictoria.SetActive(true);

    }
}
