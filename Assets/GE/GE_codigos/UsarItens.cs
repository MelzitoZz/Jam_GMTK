using UnityEngine;

public class UsarItens : MonoBehaviour
{
    public BauController bau;
    public GeladeiraController geladeira;
    public EstanteController estante; 
    public JB_Inventory inventario;

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
                    bau.DestrancarBau();
                    fezAcao = true;
                    if (inventario != null)
                    {
                        inventario.RemoveItem(itemSprite);
                        inventario.UpdateUI();
                    }
                }
                break;
            case "BOLA_0":
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
                break;
            case "BONECA_0":
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
                break;
            case "LIVRO_0":
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
                break;
            case "LEITE_0":
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