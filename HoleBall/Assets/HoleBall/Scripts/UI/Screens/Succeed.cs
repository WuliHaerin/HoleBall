using UnityEngine;

using LightDev;
using LightDev.Core;
using LightDev.UI;

using DG.Tweening;

namespace HoleBall.UI
{
  public class Succeed : CanvasElement
  {
    [Header("References")]
    public BaseText passedLevel;
    public TextHolder earnedText;
    public Base tapToContinue;

    public override void Subscribe()
    {
      Events.GameSucceed += Show;
      Events.GamePreReset += Hide;
    }

    public override void Unsubscribe()
    {
      Events.GameSucceed -= Show;
      Events.GamePreReset -= Hide;
    }

    protected override void OnStartShowing()
    {
      passedLevel.SetText($" 关卡 {LevelsManager.GetCurrentLevelIndex() + 1}通关");
      earnedText.SetText($"你赚了: {CoinManager.GetLevelReward()}");

      AnimateLevelShow();
      AnimateTapToContinue();
    }

    private void AnimateLevelShow()
    {
      passedLevel.SetPositionY(200);
      passedLevel.Sequence(
        passedLevel.MoveY(-380, 0.4f).SetEase(Ease.OutBack)
      );
    }

    private void AnimateTapToContinue()
    {
      tapToContinue.SetScale(0);
      tapToContinue.SetFade(1);
      tapToContinue.Sequence(
        tapToContinue.Delay(0.5f),
        tapToContinue.Scale(1, 0.4f).SetEase(Ease.OutBack),
        tapToContinue.OnFinish(() =>
        {
          tapToContinue.Sequence(
            tapToContinue.Fade(0, 0.9f).SetEase(Ease.InSine),
            tapToContinue.Fade(1, 0.6f).SetEase(Ease.InSine)
          ).SetLoops(-1);
        })
      );
    }
  }
}
