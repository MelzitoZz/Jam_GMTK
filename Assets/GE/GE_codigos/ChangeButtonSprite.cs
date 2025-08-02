using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeButtonSpriteAndScene : MonoBehaviour
{
    public Sprite normalSprite;
    public Sprite clickedSprite;
    public string sceneToLoad;
    public float timeToReturn = 0.2f;

    private Image buttonImage;
    private bool isClicked = false;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = normalSprite;
    }

    public void OnButtonClick()
    {
        if (isClicked) return;
        isClicked = true;
        buttonImage.sprite = clickedSprite;
        Invoke(nameof(ReturnToNormal), timeToReturn);
        SceneManager.LoadScene(sceneToLoad);
    }

    void ReturnToNormal()
    {
        buttonImage.sprite = normalSprite;
        isClicked = false;
    }
}