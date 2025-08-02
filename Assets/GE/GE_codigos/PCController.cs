using UnityEngine;

public class PCController : MonoBehaviour
{
    public bool crachaUsado = false;
    public AudioClip audioTaskCompleta;
    public AudioSource audioSource;

    void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void UsarCracha()
    {
        if (!crachaUsado)
        {
            crachaUsado = true;
            Debug.Log("Crachá usado no PC!");

            if (audioTaskCompleta != null && audioSource != null)
            {
                audioSource.PlayOneShot(audioTaskCompleta);
            }
        }
        else
        {
            Debug.Log("O crachá já foi usado.");
        }
    }
}