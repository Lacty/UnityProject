
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

  [SerializeField]
  private GameObject _snakeHead = null;
  public GameObject SnakeHead {
    get
    {
      if (_snakeHead != null) return _snakeHead;
      _snakeHead = Resources.Load<GameObject>("SnakeHead");
      return _snakeHead;
    }
  }

  [SerializeField]
  private GameObject _snakeBody = null;
  public GameObject SnakeBody {
    get
    {
      if (_snakeBody != null) return _snakeBody;
      _snakeBody = Resources.Load<GameObject>("SnakeBody");
      return _snakeBody;
    }
  }

  [SerializeField]
  private const float UpdateRate = 0.6f;
  private float _updateCount = 0.0f;

  private const int Columns = 10;
  private const int Rows = 10;

  private readonly GameObject[,] _cells = new GameObject[Rows, Columns];

  void Start()
  {
    InitGame();
  }

  void Update()
  {
    if (!souldUpdate()) return;
    Debug.Log("update");
  }

  private bool souldUpdate()
  {
    _updateCount += Time.deltaTime;
    if (_updateCount <= UpdateRate) return false;
    _updateCount -= UpdateRate;
    return true;
  }

  private void InitGame()
  {
    InitCells();
  }

  private void InitCells()
  {
    for (var r = 0; r < Rows; r++)
    {
      for (var c = 0; c < Columns; c++)
      {
        _cells[r, c] = CreateCell(r, c);
      }
    }
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
