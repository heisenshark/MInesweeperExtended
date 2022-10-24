using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement : MonoBehaviour
{
    //static MasterClass masterClass;
    public enum GridElementState
    {
        BOMB,
        HIDDEN,
        SUSSY,
        REVEALED
    }
    public GridElementState state = GridElementState.HIDDEN;
    public bool isBomb = false;
    public int mineCount = 0;
    public Vector2Int position;

    public TMPro.TMP_Text bombs;
    public SpriteRenderer background;
    private void Start()
    {
        UpdateState();
    }
    void UpdateState()
    {
        switch (state)
        {
            case GridElementState.BOMB:
                bombs.gameObject.SetActive(false);
                background.color = Color.red;
                break;
            case GridElementState.SUSSY:
                bombs.gameObject.SetActive(false);
                background.color = Color.yellow;
                break;
            case GridElementState.HIDDEN:
                bombs.gameObject.SetActive(false);
                background.color = Color.gray;
                break;
            case GridElementState.REVEALED:
                bombs.gameObject.SetActive(true);
                break;
        }
    }

    void Click(){
        if(!isBomb) 
            state = GridElementState.REVEALED;
        else;
            //loseGame();    
    }
}