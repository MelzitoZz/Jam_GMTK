using UnityEngine;

public class InteracaoObjeto : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        if (!Input.GetKeyDown(KeyCode.E))
            return; // Só processa se pressionou E neste quadro

        // Prioridade: Cama, PC, Geladeira, Ventilador
        if (other.CompareTag("Cama"))
        {
            Debug.Log("Detectado objeto com tag 'Cama' e pressionado E.");
            var cama = other.GetComponent<CamaController>();
            if (cama != null)
                cama.InteragirComCama();
            else
                Debug.LogWarning("CamaController não encontrado no objeto com tag 'Cama'.");
            return;
        }
        if (other.CompareTag("PC"))
        {
            Debug.Log("Detectado objeto com tag 'PC' e pressionado E.");
            var pc = other.GetComponent<PCController>();
            if (pc != null)
                pc.InteragirMaoVazia();
            else
                Debug.LogWarning("PCController não encontrado no objeto com tag 'PC'.");
            return;
        }
        if (other.CompareTag("Geladeira"))
        {
            Debug.Log("Detectado objeto com tag 'Geladeira' e pressionado E.");
            var geladeira = other.GetComponent<GeladeiraController>();
            if (geladeira != null)
            {
                Debug.Log($"Estado atual da geladeira: aberta = {geladeira.aberta}, temLeite = {geladeira.temLeite}");
                if (!geladeira.aberta)
                    geladeira.AbrirGeladeira();
                else
                    geladeira.FecharGeladeira();
            }
            else
                Debug.LogWarning("GeladeiraController não encontrado no objeto com tag 'Geladeira'.");
            return;
        }
        if (other.CompareTag("Ventilador"))
        {
            Debug.Log("Detectado objeto com tag 'Ventilador' e pressionado E.");
            var ventilador = other.GetComponent<VentiladorController>();
            if (ventilador != null)
                ventilador.AlternarSom();
            else
                Debug.LogWarning("VentiladorController não encontrado no objeto com tag 'Ventilador'.");
            return;
        }
    }
}