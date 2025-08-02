using UnityEngine;
using System.Collections;

public class JB_PlayerCollect : MonoBehaviour
{
    public Transform collectPoint;
    public float collectRange = 0.5f;
    public LayerMask itemLayer;
    public KeyCode collectKey = KeyCode.P;
    public Animator animator;

    public float delayColeta = 0.7f;
    public float cooldownColeta = 0.7f;

    private bool podeColetar = true;
    private JB_Inventory inventory;

    // Adicione a referência ao seu BackgroundAudioManager aqui
    public BackgroundAudioManager backgroundAudioManager;
    private bool overlayAudioParado = false;

    void Start()
    {
        inventory = FindObjectOfType<JB_Inventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(collectKey))
        {
            TryCollect();
        }
    }

    public void TryCollect()
    {
        if (podeColetar)
            StartCoroutine(ColetarCoroutine());
    }

    IEnumerator ColetarCoroutine()
    {
        podeColetar = false;

        if (animator != null)
            animator.SetTrigger("Coletar");

        yield return new WaitForSeconds(delayColeta);

        Collider2D[] items = Physics2D.OverlapCircleAll(collectPoint.position, collectRange, itemLayer);
        foreach (Collider2D item in items)
        {
            JB_ItemPickup pickup = item.GetComponent<JB_ItemPickup>();
            if (pickup != null)
            {
                // Antes de coletar, pegue o nome do sprite deste item
                if (pickup.itemSprite != null)
                {
                    string spriteName = pickup.itemSprite.name;
                    if (!overlayAudioParado && backgroundAudioManager != null &&
                        (spriteName == "BONECA_0" || spriteName == "BOLA_0"))
                    {
                        backgroundAudioManager.StopOverlayAudio(12); // ajuste o tempo conforme necessário
                        overlayAudioParado = true;
                    }
                }

                pickup.Collect();
                break;
            }
        }

        yield return new WaitForSeconds(Mathf.Max(0, cooldownColeta - delayColeta));

        podeColetar = true;
    }

    void OnDrawGizmosSelected()
    {
        if (collectPoint == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(collectPoint.position, collectRange);
    }
}