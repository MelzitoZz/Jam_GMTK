using UnityEngine;
using UnityEngine.UI;

public class JB_Inventory : MonoBehaviour
{
    public Image[] itemImages;
    private Sprite[] itemSprites;

    public BackgroundAudioManager backgroundAudioManager;

    public AudioClip audioTaskCompleta;
    private AudioSource audioSource;

    private bool taskBonecaBolaCompleta = false;
    private bool taskCrachaCompleta = false;

    // Novas flags
    private bool bonecaColetada = false;
    private bool bolaColetada = false;

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

                // Marcar que foi coletado
                if (itemSprite.name == "BONECA_0") bonecaColetada = true;
                if (itemSprite.name == "BOLA_0") bolaColetada = true;

                UpdateUI();
                PararOverlaySePegouBonecaEBola();
                return true;
            }
        }
        return false;
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
        if (bonecaColetada && bolaColetada && !taskBonecaBolaCompleta)
        {
            if (backgroundAudioManager != null)
                backgroundAudioManager.StopOverlayAudio(9);
            if (audioTaskCompleta != null && audioSource != null)
                audioSource.PlayOneShot(audioTaskCompleta);
            taskBonecaBolaCompleta = true;
        }

        bool temCracha = false;
        foreach (Sprite s in itemSprites)
        {
            if (s != null && s.name == "CRACHÁ_0")
            {
                temCracha = true;
                break;
            }
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
