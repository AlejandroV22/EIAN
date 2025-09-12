using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AvatarManager : MonoBehaviour
{
    [Header("Referencias del Avatar")]
    public Image imagenAvatar;
    public TextMeshProUGUI nombreAvatar;

    [Header("Avatares por progreso")]
    public Sprite[] imagenesAvatares;      // imágenes diferentes
    public string[] nombresAvatares;       // nombres diferentes

    [Header("Temas del quiz (en orden)")]
    public string[] nombresTemas;          // 

    void Start()
    {
        ActualizarAvatar();
    }

    public void ActualizarAvatar()
    {
        // Contar cuántos quizzes están aprobados
        int aprobados = 0;

        foreach (string tema in nombresTemas)
        {
            if (PlayerPrefs.GetInt("QuizAprobado_" + tema, 0) == 1)
            {
                aprobados++;
                Debug.Log(aprobados);
            }
        }

        // Calcular índice de avatar
        int index = Mathf.Clamp(aprobados, 0, imagenesAvatares.Length - 1);

        // Asignar imagen y nombre
        if (imagenesAvatares.Length > 0 && index < imagenesAvatares.Length)
        {
            imagenAvatar.sprite = imagenesAvatares[index];
        }

        if (nombresAvatares.Length > 0 && index < nombresAvatares.Length)
        {
            nombreAvatar.text = nombresAvatares[index];
        }
    }
}