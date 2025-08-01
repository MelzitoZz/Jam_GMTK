using UnityEngine;

public class BauController : MonoBehaviour
{
    public Sprite fechadoSprite;      // Sprite do baú fechado
    public Sprite abertoSprite;       // Sprite do baú aberto (com bola dentro)
    public Sprite fechandoSprite;     // Sprite do baú fechando (opcional, pode ser igual ao fechado)
    public GameObject bolaNoBau;      // Referência para o objeto Bola dentro do Baú
    private SpriteRenderer sr;
    public bool aberto = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = fechadoSprite;
        if (bolaNoBau != null)
            bolaNoBau.SetActive(false);
    }

    public void AbrirBau()
    {
        if (!aberto)
        {
            aberto = true;
            sr.sprite = abertoSprite;
            if (bolaNoBau != null)
                bolaNoBau.SetActive(true); // Mostra a bola ao abrir
            Debug.Log("Baú aberto!");
        }
        else
        {
            Debug.Log("O baú já estava aberto.");
        }
    }

    public void FecharBau()
    {
        if (aberto)
        {
            aberto = false;
            sr.sprite = fechandoSprite != null ? fechandoSprite : fechadoSprite;
            if (bolaNoBau != null)
                bolaNoBau.SetActive(false); // Esconde a bola ao fechar
            Debug.Log("Baú fechado!");
        }
    }
}