using UnityEngine;

public class SlotView : MonoBehaviour
{
    public Item slotItem;
    public bool isSelected = false;
    public CanvasGroup canvasGroup;

    public void UpdateView()
    {
        canvasGroup.alpha = isSelected ? 1f : 0.5f;
    }
}