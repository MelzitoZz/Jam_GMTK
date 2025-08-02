using UnityEngine;
using TMPro;

public class JB_UsarMensagem : MonoBehaviour
{
    public TMP_Text mensagemText;
    public float blinkSpeed = 0.5f;

    private Coroutine blinkCoroutine;

    void Awake()
    {
        if (mensagemText != null)
            mensagemText.text = "Use E";
    }

    public void ShowMessage()
    {
        if (blinkCoroutine == null)
            blinkCoroutine = StartCoroutine(Blink());
    }

    public void HideMessage()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }
        if (mensagemText != null)
            mensagemText.enabled = false;
    }

    System.Collections.IEnumerator Blink()
    {
        while (true)
        {
            if (mensagemText != null)
                mensagemText.enabled = !mensagemText.enabled;
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
}
