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
        for (int i = 0; i < itemImages.Length && i < 7; i++)
        {
            // KeyCode.Alpha1 é o número 1, Alpha2 é o número 2, etc.
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                selectedSlot = i;
                UpdateSlotVisual();
            }
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
