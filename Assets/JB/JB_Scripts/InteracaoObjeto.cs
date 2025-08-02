using UnityEngine;

public class InteracaoObjeto : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        // Baú
        if (other.CompareTag("Bau") && Input.GetKeyDown(KeyCode.E))
        {
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
        }

        // Geladeira
        if (other.CompareTag("Geladeira") && Input.GetKeyDown(KeyCode.E))
        {
            GeladeiraController geladeira = other.GetComponent<GeladeiraController>();
            if (geladeira != null)
            {
                if (!geladeira.aberta)
                    geladeira.AbrirGeladeira();
                else
                    geladeira.FecharGeladeira();
            }
        }

        // Atenção: lógica da mesa foi removida porque agora é feita com tecla P via ItemPickup.cs
    }
}
