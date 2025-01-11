using UnityEngine;

using LightDev;
using LightDev.Core;

using DG.Tweening;

namespace HoleBall
{
  public class SlowMotionManager : Base
  {
    private float initFixedDeltaTime;

    private void Awake()
    {
      initFixedDeltaTime = Time.fixedDeltaTime;

      Events.GamePreReset += OnGamePreReset;
      Events.GameFailed += OnGameFailed;
    }

    private void OnDestroy()
    {
      Events.GamePreReset -= OnGamePreReset;
      Events.GameFailed -= OnGameFailed;
    }

    private void OnGamePreReset()
    {
      KillSequences();
      SetTimeScale(1);
    }

    private void OnGameFailed()
    {
      Sequence(
        DOTween.To((value) =>
        {
          SetTimeScale(value);
        }, 1, 0.5f, 0.4f).SetEase(Ease.InSine),
        Delay(0.5f),
        DOTween.To((value) =>
        {
          SetTimeScale(value);
        }, 0.5f, 1, 0.3f).SetEase(Ease.InSine)
      );
    }

    private void SetTimeScale(float scale)
    {
      Time.timeScale = scale;
      Time.fixedDeltaTime = initFixedDeltaTime * Time.timeScale;
    }
  }
}
