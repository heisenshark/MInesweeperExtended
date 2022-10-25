using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterObject : MonoBehaviour
{
    [SerializeField] GridElement[,] gridElements;
    [SerializeField] GridElement templateGridElement;
    [SerializeField] bool won = false;
    [SerializeField] float timeElepsed;
    [SerializeField] bool isGameStarted = true;
    [SerializeField] int boardWidth = 10;
    [SerializeField] int flagCount = 0;
    [SerializeField] int boardHeight = 10;
    [SerializeField] int bombsCount = 10;
    void Start()
    {
        GenerateGrid();
        foreach (var item in gridElements)
        {
            item.Reveal();    
        }
    }

    private void GenerateGrid()
    {
        gridElements = new GridElement[boardWidth, boardHeight];
        for (int i = 0; i < boardHeight; i++)
        {
            for (int j = 0; j < boardWidth; j++)
            {
                var gridElement = Instantiate(templateGridElement, transform.position + (Vector3)(new Vector2(i, -j)), Quaternion.identity, transform);
                gridElements[i, j] = gridElement;
                Random.Range(0, 10);
                //TODO:change it so the bombs placed number is even
                //                gridElement.isBomb = Random.Range(0, 10) == 1;
                gridElement.position.x = i;
                gridElement.position.y = j;
            }
        }
        ScatterBombs();
        CheckNeighbours(10, 10);
        CheckNeighbours(5, 5);
        CheckNeighbours(1, 1);
    }
    private void ScatterBombs()
    {
        var bombsNumber = bombsCount;

        //TODO: Change it to spaggetthi code that will modify array and then fix it xDDDD
        (int, int)[] xd = new (int, int)[boardWidth * boardHeight];
        for (int i = 0; i < boardWidth * boardHeight; i++)
        {
            xd[i] = (i % boardWidth, i / boardHeight);
        }
        for (int i = 0; i < bombsCount; i++)
        {
            var rr = Random.Range(i,xd.Length);
            var temp = xd[i];
            xd[i] = xd[rr];
            xd[rr] =temp;
        }
        xd.Take(bombsCount).ToList().ForEach(x => {gridElements[x.Item1,x.Item2].isBomb = true;});
    }
    private GridElement GetElement(int x, int y)
    {
        if (x >= 0 && x < boardWidth && y >= 0 && y < boardHeight)
            return gridElements[x, y];
        return null;
    }
    private void CheckNeighbours(int x, int y)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GetElement(x - 1 + i, y - 1 + j)?.Reveal();
            }
        }
    }

}
