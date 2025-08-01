using UnityEngine;
using UnityEngine.UI;

public class JB_Inventory : MonoBehaviour
{
    public Image[] itemImages; // Arraste aqui as Images dos itens
    private Sprite[] itemSprites;

    void Start()
    {
        itemSprites = new Sprite[itemImages.Length];
        UpdateUI();
    }

    public bool AddItem(Sprite itemSprite)
    {
        for (int i = 0; i < itemSprites.Length; i++)
        {
            if (itemSprites[i] == null)
            {
                itemSprites[i] = itemSprite;
                UpdateUI();
                return true;
            }
        }
        return false; // Inventário cheio
    }

    public void UpdateUI()
    {
        for (int i = 0; i < itemImages.Length; i++)
        {
            itemImages[i].sprite = itemSprites[i];
            itemImages[i].enabled = itemSprites[i] != null;
        }
    }

    // Corrigido para acessar itemSprites
    public Sprite GetItemAt(int index)
    {
        if (index >= 0 && index < itemSprites.Length)
            return itemSprites[index];
        else
            return null;
    }

    // Método para usar o item
    public bool UseItem(Sprite itemSprite)
    {
        if (itemSprite == null) return false;

        // Busca o controlador UsarItens na cena
        UsarItens usarItensController = FindObjectOfType<UsarItens>();
        if (usarItensController != null)
        {
            usarItensController.UseItem(itemSprite);
            RemoveItem(itemSprite);
            UpdateUI();
            return true;
        }
        return false;
    }

    // Remove o item do inventário apos uso
    public void RemoveItem(Sprite itemSprite)
    {
        for (int i = 0; i < itemSprites.Length; i++)
        {
            if (itemSprites[i] == itemSprite)
            {
                itemSprites[i] = null;
                break;
            }
        }
    }
}