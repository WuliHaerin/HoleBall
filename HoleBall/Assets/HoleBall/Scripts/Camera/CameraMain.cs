using UnityEngine;

using LightDev;
using LightDev.Core;

using DG.Tweening;

namespace HoleBall
{
  [RequireComponent(typeof(Camera))]
  public class CameraMain : Base 
  {
    public CameraState gameState;
    public CameraState failState;

    private Camera _camera;

    private void Awake()
    {
      Application.targetFrameRate = 60;
      _camera = GetComponent<Camera>();

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
      ChangeStateInstantly(gameState);
    }

    private void OnGameFailed()
    {
      ChangeState(failState);
    }

    private void ChangeStateInstantly(CameraState state)
    {
      KillSequences();
      SetLocalPosition(state.localPosition);
      SetLocalRotation(state.localRotation);
      _camera.fieldOfView = state.fov;
    }

    private void ChangeState(CameraState state)
    {
      KillSequences();
      Sequence(MoveLocal(state.localPosition, state.duration).SetEase(state.ease));
      Sequence(RotateLocal(state.localRotation, state.duration).SetEase(state.ease));
      Sequence(_camera.DOFieldOfView(state.fov, state.duration).SetEase(state.ease));
    }
  }
}
