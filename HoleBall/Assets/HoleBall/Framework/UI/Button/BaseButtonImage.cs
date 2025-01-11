using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace LightDev.UI
{
  [RequireComponent(typeof(Image))]
  public class BaseButtonImage : BaseButton
  {
    [Space]
    public Sprite normalImage;
    public Sprite pressedImage;

    protected override void Awake()
    {
      base.Awake();

      image.sprite = normalImage;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
      base.OnPointerDown(eventData);

      image.sprite = pressedImage;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
      base.OnPointerUp(eventData);

      image.sprite = normalImage;
    }
  }
}
