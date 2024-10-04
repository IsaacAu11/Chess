using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PiecesToOther
{
    static public string PieceValueToString(Piece_ p)
    {
        string stringValue = "";
        if (p.colour == Colour.BLACK)
        {
            switch (p.piece)
            {
                default:
                    stringValue = null;
                    break;
                case PieceTYPE.KING:
                    stringValue = "B_King";
                    break;
                case PieceTYPE.QUEEN:
                    stringValue = "B_Queen";
                    break;
                case PieceTYPE.BISHOP:
                    stringValue = "B_Bishop";
                    break;
                case PieceTYPE.KNIGHT:
                    stringValue = "B_Knight";
                    break;
                case PieceTYPE.ROOK:
                    stringValue = "B_Rook";
                    break;
                case PieceTYPE.PAWN:
                    stringValue = "B_Pawn";
                    break;
            }
        }
        else
        {
            switch (p.piece)
            {
                default:
                    stringValue = null;
                    break;
                case PieceTYPE.KING:
                    stringValue = "W_King";
                    break;
                case PieceTYPE.QUEEN:
                    stringValue = "W_Queen";
                    break;
                case PieceTYPE.BISHOP:
                    stringValue = "W_Bishop";
                    break;
                case PieceTYPE.KNIGHT:
                    stringValue = "W_Knight";
                    break;
                case PieceTYPE.ROOK:
                    stringValue = "W_Rook";
                    break;
                case PieceTYPE.PAWN:
                    stringValue = "W_Pawn";
                    break;
            }
        }
        return stringValue;
    }
    static public bool isOwnPiece(Piece_ p)
    {
        if ((MoveHolder.isWhite && p.colour == Colour.WHITE) || (!MoveHolder.isWhite && p.colour == Colour.BLACK)) 
        {
            return true;
        }
        return false;
    }

    static public int[,] PawnTable = new int[8, 8]
    {
        {0,5,5,0,5,10,50,0},
        {0,10,-5,0,5,10,50,0},
        {0,10,-10,0,10,20,50,0},
        {0,-21,0,20,25,30,50,0},
        {0,-21,0,20,25,30,50,0},
        {0,10,-10,0,10,20,50,0},
        {0,10,-5,0,5,10,50,0},
        {0,5,5,0,5,10,50,0},
    };
    static public int[,] KnightTable = new int[8, 8]
    {
        {-50,-40,-30,-30,-30,-30,-40,-50 },
        {-40,-20,5,0,5,0,-20,-40 },
        {-30,0,10,15,15,10,0,-30 },
        {-30,5,15,20,20,15,0,-30 },
        {-30,5,15,20,20,15,0,-30 },
        {-30,0,10,15,15,10,0,-30 },
        {-40,-20,5,0,5,0,-20,-40 },
        {-50,-40,-30,-30,-30,-30,-40,-50 }
    };
    static public int[,] QueenTable = new int[8, 8]
    {
        {-20,-10,-10,0,-5,-10,-10,-20 },
        {-10,0,5,0,0,0,0,-10 },
        {-10,5,5,5,5,5,0,-10 },
        {-5,0,5,5,5,5,0,-5 },
        {-5,0,5,5,5,5,0,-5 },
        {-10,0,5,5,5,5,0,-10 },
        {-10,0,0,0,0,0,0,-10 },
        {-20,-10,-10,-5,-5,-10,-10,-20 },
    };
    static public int[,] BishopTable = new int[8, 8]
    {
        {-20,-10,-10,-10,-10,-10,-10,-20 },
        {-10,5,10,0,5,0,0,-10 },
        {-10,0,10,10,5,5,0,-10 },
        {-10,0,10,10,10,10,0,-10 },
        {-10,0,10,10,10,10,0,-10 },
        {-10,0,10,10,5,5,0,-10 },
        {-10,5,10,0,5,0,0,-10 },
        {-20,-10,-10,-10,-10,-10,-10,-20 },
    };
    static public int[,] RookTable = new int[8, 8]
    {
        {0,-5,-5,-5,-5,-5,5,0 },
        {0,0,0,0,0,0,10,0 },
        {0,0,0,0,0,0,10,0 },
        {5,0,0,0,0,0,10,0 },
        {5,0,0,0,0,0,10,0 },
        {0,0,0,0,0,0,10,0 },
        {0,0,0,0,0,0,10,0 },
        {0,-5,-5,-5,-5,-5,5,0 },
    };
    static public int[,] KingTable = new int[8, 8]
    {
        {20,20,-10,-20,-30,-30,-30,-30 },
        {30,20,-20,-30,-40,-40,-40,-40 },
        {10,0,-20,-30,-40,-40,-40,-40 },
        {0,0,-20,-40,-50,-50,-50,-50 },
        {0,0,-20,-40,-50,-50,-50,-50 },
        {10,0,-20,-30,-40,-40,-40,-40 },
        {30,20,-20,-30,-40,-40,-40,-40 },
        {20,20,-10,-20,-30,-30,-30,-30 },
    };
}


