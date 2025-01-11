using UnityEngine;
using UnityEngine.UI;
using TMPro;

using LightDev;

namespace HoleBall.UI
{
  [RequireComponent(typeof(Image))]
  public class StoreButton : MonoBehaviour
  {
    [Header("References")]
    public TMP_Text infoText;
    public Image skinImage;
    public GameObject lockImage;
    public GameObject coinImage;

    private SkinInfo skinInfo;

    private void OnEnable()
    {
      Events.BallSkinSelected += OnSkinSelectChanged;
      Events.HoleSkinSelected += OnSkinSelectChanged;
    }

    private void OnDisable()
    {
      Events.BallSkinSelected -= OnSkinSelectChanged;
      Events.HoleSkinSelected -= OnSkinSelectChanged;
    }

    private void OnSkinSelectChanged(int index)
    {
      UpdateInfo(skinInfo);
    }

    public void UpdateInfo(SkinInfo skinInfo)
    {
      this.skinInfo = skinInfo;
      int skinIndex = skinInfo.index;

      if (SkinManager.GetBallCurrentSkin().index == skinIndex || SkinManager.GetHoleCurrentSkin().index == skinIndex)
      {
        OnSelected();
      }
      else if (SkinManager.IsSkinUnlocked(skinIndex))
      {
        OnUnlocked();
      }
      else
      {
        OnLocked();
      }
    }

    private void OnSelected()
    {
      transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
      infoText.text = "已选择";
      skinImage.sprite = skinInfo.storeIcon;
      lockImage.SetActive(false);
      coinImage.SetActive(false);
    }

    private void OnLocked()
    {
      transform.localScale = Vector3.one;
      infoText.text = skinInfo.price.ToString();
      skinImage.sprite = skinInfo.storeIcon;
      lockImage.SetActive(true);
      coinImage.SetActive(true);
    }

    private void OnUnlocked()
    {
      transform.localScale = Vector3.one;
      infoText.text = "已解锁";
      skinImage.sprite = skinInfo.storeIcon;
      lockImage.SetActive(false);
      coinImage.SetActive(false);
    }

    public void OnClick()
    {
      if (SkinManager.IsSkinUnlocked(skinInfo.index))
      {
        SkinManager.SelectSkin(skinInfo.index);
      }
      else
      {
        if (CoinManager.IsCoinsEnough(skinInfo.price))
        {
          SkinManager.UnlockSkin(skinInfo.index);
          SkinManager.SelectSkin(skinInfo.index);
          CoinManager.DecreaseCoinsCount(skinInfo.price);
        }
        else
        {
          Events.SkinUnlockFailed.Call();
        }
      }
    }
  }
}
