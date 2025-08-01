using UnityEngine;

public class ChaveController : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Bau") && Input.GetKeyDown(KeyCode.E))
        {
            BauController bau = other.GetComponent<BauController>();
            if (bau != null)
            {
                bau.AbrirBau();
                Destroy(gameObject);
            }
        }
    }
}