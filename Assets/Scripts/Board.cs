using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static GameObject[,] gameObjectBoard = new GameObject[8, 8];

    static private Piece_[,] board = new Piece_[8, 8]
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

    public static Piece_[,] BoardValues { get => board; set => board = value; }

    public void generateVisualBoard()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                GameObject tile =
                    Instantiate(Resources.Load("Pf_BoardTile"),
                    new Vector3(x - 0.5f,y - 0.5f),
                    Quaternion.identity) as GameObject;
                SpriteRenderer sprite = tile.GetComponentInChildren<SpriteRenderer>();
                tile.name = "Tile";
                if ((x + y) % 2 == 0) 
                {
                    sprite.color = new Color(189/255F, 125/255F, 75/255F);
                }
                else
                {
                    sprite.color = new Color(255/255F, 218/255F, 150/255F);
                }
            }
        }
    }

    public void generatePiecesOntoBoard()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (PiecesToOther.PieceValueToString(BoardValues[x,y]) == null)
                {
                    continue;
                }
                GameObject piece =
                    Instantiate(Resources.Load(PiecesToOther.PieceValueToString(BoardValues[x,y])),
                    new Vector3((float)x, (float)y),
                    Quaternion.identity) as GameObject;
                BoardValues[x, y].position = new Position(-1, -1);
                gameObjectBoard[x,y] = piece;
            }
        }
    }
}
