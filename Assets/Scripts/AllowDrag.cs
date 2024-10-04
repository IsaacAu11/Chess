using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllowDrag : MonoBehaviour
{
    private bool IsDragging;
    private Vector2 originalPiecePos;
    private Vector2 finalPiecePos;
    private Vector2 mousePos;
    Renderer r;

    public void OnMouseDown()
    {
        r = GetComponentInChildren<Renderer>();
        r.sortingOrder = 11;
        originalPiecePos = transform.position;
        if (isCorrectColour(gameObject))
        {
            IsDragging = true;
        }
    }
   
    public void OnMouseUp()
    {
        if (IsDragging == true)
        {
            r.sortingOrder = 10;
            transform.position = new Vector2(Mathf.Round(transform.position.x) , Mathf.Round(transform.position.y));
            finalPiecePos = transform.position;
            IsDragging = false;
            if (transform.position.x > 7 || transform.position.x < 0 || transform.position.y > 7 || transform.position.y < 0)
            {
                transform.position = originalPiecePos;
            }
            else
            {
                Position startPosition = new Position((int)originalPiecePos.x, (int)originalPiecePos.y);
                Position endingPosition = new Position((int)finalPiecePos.x, (int)finalPiecePos.y);
                Move move = new Move(startPosition, endingPosition);
                Piece_ pieceMoved = Board.BoardValues[startPosition.x, startPosition.y];
                //generate the next set of moves
                List<Move> moveList = MoveHolder.generateMoves(Board.BoardValues);
                //if the move made is a valid one:
                if (MoveHolder.checkValidMove(move, moveList, Board.BoardValues))
                {

                    setValues(move,Board.BoardValues);
                    setGameObjectBoard(Board.BoardValues,move);

                    if(MoveHolder.isCheckmate(Board.BoardValues))
                    {
                        Debug.Log("game won");
                        Text t = GameObject.FindGameObjectWithTag("Win").GetComponent<Text>();
                        if (!MoveHolder.isWhite)
                        {
                            t.text = "White Wins!";
                        }
                        else
                        {
                            t.text = "Black Wins!";
                        }
                    }

                    //if stalemate
                    if (moveList.Count == 0)
                    {
                        Text t = GameObject.FindGameObjectWithTag("Win").GetComponent<Text>();
                        t.text = "Stalemate!";
                    }

                    //pawn promotion : change the sprite and pieceType
                    //this has to be outside a method as it cannot be static

                    if (Board.BoardValues[move.to.x, move.to.y].piece == PieceTYPE.PAWN && MoveHolder.isWhite && move.to.y == 7)
                    {
                        this.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Chess_qlt60");
                        Board.BoardValues[move.to.x, move.to.y].piece = PieceTYPE.QUEEN;
                    }
                    else if (Board.BoardValues[move.to.x, move.to.y].piece == PieceTYPE.PAWN && !MoveHolder.isWhite && move.to.y == 0)
                    {
                        this.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Chess_qdt60");
                        Board.BoardValues[move.to.x, move.to.y].piece = PieceTYPE.QUEEN;
                    }

                    MoveHolder.isWhite = !MoveHolder.isWhite;
                    MoveHolder.isTurn = !MoveHolder.isTurn;
                }
                else
                {
                    //return piece to original position
                    transform.position = originalPiecePos;
                }
            }
        }
    }

    static public void boardCheckFor(Move move, List<Move> moveList)
    {
        //if checkmate
        if (MoveHolder.isCheckmate(Board.BoardValues))
        {
            Debug.Log("game won");
            Text t = GameObject.FindGameObjectWithTag("Win").GetComponent<Text>();
            if (!MoveHolder.isWhite)
            {
                t.text = "White Wins!";
            }
            else
            {
                t.text = "Black Wins!";
            }
        }

        //if stalemate
        if (moveList.Count == 0)
        {
            Text t = GameObject.FindGameObjectWithTag("Win").GetComponent<Text>();
            t.text = "Stalemate!";
        }
    }

    void Update()
    {
        if (IsDragging)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePos);
        }
    }

    private bool isCorrectColour(GameObject piece)
    {
        if (MoveHolder.isWhite && piece.CompareTag("White"))
        {
            return true;
        }
        else if (!MoveHolder.isWhite && piece.CompareTag("Black"))
        {
            return true;
        }
        return false;
    }

    static public void setGameObjectBoard(Piece_[,] board, Move m)
    {
        //If move was king then disable the castling on that side
        if (board[m.from.x, m.from.y].piece == PieceTYPE.KING)
        {
            if (MoveHolder.isWhite)
            {
                MoveHolder.hasWhiteCastled = true;
            }
            else
            {
                MoveHolder.hasBlackCastled = true;
            }
        }

        //If king and the move was a castle
        if (board[m.from.x, m.from.y].piece == PieceTYPE.KING && (m.Equals(new Move(m.from, new Position(m.from.x + 2, m.from.y))) || m.Equals(new Move(m.from, new Position(m.from.x - 2, m.from.y)))))
        {
            Board.gameObjectBoard[m.to.x, m.to.y] = Board.gameObjectBoard[m.from.x, m.from.y];
            Board.gameObjectBoard[m.from.x, m.from.y] = null;

            if (m.from.x - m.from.y > 0)
            {
                Transform t = Board.gameObjectBoard[0, m.from.y].GetComponent<Transform>();
                t.transform.position = new Vector2(m.to.x + 1, m.to.y);
                Board.gameObjectBoard[m.to.x + 1, m.to.y] = Board.gameObjectBoard[0, m.from.y];
                Board.gameObjectBoard[0, m.from.y] = null;
            }
            else
            {
                Transform t = Board.gameObjectBoard[7, m.from.y].GetComponent<Transform>();
                t.transform.position = new Vector2(m.to.x - 1, m.to.y);
                Board.gameObjectBoard[m.to.x - 1, m.to.y] = Board.gameObjectBoard[7, m.from.y];
                Board.gameObjectBoard[7, m.from.y] = null;
            }
            if (MoveHolder.isWhite)
            {
                MoveHolder.hasWhiteCastled = true;
            }
            else
            {
                MoveHolder.hasBlackCastled = true;
            }
        }
        else
        {
            //If move was a king then
            if (board[m.from.x, m.from.y].piece == PieceTYPE.KING)
            {
                if (MoveHolder.isWhite)
                {
                    MoveHolder.hasWhiteCastled = true;
                }
                else
                {
                    MoveHolder.hasBlackCastled = true;
                }
            }
            //Sort out the gameObjectBoard
            if (board[m.to.x, m.to.y].piece != PieceTYPE.NONE)
            {
                Destroy(Board.gameObjectBoard[m.to.x, m.to.y]);
            }
            Board.gameObjectBoard[m.to.x, m.to.y] = Board.gameObjectBoard[m.from.x, m.from.y];
            Board.gameObjectBoard[m.from.x, m.from.y] = null;
        }
    }

    static public void setValues(Move m, Piece_[,] board)
    {
        //if move was castle
        if (board[m.from.x, m.from.y].piece == PieceTYPE.KING && (m.Equals(new Move(m.from, new Position(m.from.x + 2, m.from.y))) || m.Equals(new Move(m.from, new Position(m.from.x - 2, m.from.y)))))
        {
            board[m.to.x, m.to.y] = board[m.from.x, m.from.y];
            board[m.to.x, m.to.y].position = m.to;
            board[m.from.x, m.from.y].position = m.from;
            board[m.from.x, m.from.y] = new Piece_(PieceTYPE.NONE, Colour.NONE);

            if (m.from.x - m.to.x > 0) //left
            {
                board[m.to.x + 1, m.to.y] = board[0, m.from.y];
                board[m.to.x + 1, m.to.y].position = new Position(m.to.x + 1, m.to.y);
                board[0, m.from.y].position = new Position(0, m.to.y);
                board[0, m.from.y] = new Piece_(PieceTYPE.NONE, Colour.NONE);
            }
            else //right
            {
                board[m.to.x - 1, m.to.y] = board[7, m.from.y];
                board[m.to.x - 1, m.to.y].position = new Position(m.to.x - 1, m.to.y);
                board[7, m.from.y].position = new Position(7, m.to.y);
                board[7, m.from.y] = new Piece_(PieceTYPE.NONE, Colour.NONE);
            }
        }
        //if last move not a castle
        else
        {
            //Set board values
            board[m.to.x, m.to.y] = board[m.from.x, m .from.y];
            board[m.to.x, m.to.y].position = m.to;
            board[m.from.x, m.from.y].position = m.from;
            board[m.from.x, m.from.y] = new Piece_(PieceTYPE.NONE, Colour.NONE);           
        }
    }

}
