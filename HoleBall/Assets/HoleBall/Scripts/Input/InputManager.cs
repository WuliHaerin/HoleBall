using UnityEngine;

namespace HoleBall
{
  public class InputManager : MonoBehaviour 
  {
    public Touchpad touchpad;

    private static float horizontal;
    private static float vertical;

    public static float GetHorizontal() { return horizontal; }
    public static float GetVertical() { return vertical; }

    private void Update()
    {
      horizontal = touchpad.GetHorizontal();
      vertical = touchpad.GetVertical();
    }
  }
}
