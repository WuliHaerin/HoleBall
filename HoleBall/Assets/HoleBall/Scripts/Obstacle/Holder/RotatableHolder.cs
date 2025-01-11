using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LightDev.Core;

namespace HoleBall
{
  public class RotatableHolder : Base
  {
    public float rotationSpeed;
    private bool isReady;

    private void Awake()
    {
      var holder = GetComponent<ObstaclesHolder>();
      if (holder)
      {
        holder.onObstaclesSpawnFinished += OnObstaclesSpawnFinished;
        holder.onObstacleSpawned += OnObstacleSpawned;
      }
      else
      {
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
          rb.isKinematic = true;
        }
        isReady = true;
      }
    }

    private void OnObstacleSpawned(Transform obstacle)
    {
      obstacle.parent = transform;
      obstacle.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OnObstaclesSpawnFinished()
    {
      isReady = true;
    }

    private void Rotate()
    {
      if (isReady)
      {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
      }
    }

    private void Update()
    {
      Rotate();
    }
  }
}
