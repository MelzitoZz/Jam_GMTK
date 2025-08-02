using UnityEngine;
using UnityEngine.UI;

public class JB_Inventory : MonoBehaviour
{
    public Image[] itemImages;
    private Sprite[] itemSprites;

    public BackgroundAudioManager backgroundAudioManager;

    public AudioClip audioTaskCompleta;
    private AudioSource audioSource;

    // Travas individuais para cada task
    private bool taskBonecaBolaCompleta = false;
    private bool taskCrachaCompleta = false;

    void Start()
    {
        itemSprites = new Sprite[itemImages.Length];
        UpdateUI();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public bool AddItem(Sprite itemSprite)
    {
        for (int i = 0; i < itemSprites.Length; i++)
        {
            if (itemSprites[i] == null)
            {
                itemSprites[i] = itemSprite;
                UpdateUI();

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
        bool temBoneca = false, temBola = false, temCracha = false;

        foreach (Sprite s in itemSprites)
        {
            if (s != null)
            {
                if (s.name == "BONECA_0") temBoneca = true;
                if (s.name == "BOLA_0") temBola = true;
                if (s.name == "CRACHÁ_0") temCracha = true;
            }
        }

        if (temBoneca && temBola && !taskBonecaBolaCompleta)
        {
            if (backgroundAudioManager != null)
                backgroundAudioManager.StopOverlayAudio(9);
            if (audioTaskCompleta != null && audioSource != null)
                audioSource.PlayOneShot(audioTaskCompleta);
            taskBonecaBolaCompleta = true;
        }
        if (temCracha && !taskCrachaCompleta)
        {
            if (backgroundAudioManager != null)
                backgroundAudioManager.StopOverlayAudio(4);
            if (audioTaskCompleta != null && audioSource != null)
                audioSource.PlayOneShot(audioTaskCompleta);
            taskCrachaCompleta = true;
        }
    }
}