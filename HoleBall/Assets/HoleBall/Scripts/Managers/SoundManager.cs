using UnityEngine;

using LightDev;

namespace HoleBall
{
  public class SoundManager : MonoBehaviour
  {
    public AudioSource source;

    [Header("UI Click")]
    public AudioClip click;

    [Header("Store")]
    public AudioClip skinUnlocked;
    public AudioClip skinUnlockFailed;

    [Header("Obstacle")]
    public AudioClip obstacle;

    [Header("Finish")]
    public AudioClip levelComplete;

    private void Awake()
    {
      Events.RequestClickSound += OnClickSound;
      Events.ShowStore += OnClickSound;
      Events.CloseStore += OnClickSound;
      Events.BallSkinSelected += OnBallSkinSelected;
      Events.HoleSkinSelected += OnHoleSkinSelected;

      Events.BallSkinUnlocked += OnBallSkinUnlocked;
      Events.HoleSkinUnlocked += OnHoleSkinUnlocked;

      Events.SkinUnlockFailed += OnSkinUnlockFailed;

      Events.ObstacleHitLayerChanger += OnObstacleHit;

      Events.GameSucceed += OnGameSucceed;
    }

    private void OnDestroy()
    {
      Events.RequestClickSound -= OnClickSound;
      Events.ShowStore -= OnClickSound;
      Events.CloseStore -= OnClickSound;
      Events.BallSkinSelected -= OnBallSkinSelected;
      Events.HoleSkinSelected -= OnHoleSkinSelected;

      Events.BallSkinUnlocked -= OnBallSkinUnlocked;
      Events.HoleSkinUnlocked -= OnHoleSkinUnlocked;

      Events.SkinUnlockFailed -= OnSkinUnlockFailed;

      Events.ObstacleHitLayerChanger -= OnObstacleHit;

      Events.GameSucceed -= OnGameSucceed;
    }

    private void OnClickSound() => PlaySound(click);

    private void OnBallSkinSelected(int index) => OnClickSound();
    private void OnHoleSkinSelected(int index) => OnClickSound();

    private void OnBallSkinUnlocked(int index) => OnSkinUnlocked();
    private void OnHoleSkinUnlocked(int index) => OnSkinUnlocked();
    private void OnSkinUnlocked() => PlaySound(skinUnlocked);

    private void OnSkinUnlockFailed() => PlaySound(skinUnlockFailed);

    private void OnObstacleHit() => PlaySound(obstacle);

    private void OnGameSucceed() => PlaySound(levelComplete);

    private void PlaySound(AudioClip clip)
    {
      if (clip == null || source == null) return;

      source.clip = clip;
      source.Play();
    }
  }
}
