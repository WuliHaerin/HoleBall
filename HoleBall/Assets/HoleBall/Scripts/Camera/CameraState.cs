using UnityEngine;
using DG.Tweening;

namespace HoleBall
{
  [System.Serializable]
  public class CameraState
  {
    public Vector3 localPosition;
    public Vector3 localRotation;
    public float fov;
    public float duration;
    public Ease ease;
  }
}
