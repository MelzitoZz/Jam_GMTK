using UnityEngine;

public class EstanteController : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite estanteComLivroSprite;

    public bool temLivro = false;

    public void ColocarLivro()
    {
        if (!temLivro)
        {
            temLivro = true;
            sr.sprite = estanteComLivroSprite;
            Debug.Log("Livro colocado na estante.");
        }
    }
}
