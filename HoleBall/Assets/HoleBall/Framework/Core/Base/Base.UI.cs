﻿using System;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LightDev.Core
{
  public partial class Base
  {
    public virtual float GetFade()
    {
      if (GetComponent<Image>())
      {
        return GetComponent<Image>().color.a;
      }
      
      if(GetComponent<TMP_Text>())
      {
        return GetComponent<Text>().color.a;
      }

      throw new NotImplementedException();
    }

    public virtual void SetFade(float fade)
    {
      if(GetComponent<Image>())
      {
        Image img = GetComponent<Image>();
        Color color = img.color;
        color.a = fade;
        img.color = color;
        return;
      }
      
      if(GetComponent<TMP_Text>())
      {
        TMP_Text text = GetComponent<TMP_Text>();
        Color color = text.color;
        color.a = fade;
        text.color = color;
        return;
      }

      throw new NotImplementedException();
    }

    public virtual Color GetColor()
    {
      if (GetComponent<Image>())
      {
        return GetComponent<Image>().color;
      }
      if(GetComponent<TMP_Text>())
      {
        return GetComponent<TMP_Text>().color;
      }

      throw new NotImplementedException();
    }

    public virtual void SetColor(Color color)
    {
      if (GetComponent<Image>())
      {
        GetComponent<Image>().color = color;
        return;
      }

      if(GetComponent<TMP_Text>())
      {
        GetComponent<TMP_Text>().color = color;
        return;
      }

      throw new NotImplementedException();
    }
  }
}
