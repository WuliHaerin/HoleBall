using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace LightDev.UI
{
  [RequireComponent(typeof(Image))]
  public class BaseButtonScale : BaseButton
  {
    public override void OnPointerDown(PointerEventData eventData)
    {
      base.OnPointerDown(eventData);

      Sequence(Scale(0.8f, 0.1f));
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
      base.OnPointerUp(eventData);

      Sequence(Scale(1f, 0.1f));
    }
  }
}
