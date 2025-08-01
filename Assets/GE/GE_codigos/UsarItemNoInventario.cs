using UnityEngine;
using UnityEngine.UI;

public class UsarItemNoInventario : MonoBehaviour
{
    public MudarIten mudarIten; // Referência ao script que controla o inventário visual
    public JB_Inventory inventario; // Referência ao inventário (onde estão os sprites)
    public UsarItens usarItens; // Referência ao seu script de lógica de uso

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            int slotSelecionado = mudarIten.GetSelectedSlot();

            // Obtém o sprite do slot selecionado no inventário
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