using UnityEngine;

public class BauController : MonoBehaviour
{
    public Sprite fechadoSprite;
    public Sprite abertoSprite;
    public Sprite fechandoSprite;
    public GameObject bolaNoBau;
    public GameObject bonecaNoBau;

    private SpriteRenderer sr;
    public bool aberto = false;
    private bool bolaDentro = false;
    private bool bonecaDentro = false;
    private bool trancado = true;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = fechadoSprite;
        aberto = false;
        bolaDentro = false;
        bonecaDentro = false;
        trancado = true;

        if (bolaNoBau != null)
            bolaNoBau.SetActive(false);

        if (bonecaNoBau != null)
            bonecaNoBau.SetActive(false);
    }

    public void DestrancarBau()
    {
        Debug.Log($"DestrancarBau chamado - trancado antes: {trancado}");
        if (trancado)
        {
            trancado = false;
            aberto = true;
            sr.sprite = abertoSprite;
            Debug.Log("Baú destrancado e aberto.");
        }
        else
        {
            Debug.Log("Baú já está destrancado.");
        }
        Debug.Log($"trancado depois: {trancado}");
    }


    public void ColocarBola()
    {
        if (aberto && !bolaDentro)
        {
            bolaDentro = true;
            if (bolaNoBau != null)
                bolaNoBau.SetActive(true);

            Debug.Log("Bola colocada dentro do baú.");

            VerificarSeTranca();
        }
    }

    public void ColocarBoneca()
    {
        if (aberto && !bonecaDentro)
        {
            bonecaDentro = true;
            if (bonecaNoBau != null)
                bonecaNoBau.SetActive(true);

            Debug.Log("Boneca colocada dentro do baú.");

            VerificarSeTranca();
        }
    }

    private void VerificarSeTranca()
    {
        if (bolaDentro && bonecaDentro)
        {
            TrancarBau();
        }
    }

    private void TrancarBau()
    {
        trancado = true;
        aberto = false;
        sr.sprite = fechandoSprite != null ? fechandoSprite : fechadoSprite;
        Debug.Log("Baú trancado com os dois itens dentro!");
    }

    public void AbrirBau()
    {
        if (!trancado)
        {
            aberto = true;
            sr.sprite = abertoSprite;
            Debug.Log("Baú aberto.");
        }
        else
        {
            Debug.Log("Baú está trancado, destranque primeiro.");
        }
    }

    public void FecharBau()
    {
        if (aberto && !trancado)
        {
            aberto = false;
            sr.sprite = fechandoSprite != null ? fechandoSprite : fechadoSprite;

            if (bolaNoBau != null)
                bolaNoBau.SetActive(false);
            if (bonecaNoBau != null)
                bonecaNoBau.SetActive(false);

            Debug.Log("Baú fechado!");
        }
        else
        {
            Debug.Log("Não é possível fechar o baú porque está trancado ou já fechado.");
        }
    }

    public bool IsTrancado()
    {
        return trancado;
    }
}
