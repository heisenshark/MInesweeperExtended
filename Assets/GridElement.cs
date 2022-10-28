using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement : MonoBehaviour
{
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
        startColor = new Color32(0x57, 0x57, 0x57, 0xFF);
        UpdateState();
    }
    public void UpdateState()
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

                break;

        }
        if (mineCount == 0)
            bombs.gameObject.SetActive(false);
        bombs.text = $"{mineCount}";
        var xd = ((float)mineCount / 8);
        bombs.color = new Color(xd, 0.7f, xd);
    }
    public void Reveal()
    {
        MasterObject.masterObject.tilesRevealed++;
        if (!isBomb)
        {
            MasterObject.masterObject.isGameStarted = true;
            state = GridElementState.REVEALED;
            if (mineCount == 0)
                MasterObject.masterObject.RevealSquare(position.x, position.y);
        }
        else
        {
            state = GridElementState.BOMB;
            //TODO: loseGame(); function  \
            MasterObject.masterObject.LoseGame();
        }
        UpdateState();
    }
    void Click()
    {
        if (MasterObject.masterObject.lostGame) return;
        switch (state)
        {
            case GridElementState.HIDDEN:
                Reveal();
                break;
            case GridElementState.REVEALED:
                //TODO: check all of neighbours and reveal them if there is enough flags
                MasterObject.masterObject.RevealIf(position.x, position.y);
                break;
            default:
                return;
        }
        UpdateState();
    }
    void RightClick()
    {
        if (MasterObject.masterObject.lostGame) return;
        if (state == GridElementState.HIDDEN)
        {
            state = GridElementState.SUSSY;
            MasterObject.masterObject.flagCount++;
        }
        else if (state == GridElementState.SUSSY)
        {
            MasterObject.masterObject.flagCount--;
            state = GridElementState.HIDDEN;
        }
        UpdateState();
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(1))
            RightClick();
        if (Input.GetMouseButtonUp(0))
            Click();
        parentSprite.color = Color.white;
        if (Input.GetMouseButton(0))
            parentSprite.color = Color.gray;

        // Debug.Log("Left Click");
    }
    void OnMouseExit()
    {
        parentSprite.color = startColor;
        UpdateState();
    }

    internal void incrementMineCount()
    {
        mineCount++;
    }
}