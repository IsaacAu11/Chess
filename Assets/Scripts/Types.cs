using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Position
{
    public int x;
    public int y;

    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
public struct Move
{
    public Position from;
    public Position to;

    public Move(Position from, Position to)
    {
        this.from = from;
        this.to = to;
    }
}
public struct Piece_
{
    public PieceTYPE piece;
    public Colour colour;
    public Position position;

    public Piece_(PieceTYPE p, Colour c)
    {
        this.piece = p;
        this.colour = c;
        this.position = new Position(0, 0);
    }
}
public enum Colour
{
    BLACK,
    WHITE,
    NONE
}

public enum PieceTYPE
{
    NONE,
    PAWN,
    ROOK,
    KNIGHT,
    BISHOP,
    QUEEN,
    KING
}