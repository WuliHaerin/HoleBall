using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using LightDev.Core;

using DG.Tweening;

namespace LightDev.UI
{
  [RequireComponent(typeof(Image))]
  public abstract class BaseButton : Base, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
  {
    public UnityEvent onClick;

    protected Image image;

    public bool IsRaycaseTarget() { return image.raycastTarget; }
    public void SetRaycastTarget(bool raycaseTarget) { image.raycastTarget = raycaseTarget; }

    protected virtual void Awake()
    {
      image = GetComponent<Image>();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
      onClick.Invoke();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
      KillSequences();
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
      KillSequences();
    }

    public virtual void AnimateFadeShow(float time = 0.3f)
    {
      KillSequences();
      SetFade(0);
      Sequence(
        Fade(1, time).SetEase(Ease.InSine)
      );
    }

    public virtual void AnimateScaleShow(float time = 0.3f)
    {
      KillSequences();
      SetScale(0);
      Sequence(
        Scale(1, time).SetEase(Ease.OutBack)
      );
    }
  }
}
