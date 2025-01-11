using UnityEngine;

namespace HoleBall
{
  public class Magnet : MonoBehaviour
  {
    public float magnetForce;

    protected virtual void OnTriggerStay(Collider collider)
    {
      Rigidbody rb = collider.GetComponent<Rigidbody>();
      if (rb)
      {
        Vector3 direction = (transform.position - collider.transform.position);
        rb.AddForce(direction.normalized * magnetForce, ForceMode.Impulse);
      }
    }
  }
}
