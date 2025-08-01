using UnityEngine;

public class UsarItens : MonoBehaviour
{
    public void UseItem(Sprite itemSprite)
    {
        if (itemSprite == null)
        {
            Debug.LogWarning("Nenhum item selecionado para uso.");
            return;
        }

        string itemName = itemSprite.name;
        Debug.Log("Usando item: " + itemName);

        switch (itemName)
        {
            default:
                Debug.Log("Esse item não faz nada especial.");
                break;
        }
    }
}