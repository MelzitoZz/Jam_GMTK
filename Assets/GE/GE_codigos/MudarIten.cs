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
}