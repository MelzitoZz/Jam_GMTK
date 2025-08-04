using UnityEngine;

public class JB_ItemPickup : MonoBehaviour
{
    public Sprite itemSprite;
    public JB_ColetarMensagem coletarMensagem;

    [Header("Som de Coleta")]
    public AudioClip audioColeta;
    [Range(0f, 1f)] public float volumeAudioColeta = 0.5f;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Collect()
    {
        JB_Inventory inv = FindObjectOfType<JB_Inventory>();
        if (inv != null && inv.AddItem(itemSprite))
        {
            if (audioColeta != null && audioSource != null)
            {
                audioSource.PlayOneShot(audioColeta, volumeAudioColeta);
            }

            coletarMensagem?.HideMessage();

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Não foi possível adicionar o item ao inventário.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            coletarMensagem?.ShowMessage();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            coletarMensagem?.HideMessage();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.P))
        {
            Collect();
        }
    }
}
