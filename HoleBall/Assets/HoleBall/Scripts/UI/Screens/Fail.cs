using UnityEngine;

using LightDev;
using LightDev.Core;
using LightDev.UI;

using DG.Tweening;

namespace HoleBall.UI
{
  public class Fail : CanvasElement
  {
    [Header("References")]
    public Base background;
    public Base crashedText;
    public Base tapToRestartText;

    public override void Subscribe()
    {
      Events.GamePreReset += Hide;
      Events.GameFailed += Show;
    }

    public override void Unsubscribe()
    {
      Events.GamePreReset -= Hide;
      Events.GameFailed -= Show;
    }

    protected override void OnStartShowing()
    {
      background.SetFade(0);
      crashedText.SetFade(0);
      tapToRestartText.SetFade(0);

      AnimateBackground();
      AnimateCrashedText();
      AnimateTapToRestartText();
    }

    private void AnimateBackground()
    {
      background.Sequence(
        background.Fade(106f / 255f, 0.2f).SetEase(Ease.InSine)
      );
    }

    private void AnimateCrashedText()
    {
      crashedText.Sequence(
        crashedText.Delay(0.1f),
        crashedText.Fade(1, 0.2f).SetEase(Ease.InSine)
      );
    }

    public float to;
    public float from;

    private void AnimateTapToRestartText()
    {
      tapToRestartText.Sequence(
        tapToRestartText.Delay(0.7f),
        tapToRestartText.OnFinish(() =>
        {
          tapToRestartText.Sequence(
            tapToRestartText.Fade(1, 0.4f).SetEase(Ease.InSine),
            tapToRestartText.Fade(0, 0.6f).SetEase(Ease.InSine)
          ).SetLoops(-1);
        })
      );
    }
  }
}
