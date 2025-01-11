using UnityEngine;

using LightDev;
using LightDev.Core;
using LightDev.UI;

using DG.Tweening;

namespace HoleBall.UI
{
    public class Store : CanvasElement
    {
        [Header("References")]
        public TextHolder coinsText;
        public BaseText textInfo;
        public StoreButton[] buttons;

        public GameObject adPanel;

        public override void Subscribe()
        {
            Events.ShowStore += Show;
            Events.CoinCountChanged += OnCoinCountChanged;
            Events.SkinUnlockFailed += OnSkinUnlockFailed;
            Events.BallSkinUnlocked += OnSkinUnlocked;
            Events.HoleSkinUnlocked += OnSkinUnlocked;
        }

        public override void Unsubscribe()
        {
            Events.ShowStore -= Show;
            Events.CoinCountChanged -= OnCoinCountChanged;
            Events.SkinUnlockFailed -= OnSkinUnlockFailed;
            Events.BallSkinUnlocked -= OnSkinUnlocked;
            Events.HoleSkinUnlocked -= OnSkinUnlocked;
        }

        protected override void OnStartShowing()
        {
            coinsText.SetText(CoinManager.GetCoinsCount());
            textInfo.SetFade(0);
            OnSelectBallSkins();
        }

        protected override void OnStartHiding()
        {
            Events.CloseStore.Call();
        }

        private void OnCoinCountChanged()
        {
            coinsText.SetText(CoinManager.GetCoinsCount());
        }

        private void OnSkinUnlockFailed()
        {
            UpdateInfoText("钻石不足");
            adPanel.SetActive(true);
        }

        private void OnSkinUnlocked(int index)
        {
            UpdateInfoText("皮肤解锁！");
        }

        private void UpdateInfoText(string text)
        {
            textInfo.SetText(text);
            textInfo.KillSequences();
            textInfo.SetFade(0);
            textInfo.Sequence(
              textInfo.Fade(1, 0.2f).SetEase(Ease.InSine),
              textInfo.Fade(0, 1.5f).SetEase(Ease.InSine)
            );
        }

        private void SelectSkins(SkinInfo[] skins)
        {
            for (int i = 0; i < skins.Length; i++)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].UpdateInfo(skins[i]);
            }
            for (int i = skins.Length; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }
        }

        public void OnCloseStore()
        {
            Hide();
        }

        public void OnSelectBallSkins()
        {
            Events.RequestClickSound.Call();
            SelectSkins(SkinManager.GetBallSkins());
        }

        public void OnSelectHoleSkins()
        {
            Events.RequestClickSound.Call();
            SelectSkins(SkinManager.GetHoleSkins());
        }
    }
}
