using UnityEngine;

namespace HoleBall
{
  public class HoleRotator : MonoBehaviour
  {
    public float rotationSpeed;

    private void Update()
    {
      transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }
  }
}
