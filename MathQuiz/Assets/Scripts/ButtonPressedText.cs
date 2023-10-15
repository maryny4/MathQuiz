using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPressedText : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float pressedSpaceY = 4;
    private Transform text;
    private float normalPosY;
    private Button button;
    private void Start()
    {
        text = transform.GetChild(0);
        normalPosY = text.localPosition.y;
        button = GetComponent<Button>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(button != null)
            if(button.interactable)
                text.localPosition = new Vector2(text.localPosition.x, normalPosY - pressedSpaceY);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        text.localPosition = new Vector2(text.localPosition.x, normalPosY);
    }
}