using UnityEngine;
using UnityEngine.UI;

public class JB_Inventory : MonoBehaviour
{
    public Image[] itemImages;
    private Sprite[] itemSprites;

    public BackgroundAudioManager backgroundAudioManager;
    private bool overlayAudioParado = false;

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

                // Verifica se pegou boneca e bola
                PararOverlaySePegouBonecaEBola();

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

    public Sprite GetItemAt(int index)
    {
        if (index >= 0 && index < itemSprites.Length)
            return itemSprites[index];
        else
            return null;
    }

    public bool UseItem(Sprite itemSprite)
    {
        if (itemSprite == null) return false;

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
    private void PararOverlaySePegouBonecaEBola()
    {
        if (overlayAudioParado || backgroundAudioManager == null)
            return;

        bool temBoneca = false, temBola = false;

        foreach (Sprite s in itemSprites)
        {
            if (s != null)
            {
                if (s.name == "BONECA_0") temBoneca = true;
                if (s.name == "BOLA_0") temBola = true;
            }
        }

        if (temBoneca && temBola)
        {
            backgroundAudioManager.StopOverlayAudio(9);
            overlayAudioParado = true;
        }
    }
}