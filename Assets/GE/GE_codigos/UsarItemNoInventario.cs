using UnityEngine;
using UnityEngine.UI;

public class UsarItemNoInventario : MonoBehaviour
{
    public MudarIten mudarIten;
    public JB_Inventory inventario;
    public UsarItens usarItens;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            int slotSelecionado = mudarIten.GetSelectedSlot();

            Sprite itemParaUsar = inventario.GetItemAt(slotSelecionado);

            if (itemParaUsar != null)
            {
                usarItens.UseItem(itemParaUsar);
            }
            else
            {
                Debug.Log("Nenhum item neste slot para usar.");
            }
        }
    }
}