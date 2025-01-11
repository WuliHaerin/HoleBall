using UnityEngine.EventSystems;
using UnityEngine;

using LightDev;

namespace HoleBall
{
  public class Touchpad : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
  {
    public float sensitivity = 20f;

    private float _horizontalValue;
    private float _vertivalValue;

    private float _previousHorizontalValue;
    private float _previousVerticalValue;

    public float GetHorizontal()
    {
      float current = (_previousHorizontalValue == _horizontalValue) ? 0 : _horizontalValue;
      _previousHorizontalValue = _horizontalValue;

      return current;
    }

    public float GetVertical()
    {
      float current = (_previousVerticalValue == _vertivalValue) ? 0 : _vertivalValue;
      _previousVerticalValue = _vertivalValue;

      return current;
    }

    public void OnDrag(PointerEventData eventData)
    {
      _horizontalValue = eventData.delta.x * 0.0061f * sensitivity;
      _vertivalValue = eventData.delta.y * 0.0061f * sensitivity;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      Events.PointerDown.Call();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      Events.PointerUp.Call();

      _horizontalValue = 0;
      _vertivalValue = 0;
    }
  }
}
