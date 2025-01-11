using System;
using System.Linq;
using UnityEngine;

using LightDev;

namespace HoleBall
{
  public class SkinManager : MonoBehaviour
  {
    public SkinsSettings skinsSettings;

    private const string BallCurrentSkinKey = "Ball";
    private const string HoleCurrentSkinKey = "Hole";
    private const string UnlockedSkinsKey = "Skins";

    private static SkinsSettings skins;
    private static int[] unlockedSkins;

    public static SkinInfo[] GetBallSkins() { return skins.ballSkins; }
    public static SkinInfo[] GetHoleSkins() { return skins.holeSkins; }

    public static SkinInfo GetBallSkin(int index)
    {
      for (int i = 0; i < GetBallSkins().Length; i++)
      {
        if (GetBallSkins()[i].index == index)
        {
          return GetBallSkins()[i];
        }
      }

      throw new Exception($"No such ball skin with index {index} found");
    }
    public static SkinInfo GetHoleSkin(int index)
    {
      for (int i = 0; i < GetHoleSkins().Length; i++)
      {
        if (GetHoleSkins()[i].index == index)
        {
          return GetHoleSkins()[i];
        }
      }

      throw new Exception($"No such hole skin with index {index} found");
    }

    public static bool BallSkinsContains(int index)
    {
      for (int i = 0; i < GetBallSkins().Length; i++)
      {
        if (GetBallSkins()[i].index == index)
        {
          return true;
        }
      }

      return false;
    }
    public static bool HoleSkinsContains(int index)
    {
      for (int i = 0; i < GetHoleSkins().Length; i++)
      {
        if (GetHoleSkins()[i].index == index)
        {
          return true;
        }
      }

      return false;
    }

    public static SkinInfo GetBallCurrentSkin()
    {
      return GetBallSkin(PlayerPrefs.GetInt(BallCurrentSkinKey, skins.defalutBallSkinIndex));
    }
    public static SkinInfo GetHoleCurrentSkin()
    {
      return GetHoleSkin(PlayerPrefs.GetInt(HoleCurrentSkinKey, skins.defaultHoleSkinIndex));
    }

    public static void SelectSkin(int index)
    {
      if (!IsSkinUnlocked(index))
      {
        throw new Exception($"Skin with index {index} in not unlocked.");
      }

      if (BallSkinsContains(index))
      {
        PlayerPrefs.SetInt(BallCurrentSkinKey, index);
        Events.BallSkinSelected.Call(index);
      }
      else if (HoleSkinsContains(index))
      {
        PlayerPrefs.SetInt(HoleCurrentSkinKey, index);
        Events.HoleSkinSelected.Call(index);
      }
    }

    public static int[] GetUnlockedSkinIndexes() { return unlockedSkins; }
    public static bool IsSkinUnlocked(int skinIndex) { return unlockedSkins.Contains(skinIndex); }
    public static void UnlockSkin(int index)
    {
      if (IsSkinUnlocked(index))
      {
        throw new InvalidOperationException($"Skin with {index} index has been unlocked.");
      }

      int[] skins = new int[unlockedSkins.Length + 1];
      skins[unlockedSkins.Length] = index;
      Array.Copy(unlockedSkins, 0, skins, 0, unlockedSkins.Length);
      unlockedSkins = skins;

      if (BallSkinsContains(index))
      {
        Events.BallSkinUnlocked.Call(index);
      }
      else if (HoleSkinsContains(index))
      {
        Events.HoleSkinUnlocked.Call(index);
      }

      AddSkinsToPlayerPrefs(unlockedSkins);
    }

    private void Awake()
    {
      skins = skinsSettings;
      unlockedSkins = GetSkinsFromPlayerPrefs();
    }

    private static int[] GetSkinsFromPlayerPrefs()
    {
      string json = PlayerPrefs.GetString(UnlockedSkinsKey, JsonUtility.ToJson(new ArrayHolder(new int[] { skins.defalutBallSkinIndex, skins.defaultHoleSkinIndex })));
      return JsonUtility.FromJson<ArrayHolder>(json).array;
    }

    private static void AddSkinsToPlayerPrefs(int[] skins)
    {
      string json = JsonUtility.ToJson(new ArrayHolder(skins));
      PlayerPrefs.SetString(UnlockedSkinsKey, json);
    }

    [System.Serializable]
    private class ArrayHolder
    {
      public int[] array;

      public ArrayHolder()
      {
        this.array = new int[0];
      }

      public ArrayHolder(int[] array)
      {
        this.array = array;
      }
    }
  }
}
