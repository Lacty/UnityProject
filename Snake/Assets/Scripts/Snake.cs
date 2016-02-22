
using UnityEngine;

namespace Assets.Scripts {
  public enum SnakeType {
    Head, Body
  }

  public class Snake : MonoBehaviour {
    private SnakeType _type;
    public SnakeType Type {
      get { return _type; }
      set { _type = value; }
    }
  }
}
