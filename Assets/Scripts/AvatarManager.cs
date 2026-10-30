using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class AvatarData
{
    public string nombre;      // "Avatar 1"
    public Sprite principiante;
    public Sprite intermedio;
    public Sprite avanzado;
}

public class AvatarManager : MonoBehaviour
{
    [Header("Datos compartidos")]
    public CatalogoAvatares catalogo;

    [Header("Referencias (opcionales según la escena)")]
    public Image imagenAvatar;
    public TextMeshProUGUI nombreAvatar;
    public TMP_Dropdown dropdownAvatar;   

    private const string KeyAvatarSeleccionado = "AvatarSeleccionado";

    void Start()
    {
        if (dropdownAvatar != null)
        {
            int guardado = Mathf.Clamp(PlayerPrefs.GetInt(KeyAvatarSeleccionado, 0), 0, catalogo.avatares.Length - 1);
            dropdownAvatar.value = guardado;
            dropdownAvatar.RefreshShownValue();
            dropdownAvatar.onValueChanged.AddListener(CambiarAvatar);
        }

        ActualizarAvatar();
    }

    public void CambiarAvatar(int indice)
    {
        PlayerPrefs.SetInt(KeyAvatarSeleccionado, indice);
        PlayerPrefs.Save();
        ActualizarAvatar();
    }

    public void ActualizarAvatar()
    {
        if (catalogo == null || catalogo.avatares.Length == 0) return;

        int seleccionado = Mathf.Clamp(PlayerPrefs.GetInt(KeyAvatarSeleccionado, 0), 0, catalogo.avatares.Length - 1);
        AvatarData avatar = catalogo.avatares[seleccionado];

        int aprobados = ContarQuizzesAprobados();

        Sprite forma;
        if (aprobados >= catalogo.quizzesParaAvanzado)
            forma = avatar.avanzado;
        else if (aprobados >= catalogo.quizzesParaIntermedio)
            forma = avatar.intermedio;
        else
            forma = avatar.principiante;

        if (imagenAvatar != null) imagenAvatar.sprite = forma;
        if (nombreAvatar != null) nombreAvatar.text = avatar.nombre;
    }

    int ContarQuizzesAprobados()
    {
        int aprobados = 0;
        foreach (string tema in catalogo.nombresTemas)
        {
            if (PlayerPrefs.GetInt("QuizAprobado_" + tema, 0) == 1)
                aprobados++;
        }
        return aprobados;
    }
}