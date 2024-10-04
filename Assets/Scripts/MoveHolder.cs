using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveHolder : MonoBehaviour
{
    static public bool isTurn;
    static public bool isWhite;
    static public bool hasWhiteCastled;
    static public bool hasBlackCastled;



    public static bool checkValidMove(Move m, List<Move> moves, Piece_[,] board) 
    {
        List<Move> kingMoves = new List<Move>();

        AllowDrag.boardCheckFor(m, moves);

        for (int i = 0; i < moves.Count; i++)
        {
            //Debug.Log("item from : [" + moves[i].from.x + "," + moves[i].from.y + "] | item to : [" + moves[i].to.x + "," + moves[i].to.y + "]");           
            //If king is attacked
            if (Check(m, false, ref kingMoves,board))
            {
                if (MoveHolder.comparePossibleMoves(m, moves[i]) && MoveHolder.searchList(kingMoves,m))
                {
                    //If moving causes king to be in check
                    if (!Check(m, true, ref kingMoves, board))
                    {
                        return true;
                    }
                }
            }
            else if (MoveHolder.comparePossibleMoves(m, moves[i]))
            {
                //If moving causes king to be in check
                if (!Check(m, true, ref kingMoves, board))
                {
                    return true;
                }
            }
        }
        return false;
    }

    static public List<Move> generateMoves(Piece_[,] board)
    {
        List<Move >moves = new List<Move>();
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                Piece_ piece = board[x, y];
                if (PiecesToOther.isOwnPiece(piece))
                {
                    Position startPos = new Position(x, y);
                    switch (piece.piece)
                    {
                        case PieceTYPE.KING: //King
                            for (int a = -1; a < 2; a++)
                            {
                                for (int b = -1; b < 2; b++)
                                {
                                    moves.Add(MoveHolder.addPossibleMoves(startPos, new Position(startPos.x + a, startPos.y + b), board, false));
                                }
                            }
                            if (isWhite && hasWhiteCastled == false)
                            {
                                castle(startPos, board, false, true);
                                castle(startPos, board, false, false);
                            }
                            else if (!isWhite && hasBlackCastled == false)
                            {
                                castle(startPos, board, false, true);
                                castle(startPos, board, false, false);
                            }
                            break;
                        case PieceTYPE.QUEEN: //Queen
                            //straight 
                            moves.AddRange(MoveHolder.slidingMoves(startPos, 1, 0, board, false));
                            moves.AddRange(MoveHolder.slidingMoves(startPos, 0, 1, board, false));
                            moves.AddRange(MoveHolder.slidingMoves(startPos, -1, 0, board, false));
                            moves.AddRange(MoveHolder.slidingMoves(startPos, 0, -1, board, false));
                            //diagonal             
                            moves.AddRange(MoveHolder.slidingMoves(startPos, 1, 1, board, false));
                            moves.AddRange(MoveHolder.slidingMoves(startPos, 1, -1, board, false));
                            moves.AddRange(MoveHolder.slidingMoves(startPos, -1, -1, board, false));
                            moves.AddRange(MoveHolder.slidingMoves(startPos, -1, 1, board, false));
                            break;
                        case PieceTYPE.BISHOP: //Bishop
                            moves.AddRange(MoveHolder.slidingMoves(startPos, 1, 1, board, false));
                            moves.AddRange(MoveHolder.slidingMoves(startPos, 1, -1, board, false));
                            moves.AddRange(MoveHolder.slidingMoves(startPos, -1, -1, board, false));
                            moves.AddRange(MoveHolder.slidingMoves(startPos, -1, 1, board, false));
                            break;
                        case PieceTYPE.KNIGHT: //Knight
                            moves.AddRange(MoveHolder.addKnightMoves(startPos, board, false));
                            break;
                        case PieceTYPE.ROOK: //Rook
                            moves.AddRange(MoveHolder.slidingMoves(startPos, 1, 0, board, false));
                            moves.AddRange(MoveHolder.slidingMoves(startPos, 0, 1, board, false));
                            moves.AddRange(MoveHolder.slidingMoves(startPos, -1, 0, board, false));
                            moves.AddRange(MoveHolder.slidingMoves(startPos, 0, -1, board, false));
                            break;
                        case PieceTYPE.PAWN: //Pawn
                            moves.AddRange(MoveHolder.addPawnMoves(startPos, board, false));
                            break;
                    }
                }
            }
        }
        for (int i = 0; i < moves.Count; i++)
        {
            if (moves[i].Equals(new Move()))
            {
                moves.RemoveAt(i);
            }
        }
        return moves;
    }

    static public bool searchList(List<Move> list, Move move)
    {
        foreach (Move item in list)
        {
            if (item.to.Equals(move.to))
            {
                return true;
            }
        }
        return false;
    }

    static public Position getKingPos(Piece_[,] board)
    {
        Colour c = isWhite ? Colour.WHITE : Colour.BLACK;
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (board[x, y].piece == PieceTYPE.KING && board[x, y].colour == c)
                {
                    return new Position(x, y);
                }
            }
        }
        return new Position(0, 0);
    }

    static public bool comparePossibleMoves(Move m1, Move m2)
    {
        return m1.Equals(m2);
    }

    static public bool tileIsEmpty(Position p,Piece_[,] board)
    {
        if (!(p.x > 7 || p.x < 0 || p.y > 7 || p.y < 0))
        {
            if (board[p.x,p.y].piece == new Piece_(PieceTYPE.NONE,Colour.NONE).piece)
            {
                return true;
            }
        }
        return false;
    }

    static public Move addPossibleMoves(Position startPos,Position endPos, Piece_[,] board, bool check)
    {
        if (!(endPos.x > 7 || endPos.x < 0 || endPos.y > 7 || endPos.y < 0))
        {
            if (!PiecesToOther.isOwnPiece(board[endPos.x, endPos.y])) 
            {
                return new Move(startPos, endPos);
            }
        }
        return new Move();
    }

    static public List<Move> slidingMoves(Position startPos, int xDirection, int yDirection, Piece_[,] board, bool check)
    {
        List<Move> m = new List<Move>();
        for (int a = 1; a < 8; a++)
        {
            Position moved = new Position(startPos.x + (xDirection * a), startPos.y + (yDirection * a));
            if (moved.x > 7 || moved.x < 0 || moved.y > 7 || moved.y < 0) break;
            if (board[moved.x, moved.y].piece != new Piece_(PieceTYPE.NONE, Colour.NONE).piece) 
            {
                if (!PiecesToOther.isOwnPiece(board[moved.x,moved.y]))
                {
                    m.Add(addPossibleMoves(startPos, new Position(moved.x, moved.y), board, check));
                }
                break;
            }
            m.Add(addPossibleMoves(startPos, new Position(moved.x, moved.y), board, check));
        }
        return m;
    }
 
    public static List<Move> addKnightMoves(Position startPos, Piece_[,] board, bool check)
    {
        List<Move> m = new List<Move>();
        int[,] pointer = new int[8, 2]
        {
            {2,1},
            {2,-1},
            {-2,1},
            {-2,-1},
            {1,2},
            {-1,2},
            {1,-2},
            {-1,-2}
        };
        for (int i = 0; i < 8; i++)
        {
            m.Add(addPossibleMoves(startPos, new Position(startPos.x + pointer[i, 0], startPos.y + pointer[i, 1]), board, check));           
        }
        return m;
    }

    public static List<Move> addAngularAttackPawn(Position p,Colour c, Piece_[,] board, bool check)
    {
        List<Move> m = new List<Move>();
        int[,] PawnMoves = new int[,]
            {
                {1,1 },
                {-1,1 },
                {1,-1 },
                {-1,-1 }
            };

        if (c == Colour.WHITE)
        {
            for (int i = 0; i < 2; i++)
            {
                if (!(p.x + PawnMoves[i, 0] > 7 || p.x + PawnMoves[i, 0] < 0 || p.y + PawnMoves[i, 1] > 7 || p.y + PawnMoves[i, 1] < 0))
                {
                    if (!PiecesToOther.isOwnPiece(board[p.x + PawnMoves[i, 0], p.y + PawnMoves[i, 1]]) && !tileIsEmpty(new Position(p.x + PawnMoves[i,0],p.y + PawnMoves[i,1]),board))
                    {
                        m.Add(MoveHolder.addPossibleMoves(p, new Position(p.x + PawnMoves[i, 0], p.y + PawnMoves[i, 1]), board, check));
                    }
                }
            }
        }
        if (c == Colour.BLACK)
        {
            for (int i = 2; i < 4; i++)
            {
                if (!(p.x + PawnMoves[i, 0] > 7 || p.x + PawnMoves[i, 0] < 0 || p.y + PawnMoves[i, 1] > 7 || p.y + PawnMoves[i, 1] < 0))
                {
                    if (!PiecesToOther.isOwnPiece(board[p.x + PawnMoves[i, 0], p.y + PawnMoves[i, 1]]) && !tileIsEmpty(new Position(p.x + PawnMoves[i, 0], p.y + PawnMoves[i, 1]), board))
                    {
                        m.Add(MoveHolder.addPossibleMoves(p, new Position(p.x + PawnMoves[i, 0], p.y + PawnMoves[i, 1]), board, check));
                    }
                }
            }
        }
        return m;
    }

    public static List<Move> addPawnMoves(Position pos, Piece_[,] board, bool check)
    {
        List<Move> m = new List<Move>();
        if (board[pos.x,pos.y].colour == Colour.WHITE)
        {
            bool pawnMove = MoveHolder.tileIsEmpty(new Position(pos.x, pos.y + 1), board);
            if (pawnMove)
            {;
                m.Add(MoveHolder.addPossibleMoves(pos, new Position(pos.x, pos.y + 1), board, check));
            }
            if (pos.y == 1 && pawnMove && MoveHolder.tileIsEmpty(new Position(pos.x, pos.y + 2), board))
            {
                m.Add(MoveHolder.addPossibleMoves(pos, new Position(pos.x, pos.y + 2), board, check));
            }
            m.AddRange(MoveHolder.addAngularAttackPawn(pos, board[pos.x, pos.y].colour, board, check));
        }
        else
        {
            bool pawnMove = MoveHolder.tileIsEmpty(new Position(pos.x, pos.y - 1), board);
            if (pawnMove)
            {
                m.Add(MoveHolder.addPossibleMoves(pos, new Position(pos.x, pos.y - 1), board, check));
            }
            if (pos.y == 6 && pawnMove && MoveHolder.tileIsEmpty(new Position(pos.x, pos.y - 2), board))
            {
                m.Add(MoveHolder.addPossibleMoves(pos, new Position(pos.x, pos.y - 2), board, check));
            }
            m.AddRange(MoveHolder.addAngularAttackPawn(pos, board[pos.x, pos.y].colour, board, check));
        }
        return m;
    }

    static public bool Check(Move m, bool pieceBehind, ref List<Move> kingMoves, Piece_[,] board)
    {
        int kingMoveCounter = 0;
        kingMoves = new List<Move>();
        Piece_[,] tempBoard = new Piece_[8, 8];
        Array.Copy(board, tempBoard, 64);
        Colour c = isWhite ? Colour.WHITE : Colour.BLACK;
        //set temp board positions
        if (pieceBehind)
        {
            tempBoard[m.to.x, m.to.y] = tempBoard[m.from.x, m.from.y];
            tempBoard[m.to.x, m.to.y].position = m.to;
            tempBoard[m.from.x, m.from.y] = new Piece_(PieceTYPE.NONE, Colour.NONE);
        }

        //Find king position
        Position kingPos = getKingPos(tempBoard);
        //go through every piece and find if it can attack another of the same piece
        Piece_[] piece = new Piece_[] 
            {new Piece_(PieceTYPE.KING,c)
            ,new Piece_(PieceTYPE.QUEEN,c)
            ,new Piece_(PieceTYPE.BISHOP,c)
            ,new Piece_(PieceTYPE.KNIGHT,c)
            ,new Piece_(PieceTYPE.ROOK,c)
            ,new Piece_(PieceTYPE.PAWN,c) };

        for (int i = 0; i < 6; i++)
        {
            tempBoard[kingPos.x,kingPos.y].piece = piece[i].piece;
            switch (piece[i].piece)
            {
                case PieceTYPE.KING: //King
                    for (int a = -1; a < 2; a++)
                    {
                        for (int b = -1; b < 2; b++)
                        {
                            kingMoves.Add(MoveHolder.addPossibleMoves(kingPos, new Position(kingPos.x + a, kingPos.y + b), tempBoard, true));
                        }
                    }
                    break;
                case PieceTYPE.QUEEN: //Queen
                    //straight 
                    kingMoves.AddRange(MoveHolder.slidingMoves(kingPos, 1, 0, tempBoard, true));
                    kingMoves.AddRange(MoveHolder.slidingMoves(kingPos, 0, 1, tempBoard, true));
                    kingMoves.AddRange(MoveHolder.slidingMoves(kingPos, -1, 0, tempBoard, true));
                    kingMoves.AddRange(MoveHolder.slidingMoves(kingPos, 0, -1, tempBoard, true));
                    //diagonal             
                    kingMoves.AddRange(MoveHolder.slidingMoves(kingPos, 1, 1, tempBoard, true));
                    kingMoves.AddRange(MoveHolder.slidingMoves(kingPos, 1, -1, tempBoard, true));
                    kingMoves.AddRange(MoveHolder.slidingMoves(kingPos, -1, -1, tempBoard, true));
                    kingMoves.AddRange(MoveHolder.slidingMoves(kingPos, -1, 1, tempBoard, true));
                    break;
                case PieceTYPE.BISHOP: //Bishop
                    kingMoves.AddRange(MoveHolder.slidingMoves(kingPos, 1, 1, tempBoard, true));
                    kingMoves.AddRange(MoveHolder.slidingMoves(kingPos, 1, -1, tempBoard, true));
                    kingMoves.AddRange(MoveHolder.slidingMoves(kingPos, -1, -1, tempBoard, true));
                    kingMoves.AddRange(MoveHolder.slidingMoves(kingPos, -1, 1, tempBoard, true));
                    break;
                case PieceTYPE.KNIGHT: //Knight
                    kingMoves.AddRange(MoveHolder.addKnightMoves(kingPos, tempBoard, true));
                    break;
                case PieceTYPE.ROOK: //Rook
                    kingMoves.AddRange(MoveHolder.slidingMoves(kingPos, 1, 0, tempBoard, true));
                    kingMoves.AddRange(MoveHolder.slidingMoves(kingPos, 0, 1, tempBoard, true));
                    kingMoves.AddRange(MoveHolder.slidingMoves(kingPos, -1, 0, tempBoard, true));
                    kingMoves.AddRange(MoveHolder.slidingMoves(kingPos, 0, -1, tempBoard, true));
                    break;
                case PieceTYPE.PAWN: //Pawn
                    kingMoves.AddRange(MoveHolder.addPawnMoves(kingPos, tempBoard, true));
                    break;
            }
            for (int a = kingMoveCounter; a < kingMoves.Count; a++)
            {
                if (tempBoard[kingMoves[kingMoveCounter].to.x, kingMoves[kingMoveCounter].to.y].piece == piece[i].piece)
                {
                    return true;
                }
                kingMoveCounter++;
            }
        }
        return false;
    }

    static public Move castle(Position pos, Piece_[,] board, bool check,bool right)
    {
        bool isValid = true;
        int kingDirection;
        List<Move> kingMoves = new List<Move>();

        if (right)
        {
            kingDirection = 1;
        }
        else
        {
            kingDirection = -1;
        }

        if (board[pos.x,pos.y].position.Equals(new Position(-1,-1)))
        {
            for (int i = 1; i < 3; i++)
            {
                if (Board.BoardValues[pos.x + (i * kingDirection), pos.y].piece != PieceTYPE.NONE)
                {
                    isValid = false;
                    break;
                }
            }
            if (isValid)//true
            {
                for (int i = 0; i < 2; i++)
                {
                    if (Check(new Move(new Position(pos.x + (i * kingDirection), pos.y), new Position(pos.x + ((i + 1) * kingDirection), pos.y)), true, ref kingMoves, board))
                    {
                        isValid = false;
                        break;
                    }
                }
                if (isValid)
                {
                    return MoveHolder.addPossibleMoves(pos, new Position(pos.x + (2 * kingDirection),pos.y), board, check);
                }
            }        
        }
        return new Move();
    }

    public static bool isCheckmate(Piece_[,] board)
    {
        List<Move> kingMoves = new List<Move>();
        if (MoveHolder.Check(new Move(), false, ref kingMoves, board)) 
        {
            Position pos = getKingPos(Board.BoardValues);
            for (int a = -1; a < 2; a++)
            {
                for (int b = -1; b < 2; b++)
                {
                    kingMoves.Add(MoveHolder.addPossibleMoves(pos, new Position(pos.x + a, pos.y + b), Board.BoardValues, true));
                }
            }
            foreach (Move item in kingMoves)
            {
                if (!Check(item, true, ref kingMoves, board)) 
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }
}

    