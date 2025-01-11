using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace LightDev.UI
{
  [RequireComponent(typeof(Image))]
  public class BaseButtonColor : BaseButton
  {
    [Space]
    public Color pressedColor = new Color(200 / 255f, 200 / 255f, 200 / 255f, 255 / 255f);

    protected Color normalColor;

    protected override void Awake()
    {
      base.Awake();

      normalColor = GetColor();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
      base.OnPointerDown(eventData);

      Sequence(Color(pressedColor, 0.2f));
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
      base.OnPointerUp(eventData);

      Sequence(Color(normalColor, 0.2f));
    }
  }
}
