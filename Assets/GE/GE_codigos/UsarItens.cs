using UnityEngine;

public class UsarItens : MonoBehaviour
{
    public BauController bau;
    public GeladeiraController geladeira;
    public JB_Inventory inventario;

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
                if (bau != null)
                {
                    bau.AbrirBau();
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
                    bau.FecharBau();
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
    }
}