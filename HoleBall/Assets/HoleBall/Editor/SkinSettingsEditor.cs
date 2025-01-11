using System.Linq;
using UnityEngine;
using UnityEditor;

namespace HoleBall
{
  [CustomEditor(typeof(SkinsSettings))]
  public class SkinsSettingsEditor : Editor
  {
    private GameObject visualPrefab;

    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      CheckDefaultSkinsIndexes();
      CheckSkinsIndexes();
    }

    private void CheckDefaultSkinsIndexes()
    {
      SkinsSettings settings = (SkinsSettings) target;

      if(settings.ballSkins.Length != 0)
      {
        bool isFound = false;
        for(int i = 0; i < settings.ballSkins.Length; i++)
        {
          if(settings.defalutBallSkinIndex == settings.ballSkins[i].index)
          {
            isFound = true;
            break;
          }
        }
        if(!isFound)
        {
          ShowError("Default Ball Skin Index must be equal one of Ball Skins index.");
        }
      }

      if(settings.holeSkins.Length != 0)
      {
        bool isFound = false;
        for(int i = 0; i < settings.holeSkins.Length; i++)
        {
          if(settings.defaultHoleSkinIndex == settings.holeSkins[i].index)
          {
            isFound = true;
            break;
          }
        }
        if(!isFound)
        {
          ShowError("Default Hole Skin Index must be equal one of Hole Skins index.");
        }
      }
    }

    private void CheckSkinsIndexes()
    {
      SkinsSettings settings = (SkinsSettings) target;
      bool stop = false;
      // Check Ball indexes
      for(int i = 0; i < settings.ballSkins.Length && !stop; i++)
      {
        for(int j = 0; j < settings.ballSkins.Length; j++)
        {
          if(i == j)
          {
            continue;
          }
          if(settings.ballSkins[i].index == settings.ballSkins[j].index)
          {
            ShowError($"Ball Skins Element{i} and Ball Skins Element{j} have equal indexes.");
            stop = true;
            break;
          }
        }
      }
      // Check Hole indexes
      for(int i = 0; i < settings.holeSkins.Length && !stop; i++)
      {
        for(int j = 0; j < settings.holeSkins.Length; j++)
        {
          if(i == j)
          {
            continue;
          }
          if(settings.holeSkins[i].index == settings.holeSkins[j].index)
          {
            ShowError($"Hole Skins Element{i} and Hole Skins Element{j} have equal indexes.");
            stop = true;
            break;
          }
        }
      }
      // Check Ball and Hole Indexes
      for(int i = 0; i < settings.ballSkins.Length && !stop; i++)
      {
        for(int j = 0; j < settings.holeSkins.Length; j++)
        {
          if(settings.ballSkins[i].index == settings.holeSkins[j].index)
          {
            ShowError($"Ball Skins Element{i} and Hole Skins Element{j} have equal indexes.");
            stop = true;
            break;
          }
        }
      }
    }

    private void ShowError(string text)
    {
      EditorGUILayout.HelpBox(text, MessageType.Error);
    }
  }
}
