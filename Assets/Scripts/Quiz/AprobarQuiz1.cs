using UnityEngine;

public class AprobarQuiz1 : MonoBehaviour
{
    // Nombre del tema/quiz a marcar como aprobado
    public string nombreTema = "quiz1";

    // Método que puedes vincular a un botón UI
    public void AprobarQuiz()
    {
        PlayerPrefs.SetInt("QuizAprobado_" + nombreTema, 1);
        PlayerPrefs.Save(); // Guarda inmediatamente en disco
        Debug.Log("Marcado como aprobado: QuizAprobado_" + nombreTema);
    }
}