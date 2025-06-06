using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonBigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    private Image image;
    public float hoverScaleMultiplier = 1.2f;

    void Start()
    {
        originalScale = transform.localScale;
        image = GetComponent<Image>();
        image.color = Color.skyBlue;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * hoverScaleMultiplier;
        image.color = Color.lightCyan;   
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
        image.color = Color.skyBlue;
    }
}