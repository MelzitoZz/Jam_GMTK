using UnityEngine;
using UnityEngine.UI;

public class ExitButtonEffect : MonoBehaviour
{
    [Header("Sprites do Botão")]
    public Sprite normalSprite;
    public Sprite clickedSprite;
    [Header("Tempo para voltar ao normal")]
    public float timeToReturn = 0.2f;

    private Image buttonImage;
    private bool isClicked = false;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        if (normalSprite != null)
            buttonImage.sprite = normalSprite;
    }

    public void OnButtonClick()
    {
        if (isClicked) return;
        isClicked = true;
        if (clickedSprite != null)
            buttonImage.sprite = clickedSprite;

        Invoke(nameof(ReturnToNormal), timeToReturn);

        // Fecha o jogo (funciona apenas em build)
#if UNITY_EDITOR
        Debug.Log("Encerrando a playmode do Editor (saída de jogo simulada)");
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void ReturnToNormal()
    {
        if (normalSprite != null)
            buttonImage.sprite = normalSprite;
        isClicked = false;
    }
}