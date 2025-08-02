using UnityEngine;

public class UsarItens : MonoBehaviour
{
    public BauController bau;
    public GeladeiraController geladeira;
    public EstanteController estante;
    public JB_Inventory inventario;
    public GameObject jogador;
    public GameObject bauDist;
    public GameObject estanteDist;
    public GameObject geladeiraDist;
    public float distanciaMaxima = 10f;

    public AudioClip audioConcluido;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void UseItem(Sprite itemSprite)
    {
        if (itemSprite == null)
        {
            Debug.LogWarning("Nenhum item selecionado para uso.");
            return;
        }

        string itemName = itemSprite.name;
        Debug.Log("Usando item: " + itemName);

        bool fezAcao = false;

        switch (itemName)
        {
            case "CHAVE_0":
                if (bau != null)
                {
                    float distancia = Vector2.Distance(jogador.transform.position, bauDist.transform.position);
                    if (distancia <= distanciaMaxima)
                    {
                        bau.DestrancarBau();
                        fezAcao = true;
                        RemoverItem(itemSprite);
                    }
                    else Debug.Log("Você está longe demais do baú.");
                }
                break;

            case "BOLA_0":
                if (bau != null && bau.aberto)
                {
                    float distancia = Vector2.Distance(jogador.transform.position, bauDist.transform.position);
                    if (distancia <= distanciaMaxima)
                    {
                        bau.ColocarBola();
                        fezAcao = true;
                        RemoverItem(itemSprite);
                    }
                    else Debug.Log("Você está longe demais do baú.");
                }
                break;

            case "BONECA_0":
                if (bau != null && bau.aberto)
                {
                    float distancia = Vector2.Distance(jogador.transform.position, bauDist.transform.position);
                    if (distancia <= distanciaMaxima)
                    {
                        bau.ColocarBoneca();
                        fezAcao = true;
                        RemoverItem(itemSprite);
                    }
                    else Debug.Log("Você está longe demais do baú.");
                }
                break;

            case "LIVRO_0":
                if (estante != null && !estante.temLivro)
                {
                    float distancia = Vector2.Distance(jogador.transform.position, estanteDist.transform.position);
                    if (distancia <= distanciaMaxima)
                    {
                        estante.ColocarLivro();
                        fezAcao = true;
                        RemoverItem(itemSprite);
                    }
                    else Debug.Log("Você está longe demais da estante.");
                }
                break;

            case "LEITE_0":
                if (geladeira != null && geladeira.aberta && !geladeira.temLeite)
                {
                    float distancia = Vector2.Distance(jogador.transform.position, geladeiraDist.transform.position);
                    if (distancia <= distanciaMaxima)
                    {
                        geladeira.ColocarLeite();
                        fezAcao = true;
                        RemoverItem(itemSprite);
                    }
                    else Debug.Log("Você está longe demais da geladeira.");
                }
                break;

            case "XICARA DE CAFE_0":
                Debug.Log("Você tomou a xícara de café!");
                fezAcao = true;
                RemoverItem(itemSprite);
                break;

            default:
                Debug.Log("Esse item não faz nada especial.");
                break;
        }

        if (fezAcao && audioConcluido != null)
            audioSource.PlayOneShot(audioConcluido);
    }

    private void RemoverItem(Sprite itemSprite)
    {
        if (inventario != null)
        {
            inventario.RemoveItem(itemSprite);
            inventario.UpdateUI();
        }
    }
}
