using UnityEngine;

public class InteracaoObjeto : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        // Baú
        if (other.CompareTag("Bau") && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Detectado objeto com tag 'Bau' e pressionado E.");

            BauController bau = other.GetComponent<BauController>();
            if (bau != null)
            {
                Debug.Log($"InteracaoObjeto: bau.aberto = {bau.aberto}, bau.IsTrancado() = {bau.IsTrancado()}");
                if (!bau.aberto)
                {
                    if (!bau.IsTrancado())
                        bau.AbrirBau();
                    else
                        Debug.Log("Baú está trancado, destranque primeiro.");
                }
                else
                {
                    bau.FecharBau();
                }
            }
            else
            {
                Debug.LogWarning("BauController não encontrado no objeto com tag 'Bau'.");
            }
        }

        // Geladeira
        if (other.CompareTag("Geladeira") && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Detectado objeto com tag 'Geladeira' e pressionado E.");

            GeladeiraController geladeira = other.GetComponent<GeladeiraController>();
            if (geladeira != null)
            {
                Debug.Log($"Estado atual da geladeira: aberta = {geladeira.aberta}, temLeite = {geladeira.temLeite}");

                if (!geladeira.aberta)
                {
                    geladeira.AbrirGeladeira();
                }
                else
                {
                    geladeira.FecharGeladeira();
                }
            }
            else
            {
                Debug.LogWarning("GeladeiraController não encontrado no objeto com tag 'Geladeira'.");
            }
        }

        // Atenção: lógica da mesa foi removida porque agora é feita com tecla P via ItemPickup.cs
    }
}
