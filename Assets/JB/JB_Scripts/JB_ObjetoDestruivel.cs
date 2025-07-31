using UnityEngine;

public class JB_ObjetoDestruivel : MonoBehaviour
{
    public int vida = 3;
    public Sprite quebradoSprite;
    public float shakeDuration = 0.1f;
    public float shakeMagnitude = 0.1f;

    private SpriteRenderer sr;
    private Vector3 originalPos;
    private bool isBroken = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalPos = transform.localPosition;
    }

    public void TakeHit(int damage)
    {
        if (isBroken) return;

        vida -= damage;
        StartCoroutine(Shake());

        if (vida <= 0)
        {
            BreakObject();
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
        if (quebradoSprite != null)
        {
            sr.sprite = quebradoSprite;
        }
        // Opcional: desabilitar colisão ou outras lógicas
    }
}