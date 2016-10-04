using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class VirtualJoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

    private Image joyStickBG;
    private Image joyStickImg;
    private Vector3 inputVector;

    public object RectransformUtility { get; private set; }

    void Start()
    {
        joyStickBG = GetComponent<Image>();
        joyStickImg = transform.GetChild(0).GetComponent<Image>();
    }

    public Vector2 stickPosition()
    {
        return new Vector2(inputVector.x, inputVector.z);
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(joyStickBG.rectTransform,ped.position,ped.pressEventCamera,out pos))
        {
            pos.x = pos.x / joyStickBG.rectTransform.sizeDelta.x;
            pos.y = pos.y / joyStickBG.rectTransform.sizeDelta.y;

            inputVector = new Vector3(pos.x * 2 + 1, 0, pos.y * 2 - 1);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joyStickImg.rectTransform.anchoredPosition =
                new Vector3(inputVector.x * (joyStickBG.rectTransform.sizeDelta.x / 3),
                            inputVector.z * (joyStickBG.rectTransform.sizeDelta.y / 3));
        }
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        //OnDrag(ped);
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        joyStickImg.rectTransform.anchoredPosition = Vector3.zero;
    }
}
