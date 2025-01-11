using UnityEngine;
using UnityEngine.UI;

using LightDev;
using LightDev.Core;

using DG.Tweening;

namespace HoleBall
{
  public class HoleIncreaser : Base
  {
    [Header("Settings")]
    public float increaseCoefficient; // how much will be added to currentProgress when pick up obstacle, from 0 to 1
    public float timeToLose; // imagine that currentProgress equals 1, so it is time needed to currentProgress be 0
    public float effectDuration;

    [Header("Visual")]
    public float effectScale = 1.5f;
    public Image imageProgress;
    public Material holeMaterial;

    private float currentProgress;
    private bool isIncreased;

    private readonly int ColorSaturationID = Shader.PropertyToID("_ColorSaturation");

    private void OnValidate()
    {
      increaseCoefficient = Mathf.Clamp(increaseCoefficient, 0, 1);
      timeToLose = Mathf.Max(timeToLose, 0);
      effectDuration = Mathf.Max(effectDuration, 0);
    }

    private void Awake()
    {
      Events.ObstacleHitLayerChanger += OnObstacleHit;
      Events.GamePreReset += OnGamePreReset;
    }

    private void OnDestroy()
    {
      Events.ObstacleHitLayerChanger -= OnObstacleHit;
      Events.GamePreReset -= OnGamePreReset;
    }

    private void Update()
    {
      if (!isIncreased)
      {
        currentProgress -= Time.deltaTime / timeToLose;
        currentProgress = Mathf.Max(currentProgress, 0);
      }

      imageProgress.fillAmount = currentProgress;
    }

    private void OnGamePreReset()
    {
      KillSequences();

      isIncreased = false;
      currentProgress = 0;
      transform.localScale = Vector3.one;
      holeMaterial.SetFloat(ColorSaturationID, 0);
    }

    private void OnObstacleHit()
    {
      if (isIncreased) return;

      currentProgress += increaseCoefficient;

      if (currentProgress >= 1)
      {
        RunEffect();
      }
    }

    private void RunEffect()
    {
      isIncreased = true;
      Sequence(
        Scale(effectScale, 0.2f).SetEase(Ease.OutBack)
      );
      Sequence(
        DOTween.To(value =>
        {
          holeMaterial.SetFloat(ColorSaturationID, value);
        }, 0, 1, 0.2f).SetEase(Ease.InSine)
      ).SetLoops(-1, LoopType.Yoyo).stringId = "c";

      Sequence(
        DOTween.To(value =>
        {
          currentProgress = value;
        }, 1, 0, effectDuration),
        OnFinish(() =>
        {
          isIncreased = false;
          KillSequence("c");
          Sequence(
            DOTween.To(value =>
            {
              holeMaterial.SetFloat(ColorSaturationID, value);
            }, 1, 0, 0.2f).SetEase(Ease.InSine)
          );
        }),
        Scale(1, 0.2f).SetEase(Ease.InSine)
      );
    }
  }
}
