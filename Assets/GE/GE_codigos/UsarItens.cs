using UnityEngine;

public class UsarItens : MonoBehaviour
{
    // Referências aos objetos da cena
    public BauController bau;

    // Controle de itens
    private bool bauAberto = false;
    private bool temBola = false;


    public void UseItem(Sprite itemSprite)
    {
        if (itemSprite == null)
        {
            Debug.LogWarning("Nenhum item selecionado para uso.");
            return;
        }

        string itemName = itemSprite.name;
        Debug.Log("Usando item: " + itemName);

        switch (itemName)
        {
            case "CHAVE_0":
                // Chave abre o baú
                if (!bauAberto)
                {
                    bau.AbrirBau();
                    bauAberto = true;
                    temBola = true; // Baú agora tem a bola
                    Debug.Log("Você abriu o baú! Tem uma bola lá dentro.");
                }
                else
                {
                    Debug.Log("O baú já está aberto.");
                }
                break;
            default:
                Debug.Log("Esse item não faz nada especial.");
                break;
        }
    }
}