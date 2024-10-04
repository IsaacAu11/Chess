using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour 
{
     Board board = new Board();

    void Start()
    {
        gameStart();
    }

    void Update()
    {
        //If it is the AI's turn
        if (MoveHolder.isTurn == false)
        {
            Move optimalMove = new Move();
            AI.minimax(Board.BoardValues, 0, false, AI.Min, AI.Max, new Move(), ref optimalMove);

            Board.gameObjectBoard[optimalMove.from.x, optimalMove.from.y].transform.position = new Vector2(optimalMove.to.x, optimalMove.to.y);
            AllowDrag.setGameObjectBoard(Board.BoardValues, optimalMove);
            AllowDrag.setValues(optimalMove, Board.BoardValues);

            if (Board.BoardValues[optimalMove.to.x, optimalMove.to.y].piece == PieceTYPE.PAWN && MoveHolder.isWhite && optimalMove.to.y == 7)
            {
                this.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Chess_qlt60");
                Board.BoardValues[optimalMove.to.x, optimalMove.to.y].piece = PieceTYPE.QUEEN;
            }
            else if (Board.BoardValues[optimalMove.to.x, optimalMove.to.y].piece == PieceTYPE.PAWN && !MoveHolder.isWhite && optimalMove.to.y == 0)
            {
                this.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Chess_qdt60");
                Board.BoardValues[optimalMove.to.x, optimalMove.to.y].piece = PieceTYPE.QUEEN;
            }

            MoveHolder.isWhite = !MoveHolder.isWhite;
            MoveHolder.isTurn = !MoveHolder.isTurn;
         }
    }

    public void gameStart()
    {
        MoveHolder.isTurn = true;
        MoveHolder.isWhite = true;  
        board.generateVisualBoard();
        board.generatePiecesOntoBoard();
        MoveHolder.hasWhiteCastled = false;
        MoveHolder.hasBlackCastled = false;
    }
}
