﻿
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Assets.Scripts;


public class SnakeStage : MonoBehaviour {

  [SerializeField]
  private GameObject _cell = null;
  public GameObject Cell {
    get
    {
      if (_cell != null) return _cell;
      _cell = Resources.Load<GameObject>("Cell/Cell");
      return _cell;
    }
  }

  [SerializeField]
  private GameObject _snakeHead = null;
  public GameObject SnakeHead {
    get
    {
      if (_snakeHead != null) return _snakeHead;
      _snakeHead = Resources.Load<GameObject>("Snake/Head");
      return _snakeHead;
    }
  }

  [SerializeField]
  private GameObject _snakeBody = null;
  public GameObject SnakeBody {
    get
    {
      if (_snakeBody != null) return _snakeBody;
      _snakeBody = Resources.Load<GameObject>("Snake/Body");
      return _snakeBody;
    }
  }

  [SerializeField]
  private const float UpdateRate = 0.6f;
  private float _updateCount = 0.0f;

  private const int Columns = 10;
  private const int Rows = 10;

  private readonly GameObject[,] _cells = new GameObject[Rows, Columns];

  private Vector3 _nextDir = new Vector3(0, -1, 0);
  // 配列だが複数形でないので注意
  private List<GameObject> _snake = new List<GameObject>();

  void Start()
  {
    InitGame();
  }

  void Update()
  {
    UpdateGame();
  }

  private void UpdateGame()
  {
    UpdateDirection();
    if (!ShouldUpdate()) return;
    MoveSnake();
  }

  private void MoveSnake()
  {
    _snake[0].transform.Translate(_nextDir);
  }

  private void UpdateDirection()
  {
    if      (IsValidInput(KeyCode.W)) _nextDir.Set( 0,  1, 0);
    else if (IsValidInput(KeyCode.S)) _nextDir.Set( 0, -1, 0);
    else if (IsValidInput(KeyCode.A)) _nextDir.Set(-1,  0, 0);
    else if (IsValidInput(KeyCode.D)) _nextDir.Set( 1,  0, 0);
  }
  
  private bool IsValidInput(KeyCode key)
  {
    switch (key)
    {
      case KeyCode.W:
        if (Input.GetKeyDown(KeyCode.W))
        if (GetDirection(0).y != -1) { return true; }
      break;
      case KeyCode.S:
        if (Input.GetKeyDown(KeyCode.S))
        if (GetDirection(0).y !=  1) { return true; }
      break;
      case KeyCode.A:
        if (Input.GetKeyDown(KeyCode.A))
        if (GetDirection(0).x !=  1) { return true; }
      break;
      case KeyCode.D:
        if (Input.GetKeyDown(KeyCode.D))
        if (GetDirection(0).x != -1) { return true; }
      break;
    }
    return false;
  }

  private Vector3 GetDirection(int index)
  {
    return _snake[index].transform.position
         - _snake[index + 1].transform.position;
  }

  private bool ShouldUpdate()
  {
    _updateCount += Time.deltaTime;
    if (_updateCount <= UpdateRate) return false;
    _updateCount -= UpdateRate;
    return true;
  }

  private void InitGame()
  {
    InitCells();
    InitSnake();
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

  private void InitSnake()
  {
    SnakeHead.GetComponent<Snake>().Type = SnakeType.Head;
    SnakeBody.GetComponent<Snake>().Type = SnakeType.Body;
    _snake.Add(CreateSnakeHead(0, Columns - 4));
    _snake.Add(CreateSnakeBody(0, Columns - 3));
    _snake.Add(CreateSnakeBody(0, Columns - 2));
    _snake.Add(CreateSnakeBody(0, Columns - 1));
  }

  private GameObject CreateObject(GameObject origin, int r, int c)
  {
    var gameObj = Instantiate(origin);
    gameObj.name = origin.name + "(" + r + "," + c + ")";
    gameObj.transform.Translate(r, c, 0);
    gameObj.transform.parent = gameObject.transform;
    return gameObj;
  }

  private GameObject CreateCell(int r, int c)
  {
    return CreateObject(Cell, r, c);
  }

  private GameObject CreateSnakeHead(int r, int c)
  {
    return CreateObject(SnakeHead, r, c);
  }

  private GameObject CreateSnakeBody(int r, int c)
  {
    return CreateObject(SnakeBody, r, c);
  }
}
