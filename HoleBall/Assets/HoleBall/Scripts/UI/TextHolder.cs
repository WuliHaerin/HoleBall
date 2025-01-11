using UnityEngine;

using LightDev.Core;
using LightDev.UI;

namespace HoleBall
{
  public class TextHolder : MonoBehaviour
  {
    [Header("References")]
    public BaseText info;
    public Base image;

    [Space]
    public float distanceBetweenComponents = 30;

    public void SetText(int text)
    {
      SetText(text.ToString());
    }

    public void SetText(string text)
    {
      info.SetText(text);

      float textWidth = info.GetTextComponent().preferredWidth;
      float imageWidth = image.GetComponent<RectTransform>().rect.width;
      float width = textWidth + distanceBetweenComponents + imageWidth;

      float infoPosition = -width / 2 + textWidth / 2;
      float imagePosition = infoPosition + textWidth / 2 + distanceBetweenComponents + imageWidth / 2;

      info.SetPositionX(infoPosition);
      image.SetPositionX(imagePosition);
    }
  }
}
