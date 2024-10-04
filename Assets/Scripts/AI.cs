using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AI 
{
    static public bool AIstarted = true;
    public const int Max = 10000000;
    public const int Min = -10000000;

    static public int evaluateBoard(Piece_[,] board)
    {
        int eval = 0;

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {               
                if (board[x,y].colour == Colour.WHITE)
                {
                    switch (board[x,y].piece)
                    {
                        case PieceTYPE.NONE:
                            break;
                        case PieceTYPE.KING:
                            eval += PiecesToOther.KingTable[x, y];
                            break;
                        case PieceTYPE.QUEEN:
                            eval += PiecesToOther.QueenTable[x, y];
                            break;
                        case PieceTYPE.BISHOP:
                            eval += PiecesToOther.BishopTable[x, y];
                            break;
                        case PieceTYPE.KNIGHT:
                            eval += PiecesToOther.KnightTable[x, y];
                            break;
                        case PieceTYPE.ROOK:
                            eval += PiecesToOther.RookTable[x, y];
                            break;
                        case PieceTYPE.PAWN:
                            eval += PiecesToOther.PawnTable[x, y];
                            break;
                    }
                }
                else if (board[x,y].colour == Colour.BLACK)
                {
                    switch (board[x, y].piece)
                    {
                        case PieceTYPE.NONE:
                            break;
                        case PieceTYPE.KING:
                            eval -= PiecesToOther.KingTable[x, 7 - y];
                            break;
                        case PieceTYPE.QUEEN:
                            eval -= PiecesToOther.QueenTable[x, 7 - y];
                            break;
                        case PieceTYPE.BISHOP:
                            eval -= PiecesToOther.BishopTable[x, 7 - y];
                            break;
                        case PieceTYPE.KNIGHT:
                            eval -= PiecesToOther.KnightTable[x, 7 - y];
                            break;
                        case PieceTYPE.ROOK:
                            eval -= PiecesToOther.RookTable[x, 7 - y];
                            break;
                        case PieceTYPE.PAWN:
                            eval -= PiecesToOther.PawnTable[x, 7 - y];
                            break;
                    }
                } 
            }
        }
        return eval;
    }

    static public int evaluateMove(Piece_[,] board)
    {
        int eval = 0;

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                int colour = board[x,y].colour == Colour.WHITE ? 1 : -1;

                switch (board[x,y].piece)
                {
                    default:
                        break;
                    case PieceTYPE.KING:
                        eval += 20000 * colour;
                        break;
                    case PieceTYPE.QUEEN:
                        eval += 900 * colour;
                        break;
                    case PieceTYPE.BISHOP:
                        eval += 330 * colour;
                        break;
                    case PieceTYPE.KNIGHT:
                        eval += 320 * colour;
                        break;
                    case PieceTYPE.ROOK:
                        eval += 500 * colour;
                        break;
                    case PieceTYPE.PAWN:
                        eval += 100 * colour;
                        break;
                }
            }
        }
        return eval;
    }



    static public int minimax(Piece_[,] board,int depth, bool maximizingPlayer, int alpha, int beta, Move lastMove, ref Move optimalMove)
    {
        //white is maximiser, black is minimiser
        Piece_[,] tempBoard = new Piece_[8,8];
        Array.Copy(board, tempBoard,64);

        //return the board evaluation
        if (depth == 3)
        {
            return evaluateMove(tempBoard) + evaluateBoard(tempBoard);
        }

        List<Move> move = MoveHolder.generateMoves(tempBoard);

        if (depth == 0)
        {
            //use the minimisingPlayer
            int best = Max;
            for (int i = 0; i < move.Count; i++)
            {
                AllowDrag.setValues(move[i], tempBoard);
                if (MoveHolder.checkValidMove(move[i],move, tempBoard))
                {
                    //set the board values to the temp board
                    int eval = minimax(tempBoard, depth + 1, true, alpha, beta, move[i],ref optimalMove);
                    if (eval < best)
                    {
                        best = eval;
                        optimalMove = move[i];
                    }
                    beta = Mathf.Min(beta, best);
                    //alpha beta pruning
                    if (beta <= alpha)
                    {
                        Debug.Log("branches pruned : ");
                        break;
                    }
                }
                else
                {
                    move.Remove(move[i]);
                }
            }
            return best;
        }

        //------------ Recursive function --------------

        else if (maximizingPlayer)
        {
            int best = Min;
            for (int i = 0; i < move.Count; i++)
            {
                AllowDrag.setValues(move[i], tempBoard);
                if (MoveHolder.checkValidMove(move[i], move, tempBoard))
                {
                    //set the board values to the temp board
                    int eval = minimax(tempBoard, depth + 1, false, alpha, beta, move[i], ref optimalMove);
                    best = Math.Max(eval, best);
                    alpha = Mathf.Max(alpha, best);
                    //alpha beta pruning
                    if (beta <= alpha)
                    {
                        Debug.Log("branches pruned : ");
                        break;
                    }
                }
                else
                {
                    move.Remove(move[i]);
                }
            }
            return best;
        }
        else
        {
            int best = Max;
            for (int i = 0; i < move.Count; i++)
            {
                AllowDrag.setValues(move[i], tempBoard);
                if (MoveHolder.checkValidMove(move[i], move, tempBoard))
                {
                    //set the board values to the temp board
                    int eval = minimax(tempBoard, depth + 1, true, alpha, beta, move[i], ref optimalMove);
                    best = Math.Min(eval, best);
                    beta = Mathf.Min(beta, best);
                    //alpha beta pruning
                    if (beta <= alpha)
                    {
                        Debug.Log("branches pruned : ");
                        break;
                    }
                }
                else
                {
                    move.Remove(move[i]);
                }
            }
            return best;
        }
    }
}
