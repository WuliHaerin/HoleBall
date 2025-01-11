using UnityEngine;

using LightDev;

namespace HoleBall
{
  public class FinishGenerator : MonoBehaviour
  {
    public GameObject finishPrefab;
    private GameObject finishObject;

    private void Awake()
    {
      finishObject = Instantiate(finishPrefab);

      Events.GamePostReset += OnGamePostReset;
    }

    private void OnDestroy()
    {
      Events.GamePostReset -= OnGamePostReset;
    }

    private void OnGamePostReset()
    {
      finishObject.transform.position = new Vector3(0, 0, LevelsManager.GetLevelLength());
    }
  }
}
