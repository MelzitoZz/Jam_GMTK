using UnityEngine;
using UnityEngine.UI;

public class JB_Inventory : MonoBehaviour
{
    public Image[] itemImages; // Arraste aqui as Images dos itens (filhos dos slots)
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

    void UpdateUI()
    {
        for (int i = 0; i < itemImages.Length; i++)
        {
            itemImages[i].sprite = itemSprites[i];
            itemImages[i].enabled = itemSprites[i] != null;
        }
    }
}