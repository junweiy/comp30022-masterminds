using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class VirtualJoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

    private Image _joyStickBg;
    private Image _joyStickImg;
    private Vector3 _inputVector;

    public object RectransformUtility { get; private set; }

    void Start()
    {
        _joyStickBg = GetComponent<Image>();
        _joyStickImg = transform.GetChild(0).GetComponent<Image>();
    }

    public Vector3 GetStickPosition()
    {
        return _inputVector;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(_joyStickBg.rectTransform,ped.position,ped.pressEventCamera,out pos))
        {
            pos.x = pos.x / _joyStickBg.rectTransform.sizeDelta.x;
            pos.y = pos.y / _joyStickBg.rectTransform.sizeDelta.y;

            _inputVector = new Vector3(pos.x * 2, 0, pos.y * 2);
            _inputVector = (_inputVector.magnitude > 1.0f) ? _inputVector.normalized : _inputVector;

            _joyStickImg.rectTransform.anchoredPosition =
                new Vector3(_inputVector.x * (_joyStickBg.rectTransform.sizeDelta.x / 3),
                            _inputVector.z * (_joyStickBg.rectTransform.sizeDelta.y / 3));
        }
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        _inputVector = Vector3.zero;
        _joyStickImg.rectTransform.anchoredPosition = Vector3.zero;
    }
}
