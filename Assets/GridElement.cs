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
    public SpriteRenderer parentSprite;
    private Color startColor;
    private void Start()
    {
        parentSprite = GetComponent<SpriteRenderer>();
        startColor = parentSprite.color;
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
                background.gameObject.SetActive(false);
                parentSprite.color = startColor;
                break;
        }
    }
    public void Reveal()
    {
        if (!isBomb)
            state = GridElementState.REVEALED;
        else
        {
            state = GridElementState.BOMB;
            //TODO: loseGame(); function    
        }
    }
    void Click()
    {
        switch (state)
        {
            case GridElementState.HIDDEN:
                Reveal();
                break;
            case GridElementState.REVEALED:
                //TODO: check all of neighbours and reveal them if there is enough flags
                break;
            default:
                return;
        }
        UpdateState();
    }
    void RightClick()
    {
        if (state == GridElementState.HIDDEN)
            state = GridElementState.SUSSY;
        else if (state == GridElementState.SUSSY)
            state = GridElementState.HIDDEN;
        UpdateState();
    }
    void OnMouseEnter()
    {
        parentSprite.color = Color.white;
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
            RightClick();
        if (Input.GetMouseButtonDown(0))
            Click();
        // Debug.Log("Left Click");
    }
    void OnMouseExit()
    {
        parentSprite.color = startColor;
        UpdateState();
    }
}