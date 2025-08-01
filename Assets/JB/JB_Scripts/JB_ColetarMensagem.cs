using UnityEngine;
using TMPro;

public class JB_ColetarMensagem : MonoBehaviour
{
    public TMP_Text mensagemText; 
    public float blinkSpeed = 0.5f;

    private Coroutine blinkCoroutine;

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
        mensagemText.enabled = false;
    }

    System.Collections.IEnumerator Blink()
    {
        while (true)
        {
            mensagemText.enabled = !mensagemText.enabled;
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
}