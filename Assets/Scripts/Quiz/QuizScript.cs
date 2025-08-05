using UnityEngine;
using UnityEngine.SceneManagement;
public class QuizScript : MonoBehaviour
{
    public string[] zonasDelTema;  // Ej: {"Tallo_1", "Tallo_2", "Tallo_3"}

    public void IntentarAbrirQuiz()
    {
        foreach (string zonaID in zonasDelTema)
        {
            if (PlayerPrefs.GetInt("ZonaVisitada_" + zonaID, 0) == 0)
            {
                Debug.Log("Faltan zonas por visitar para este tema.");
                return;
            }
        }

        Debug.Log("Todas las zonas del tema fueron visitadas. Abriendo quiz...");
        SceneManager.LoadScene("Quiz1");
    }
}


