using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
    public void OnClick()
    {
        Board.BoardValues = new Piece_[8, 8]
        {
        {new Piece_(PieceTYPE.ROOK,Colour.WHITE),new Piece_(PieceTYPE.PAWN,Colour.WHITE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.PAWN,Colour.BLACK),new Piece_(PieceTYPE.ROOK,Colour.BLACK)},
        {new Piece_(PieceTYPE.KNIGHT,Colour.WHITE),new Piece_(PieceTYPE.PAWN,Colour.WHITE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.PAWN,Colour.BLACK),new Piece_(PieceTYPE.KNIGHT,Colour.BLACK)},
        {new Piece_(PieceTYPE.BISHOP,Colour.WHITE),new Piece_(PieceTYPE.PAWN,Colour.WHITE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.PAWN,Colour.BLACK),new Piece_(PieceTYPE.BISHOP,Colour.BLACK)},
        {new Piece_(PieceTYPE.QUEEN,Colour.WHITE),new Piece_(PieceTYPE.PAWN,Colour.WHITE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.PAWN,Colour.BLACK),new Piece_(PieceTYPE.QUEEN,Colour.BLACK)},
        {new Piece_(PieceTYPE.KING,Colour.WHITE),new Piece_(PieceTYPE.PAWN,Colour.WHITE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.PAWN,Colour.BLACK),new Piece_(PieceTYPE.KING,Colour.BLACK)},
        {new Piece_(PieceTYPE.BISHOP,Colour.WHITE),new Piece_(PieceTYPE.PAWN,Colour.WHITE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.PAWN,Colour.BLACK),new Piece_(PieceTYPE.BISHOP,Colour.BLACK)},
        {new Piece_(PieceTYPE.KNIGHT,Colour.WHITE),new Piece_(PieceTYPE.PAWN,Colour.WHITE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.PAWN,Colour.BLACK),new Piece_(PieceTYPE.KNIGHT,Colour.BLACK)},
        {new Piece_(PieceTYPE.ROOK,Colour.WHITE),new Piece_(PieceTYPE.PAWN,Colour.WHITE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.NONE,Colour.NONE),new Piece_(PieceTYPE.PAWN,Colour.BLACK),new Piece_(PieceTYPE.ROOK,Colour.BLACK)},
        };
        SceneManager.LoadScene(0);
    }

}
