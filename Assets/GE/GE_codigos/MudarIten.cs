using UnityEngine;
using UnityEngine.UI;

public class MudarIten : MonoBehaviour
{
    public Image[] itemImages;
    public Color selectedColor = Color.yellow;

    private int selectedSlot = 0;
    private Color[] originalColors;

    void Start()
    {
        originalColors = new Color[itemImages.Length];
        for (int i = 0; i < itemImages.Length; i++)
        {
            originalColors[i] = itemImages[i].color;
        }
        UpdateSlotVisual();
    }

    void Update()
    {
        // Troca com números 1 a 7
        for (int i = 0; i < itemImages.Length && i < 7; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                selectedSlot = i;
                UpdateSlotVisual();
                return; // evita conflito com o scroll no mesmo frame
            }
        }

        // Troca com scroll do mouse
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            selectedSlot = (selectedSlot + 1) % itemImages.Length;
            UpdateSlotVisual();
        }
        else if (scroll < 0f)
        {
            selectedSlot = (selectedSlot - 1 + itemImages.Length) % itemImages.Length;
            UpdateSlotVisual();
        }
    }

    void UpdateSlotVisual()
    {
        for (int i = 0; i < itemImages.Length; i++)
        {
            if (i == selectedSlot)
                itemImages[i].color = selectedColor;
            else
                itemImages[i].color = originalColors[i];
        }
    }

    public int GetSelectedSlot()
    {
        return selectedSlot;
    }
}
