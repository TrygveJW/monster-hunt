﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TileChangedEventArgs: EventArgs{
    private String Letter { get; set; }
}

public class GameBoardTile : Dragable
{

    [SerializeField]
    private Text TileText;



    [SerializeField]
    private int xPos, yPos;

    private LgLetter holdingLetter;

    public int XPos { get => xPos; set => xPos = value; }
    public int YPos { get => yPos; set => yPos = value; }

    // -- properties -- //
    // -- events -- // 
    public static event EventHandler<TileChangedEventArgs> TileChangedEventArgs;
    public void CallbackBoardChangedEvent(object _, BoardChangedEventArgs __){

    }

    // -- public -- //

    /// <summary>
    /// set the letter to be displayed int the tile
    /// </summary>
    /// <param name="letter">the letter to display</param>
    public void SetTile(LgLetter letter){
        LetterGameManager.Instance.UpdateLetterPos(XPos,YPos,letter);
        holdingLetter = letter;
        this.updateDisplayedLetter();
    }

    
    // -- private -- // 

    /// <summary>
    /// resets the displayed char on the tile
    /// </summary>
    public void ResetTile(){
        holdingLetter=null;
        this.updateDisplayedLetter();
    }

    /// <summary>
    /// updates the char displayed 
    /// </summary>
    private void updateDisplayedLetter(){
        if (holdingLetter == null){
            TileText.text ="?";
        }else{
            TileText.text = this.holdingLetter.Letter;
        }
        
    }

    /// <summary>
    /// Tels whether or not the Ui element should start the drag operation 
    /// </summary>
    /// <returns>true if the drag operation can start false if not</returns>   
    override protected bool CanStartDrag(){
        if (holdingLetter != null){
            this.IconLetter = this.holdingLetter.Letter;
            TileText.text ="?";
            return true;
        }
        return false;

    }

    /// <summary>
    /// What the Ui element should do when the drag operation is complete
    /// </summary>
    override protected void OnDragCompletion(PointerEventData eventData){
        GameObject hit = eventData.pointerCurrentRaycast.gameObject;
        if (hit != null){
            // hit somthing
            if( hit.TryGetComponent<GameBoardTile>(out GameBoardTile tile)){
                // hit somthing with game tile
                if (tile != this){
                    // not self
                    tile.SetTile(holdingLetter);
                    ResetTile();
                }
            } else{
                // hit some GO without gametile
                LetterGameManager.Instance.UpdateLetterPos(-1,-1,holdingLetter);
                ResetTile();

            }
        } else {
            // hit nothing
            LetterGameManager.Instance.UpdateLetterPos(-1,-1,holdingLetter);
            ResetTile();
        }

        updateDisplayedLetter();
    }
    // -- unity -- //

    private void Start() {
        TileText = this.GetComponentInChildren<Text>();
    }
}
