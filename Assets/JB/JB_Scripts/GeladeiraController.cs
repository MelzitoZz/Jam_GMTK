using UnityEngine;

public class GeladeiraController : MonoBehaviour
{
    public Sprite fechadaSprite;
    public Sprite abertaSprite;
    public Sprite abertaComLeiteSprite;

    private SpriteRenderer sr;
    public bool aberta = false;
    public bool temLeite = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = fechadaSprite;
    }

    public void AbrirGeladeira()
    {
        aberta = true;
        sr.sprite = temLeite ? abertaComLeiteSprite : abertaSprite;
        Debug.Log("Geladeira aberta!");
    }

    public void FecharGeladeira()
    {
        aberta = false;
        sr.sprite = fechadaSprite;
        Debug.Log("Geladeira fechada!");
    }

    public void ColocarLeite()
    {
        if (aberta && !temLeite)
        {
            temLeite = true;
            sr.sprite = abertaComLeiteSprite;
            Debug.Log("Leite colocado na geladeira!");
        }
    }
}