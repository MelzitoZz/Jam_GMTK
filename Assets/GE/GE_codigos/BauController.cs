using UnityEngine;

public class BauController : MonoBehaviour
{
    public bool aberto = false;
    public GameObject bolaNoBau; // Referência para o objeto Bola dentro do Baú
    public Animator animator;     // Referência para o Animator do baú

    public void AbrirBau()
    {
        if (!aberto)
        {
            aberto = true;
            // Dispara a animação de abrir
            if (animator != null)
                animator.SetBool("Abrir", true);

            if (bolaNoBau != null)
                bolaNoBau.SetActive(true); // Mostra a bola ao abrir

            Debug.Log("Baú aberto!");
        }
        else
        {
            Debug.Log("O baú já estava aberto.");
        }
    }
}