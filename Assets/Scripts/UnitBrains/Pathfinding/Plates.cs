using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnitBrains.Pathfinding
{
    public class Plate
    {
        public int X;
        public int Y;
        public const int Cost = 10;
        public int Estimate;
        public int Value;
        public Plate Parent;

        public Plate(int x, int y)
        {
            X = x;
            Y = y;
        }
        public void CalculateValue()
        {
            Value = Cost + Estimate;
        }
        public void CalculateEstimate(int targetX, int targetY)
        {
            Estimate = Math.Abs(X - targetX) + Math.Abs(Y - targetY);
        }
        public override bool Equals(object? obj)
        {
            if (obj is not Plate Plate)
            {
                return false; ;
            }

            return X == Plate.X && Y == Plate.Y;
        }
        public Vector2Int ConverterToVector2Int()
        {
            Vector2Int plateVector = new Vector2Int(X, Y);
            return plateVector;
        }
    }

}
