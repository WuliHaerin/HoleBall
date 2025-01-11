using UnityEngine;

using LightDev;
using LightDev.Core;

namespace HoleBall
{
  [RequireComponent(typeof(Renderer))]
  [RequireComponent(typeof(Collider))]
  [RequireComponent(typeof(Rigidbody))]
  public class Player : Base
  {
    public float speed;
    public Rigidbody[] brokenParts;

    private bool _isBroken;
    private Renderer _renderer;
    private Vector3[] _brokenPartsLocalPositions;

    private static Player _player;
    public static Player GetInstance() { return _player; }

    private void Awake()
    {
      _player = this;
      _renderer = GetComponent<Renderer>();
      _brokenPartsLocalPositions = new Vector3[brokenParts.Length];
      for(int i = 0; i < brokenParts.Length; i++)
      {
        _brokenPartsLocalPositions[i] = brokenParts[i].transform.localPosition;
      }

      GetComponent<Collider>().isTrigger = true;
      GetComponent<Rigidbody>().isKinematic = true;

      Events.GamePreReset += OnGamePreReset;
      Events.GameStart += OnGameStarted;
    }

    private void OnDestroy()
    {
      Events.GamePreReset -= OnGamePreReset;
      Events.GameStart -= OnGameStarted;
    }

    // Player only detect collision with Obstacle Layer
    // Check collision matrix
    private void OnTriggerEnter(Collider other)
    {
      if (_isBroken) return;

      _isBroken = true;
      _renderer.enabled = false;
      ActivateBrokenParts();
      Events.PlayerBroken.Call();
    }

    private void Update()
    {
      UpdatePosition();
      UpdateRotation();
      CheckFinish();
    }

    private void OnGamePreReset()
    {
      _isBroken = false;
      _renderer.enabled = true;
      DeactivateBrokenParts();
      SetPosition(new Vector3(0, 0.5f, 0));
    }

    private void OnGameStarted()
    {
      SetRotation(new Vector3(0, 0, 0));
    }

    private void ActivateBrokenParts()
    {
      for (int i = 0; i < brokenParts.Length; i++)
      {
        Rigidbody part = brokenParts[i];
        part.transform.parent = null;
        part.transform.rotation = Quaternion.identity;
        part.transform.position = GetPosition() + _brokenPartsLocalPositions[i];

        Vector3 forceDirection = part.transform.position - GetPosition();

        part.isKinematic = false;
        part.velocity = Vector3.zero;
        part.angularVelocity = Vector3.zero;
        part.gameObject.SetActive(true);
        part.AddForce(forceDirection * 10, ForceMode.Impulse);
      }
    }

    private void DeactivateBrokenParts()
    {
      foreach(Rigidbody part in brokenParts)
      {
        part.gameObject.SetActive(false);
      }
    }

    private void CheckFinish()
    {
      if (!GameManager.IsGameSucceed() && GetPositionZ() > LevelsManager.GetLevelLength())
      {
        Events.LevelFinished.Call();
      }
    }

    private void UpdatePosition()
    {
      if (GameManager.IsGameStarted() && !GameManager.IsGameFailed())
      {
        SetPositionZ(GetPositionZ() + speed * Time.deltaTime);
      }
    }

    private void UpdateRotation()
    {
      Vector3 rotation = GameManager.IsGameStarted() ? new Vector3(400, 0) : new Vector3(0, -100);
      rotation *= Time.deltaTime;
      transform.Rotate(rotation);
    }
  }
}
