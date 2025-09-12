using UnityEngine;

public class PlayerPrefsChecker_AlejandroVelandia : MonoBehaviour
{
    void Start()
    {
        Debug.Log("=== Estado de PlayerPrefs ===");
        Debug.Log("Quiz1: " + PlayerPrefs.GetInt("QuizAprobado_quiz1", 0));
        Debug.Log("Quiz2: " + PlayerPrefs.GetInt("QuizAprobado_quiz2", 0));
        Debug.Log("Quiz3: " + PlayerPrefs.GetInt("Quiz3_quiz3", 0));
        Debug.Log("Avatar: " + PlayerPrefs.GetInt("Avatar", 0));
        Debug.Log("RutaDesbloqueada: " + PlayerPrefs.GetInt("Ruta", 0));
    }
}
