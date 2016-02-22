
using UnityEngine;
using System.Collections;


public class SnakeStage : MonoBehaviour {

  [SerializeField]
  private GameObject _cell = null;
  public GameObject Cell {
    get
    {
      if (_cell != null) return _cell;
      _cell = Resources.Load<GameObject>("Cell");
      return _cell;
    }
  }

  private const int Columns = 10;
  private const int Rows = 10;

  private readonly GameObject[,] _cells = new GameObject[Rows, Columns];

  void Start()
  {
    for (var r = 0; r < Rows; r++)
    {
      for (var c = 0; c < Columns; c++)
      {
        _cells[r, c] = CreateCell(r, c);
      }
    }
  }

  void Update()
  {
    Debug.Log(Time.deltaTime);
  }

  private GameObject CreateObject(GameObject origin, int r, int c)
  {
    var gameObj = Instantiate(origin);
    gameObj.name = Cell.name + "(" + r + "," + c + ")";
    gameObj.transform.Translate(r, c, 0);
    gameObj.transform.parent = gameObject.transform;
    return gameObj;
  }

  private GameObject CreateCell(int r, int c)
  {
    return CreateObject(Cell, r, c);
  }
}
