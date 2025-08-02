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
    float distancia;
    float distanciaEst;
    float distanciaGel;


    public AudioClip audioConcluido; // Arraste seu som de "concluído" aqui
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
                     distancia = Vector2.Distance(jogador.transform.position, bauDist.transform.position);

                    if(distancia <= distanciaMaxima)
                    {
                        bau.DestrancarBau();
                        fezAcao = true;
                        if (inventario != null)
                        {
                        inventario.RemoveItem(itemSprite);
                        inventario.UpdateUI();
                        }
                    }
                    else
                    {
                        Debug.Log("Não foi possível utilizar a chave.");
                    }  
                }
                break;
            case "BOLA_0":

                    distancia = Vector2.Distance(jogador.transform.position, bauDist.transform.position);

                if(distancia <= distanciaMaxima)
                {
                    if (bau != null && bau.aberto)
                    {   
                        bau.ColocarBola();
                        fezAcao = true;
                        if (inventario != null)
                        {
                            inventario.RemoveItem(itemSprite);
                            inventario.UpdateUI();
                        }
                    }
                    else
                    {
                        Debug.Log("Não foi possível utilizar a bola.");
                    }  
                }
                break;
            case "BONECA_0":
                 distancia = Vector2.Distance(jogador.transform.position, bauDist.transform.position);

                if(distancia <= distanciaMaxima)
                {
                    if (bau != null && bau.aberto)
                    {
                        bau.ColocarBoneca();
                        fezAcao = true;
                        if (inventario != null)
                        {
                            inventario.RemoveItem(itemSprite);
                            inventario.UpdateUI();
                        }
                    }
                    else
                    {
                        Debug.Log("Não foi possível utilizar a boneca.");
                    }  
                }
                
                break;
            case "LIVRO_0":

                 distanciaEst = Vector2.Distance(jogador.transform.position, estanteDist.transform.position);

                if(distanciaEst <= distanciaMaxima)
                {
                   if (estante != null && !estante.temLivro)
                    {
                        estante.ColocarLivro();
                        fezAcao = true;
                        if (inventario != null)
                        {
                            inventario.RemoveItem(itemSprite);
                            inventario.UpdateUI();
                        }
                    } 
                    else
                    {
                        Debug.Log("Não foi possível utilizar o livro.");
                    } 
                }
                
                break;
            case "LEITE_0":
                 distanciaGel = Vector2.Distance(jogador.transform.position, geladeiraDist.transform.position);

                if(distanciaGel <= distanciaMaxima)
                {
                    if (geladeira != null && geladeira.aberta && !geladeira.temLeite)
                    {
                        geladeira.ColocarLeite();
                        fezAcao = true;
                        if (inventario != null)
                        {
                            inventario.RemoveItem(itemSprite);
                            inventario.UpdateUI();
                        }
                    }
                    else
                    {
                        Debug.Log("Não foi possível utilizar o leite.");
                    } 
                }
                
                break;
            default:
                Debug.Log("Esse item não faz nada especial.");
                break;
        }

        // Toca o áudio de concluído se fez alguma ação especial
        if (fezAcao && audioConcluido != null && audioSource != null)
        {
            audioSource.PlayOneShot(audioConcluido);
        }
    }
}