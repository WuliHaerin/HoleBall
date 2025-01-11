using UnityEngine;
using UnityEngine.UI;
using TMPro;


using LightDev.Core;

using DG.Tweening;

namespace LightDev.UI
{
    [RequireComponent(typeof(TMP_Text))]
  public class BaseText : Base
  {
    protected TMP_Text textComponent;

    protected virtual void Awake()
    {
            textComponent = GetComponent<TMP_Text>();
            Debug.Log(gameObject.name);
    }

    public virtual TMP_Text GetTextComponent()
    {
      return textComponent;
    }

    public virtual void SetText(string text)
    {
      textComponent.text = text;
    }

    public virtual void SetText(int text)
    {
      SetText(text.ToString());
    }

    public virtual Tween TweenColor(Color to, float duration)
    {
      return textComponent.DOColor(to, duration);
    }

    public virtual Tween TweenFade(float to, float duration)
    {
      return textComponent.DOFade(to, duration);
    }

    //public virtual Tween TweenText(string to, float duration)
    //{
    //  return textComponent.DOText(to, duration);
    //}
  }
}
