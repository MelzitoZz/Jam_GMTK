using UnityEngine;
using UnityEngine.SceneManagement;

public class CamaController : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite spriteCamaDormindo;
    public SpriteRenderer camaRenderer;

    [Header("Referências")]
    public GameObject player;
    public BackgroundAudioManager backgroundAudioManager;

    [Header("Cena de Fim")]
    public string nomeCenaFim = "Fim";

    [Header("Tempo de espera antes de trocar de cena")]
    public float tempoAteFim = 5f;

    private bool interagiu = false;

    public void InteragirComCama()
    {
        if (interagiu) return;

        // Só pode dormir se todos overlays 0~12 estiverem desativados (ou nulos)
        if (backgroundAudioManager != null)
        {
            for (int i = 0; i < backgroundAudioManager.overlayClips.Length; i++)
            {
                if (i == 13) continue;
                // Se existe overlay e NÃO está desativado -> não pode dormir
                if (backgroundAudioManager.overlayClips[i] != null && !backgroundAudioManager.IsOverlayDisabled(i))
                {
                    Debug.Log("NÃO PODE DORMIR: Overlay " + i + " ainda está ativo!");
                    return;
                }
            }
        }

        interagiu = true;

        // Troca o sprite da cama
        if (camaRenderer != null && spriteCamaDormindo != null)
            camaRenderer.sprite = spriteCamaDormindo;

        // Remove o player
        if (player != null)
            Destroy(player);

        // Para o áudio 13
        if (backgroundAudioManager != null)
            backgroundAudioManager.StopOverlayAudio(13);

        StartCoroutine(VaiParaCenaFim());
    }

    private System.Collections.IEnumerator VaiParaCenaFim()
    {
        yield return new WaitForSeconds(tempoAteFim);
        if (!string.IsNullOrEmpty(nomeCenaFim))
            SceneManager.LoadScene(nomeCenaFim);
    }
}