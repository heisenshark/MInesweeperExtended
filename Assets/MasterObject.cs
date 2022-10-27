using System;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterObject : MonoBehaviour
{
    public static MasterObject masterObject;
    [SerializeField] GridElement[,] gridElements;
    [SerializeField] GridElement templateGridElement;
    [SerializeField] bool won = false;
    [SerializeField] public float timeElapsed;
    [SerializeField] bool isGameStarted = true;
    [SerializeField] public int boardWidth = 10;
    [SerializeField] public int flagCount = 0;
    [SerializeField] public int boardHeight = 10;
    [SerializeField] public int bombsCount = 10;
    [SerializeField] public bool lostGame = false;
    [SerializeField] public bool wonGame = false;
    [SerializeField] public int tilesRevealed = 0;
    void Start()
    {
        MasterObject.masterObject = gameObject.GetComponent<MasterObject>();
        GenerateGrid();
        Debug.Log(masterObject);
        Debug.Log("my life is like uuuuuuuuuu aaaaaaaaaa");
        //ClearBoard();
    }
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if(boardHeight*boardWidth-tilesRevealed == bombsCount)
            wonGame = true;
    }
    public void GenerateGrid()
    {
        ClearBoard();
        lostGame= false;
        wonGame = false;
        gridElements = new GridElement[boardWidth, boardHeight];
        for (int i = 0; i < boardWidth; i++)
        {
            for (int j = 0; j < boardHeight; j++)
            {
                var gridElement = Instantiate(templateGridElement, transform.position + (Vector3)(new Vector2(i, -j)), Quaternion.identity, transform);
                gridElements[i, j] = gridElement;
                UnityEngine.Random.Range(0, 10);
                gridElement.position.x = i;
                gridElement.position.y = j;
            }
        }
        //TODO: possibility that there is a bug and the numbers on board may not be good  
        ScatterBombs();


        // CheckNeighbours(10, 10);
        // CheckNeighbours(5, 5);
        // CheckNeighbours(1, 1);
    }
    private void RevealGridAndTestIt()
    {
        foreach (var item in gridElements)
        {
            item.Reveal();
            int count = 0, c2 = 0;
            IterateOverNeighbours(item.position.x, item.position.y, neighbour =>
            {
                if (neighbour.isBomb)
                    count++;
                c2++;
            });
            if (count != item.mineCount)
                Debug.Log("Error Occured At " + $"{item.position.x} {item.position.y} expected {item.mineCount} bombs" +
                $"got {count} bombs checks:{c2}"
                );
        }
    }
    private void ScatterBombs()
    {
        var bombsNumber = bombsCount;

        //TODO: Change it to spaggetthi code that will modify array and then fix it xDDDD
        (int, int)[] xd = new (int, int)[boardWidth * boardHeight];
        for (int i = 0; i < boardWidth * boardHeight; i++)
        {
            xd[i] = (i % boardWidth, i / boardWidth);
        }
        for (int i = 0; i < bombsCount; i++)
        {
            var rr = UnityEngine.Random.Range(i, boardWidth * boardHeight);
            var temp = xd[i];
            xd[i] = xd[rr];
            xd[rr] = temp;
        }
        xd.Take(bombsCount).ToList().ForEach(x =>
        {
            gridElements[x.Item1, x.Item2].isBomb = true;
            IterateOverNeighbours(x.Item1, x.Item2, element =>
            {
                element.incrementMineCount();
            });
        });
    }

    internal void RevealIf(int x, int y)
    {
        int count = 0;
        IterateOverNeighbours(x, y, element =>
        {
            if (element.state == GridElement.GridElementState.SUSSY)
                count++;
        });
        if (count == GetElement(x, y)?.mineCount)
            RevealSquare(x, y);
    }

    public void RevealSquare(int x, int y)
    {
        IterateOverNeighbours(x, y, element =>
        {
            if (!element) return;
            if (element.state == GridElement.GridElementState.HIDDEN)
            {
                element.Reveal();
            }
        });
    }

    private GridElement GetElement(int x, int y)
    {
        if (x >= 0 && x < boardWidth && y >= 0 && y < boardHeight)
            return gridElements[x, y];
        return null;
    }
    private void CalculateNeighbours(int x, int y)
    {
        IterateOverNeighbours(x, y, element =>
        {
            element?.incrementMineCount();
        });
    }

    private void IterateOverNeighbours(int x, int y, Action<GridElement> act)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (i == 1 && j == 1) continue;
                var element = GetElement(x - 1 + i, y - 1 + j);
                if (element) act.Invoke(element);
            }
        }

    }
    public void ClearBoard()
    {
        tilesRevealed = 0;
        flagCount = 0;
        if(gridElements==null)return;
        foreach (var item in gridElements)
        {
            Destroy(item.gameObject);
        }
        gridElements = null;
    }

    public void LoseGame(){
        lostGame = true;
    }
}
