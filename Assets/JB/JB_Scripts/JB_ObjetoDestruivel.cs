using UnityEngine;

public class JB_ObjetoDestruivel : MonoBehaviour
{
    [Header("Vida e Sprites")]
    public int vidaMaxima = 5;
    [Tooltip("Sprites de dano em ordem: 0 = intacto, 1 = 1 hit, ..., N = quebrado.")]
    public Sprite[] danoSprites; // Atribua 6 sprites se possível: intacto, 1 hit, ..., quebrado

    [Header("Shake")]
    public float shakeDuration = 0.1f;
    public float shakeMagnitude = 0.1f;

    [Header("Task & Som")]
    public AudioClip somTaskCompleta;
    public AudioSource audioSource; // Arraste ou deixe em branco para adicionar automaticamente
    public BackgroundAudioManager backgroundAudioManager; // Arraste no Inspector se quiser

    [Header("Layer após quebrar")]
    public string layerAposQuebrar = "Default"; // Defina o nome da Layer desejada no Inspector

    private SpriteRenderer sr;
    private Vector3 originalPos;
    private int vidaAtual;
    private bool isBroken = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalPos = transform.localPosition;
        vidaAtual = vidaMaxima;

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Inicializa com sprite intacto se houver
        if (danoSprites != null && danoSprites.Length > 0)
            sr.sprite = danoSprites[0];
    }

    public void TakeHit(int damage)
    {
        if (isBroken) return;

        vidaAtual -= damage;
        if (vidaAtual < 0) vidaAtual = 0;

        StartCoroutine(Shake());

        AtualizaSpriteDano();

        if (vidaAtual <= 0)
        {
            BreakObject();
        }
    }

    void AtualizaSpriteDano()
    {
        // Calcula o índice do sprite conforme o dano tomado
        int index = Mathf.Clamp(vidaMaxima - vidaAtual, 0, danoSprites.Length - 1);
        if (danoSprites != null && danoSprites.Length > index && danoSprites[index] != null)
        {
            sr.sprite = danoSprites[index];
        }
    }

    System.Collections.IEnumerator Shake()
    {
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            transform.localPosition = originalPos + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }

    void BreakObject()
    {
        isBroken = true;
        // Mostra o sprite final se existir
        if (danoSprites != null && danoSprites.Length > 0)
        {
            sr.sprite = danoSprites[danoSprites.Length - 1];
        }

        // Toca som de task completa
        if (somTaskCompleta != null && audioSource != null)
            audioSource.PlayOneShot(somTaskCompleta);

        // Para overlay 2
        if (backgroundAudioManager != null)
            backgroundAudioManager.StopOverlayAudio(2);

        // Muda a layer da caixa após quebrar
        if (!string.IsNullOrEmpty(layerAposQuebrar))
            gameObject.layer = LayerMask.NameToLayer(layerAposQuebrar);

        // Outras lógicas de "caixa quebrada" são possíveis aqui
    }
}