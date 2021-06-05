using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position 
{
    public int x { get; set; }
    public int y { get; set; }

    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public Position(Position _pos)
    {
        x = _pos.x;
        y = _pos.y;
    }

   
    public static Position operator+ (Position _pos1, Position _pos2)
    {
        return new Position(_pos1.x + _pos2.x, _pos1.y + _pos2.y);
    }
    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is Position))
            return false;
        Position pos = (Position)obj;
        return pos.x == x && pos.y == y;

    }

    public override int GetHashCode()
    {
        return 31 * x.GetHashCode() + 31 * y.GetHashCode();
    }

    public override string ToString()
    {
        return $"[{x},{y}]";
    }
}
