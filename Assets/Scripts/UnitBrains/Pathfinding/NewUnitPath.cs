using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;
using Utilities;

namespace UnitBrains.Pathfinding
{
    public class NewUnitPath : BaseUnitPath
{
        private int[] dx = { -1, 0, 1, 0 };
        private int[] dy = { 0, 1, 0, -1 };
        public NewUnitPath(IReadOnlyRuntimeModel runtimeModel, Vector2Int startPoint, Vector2Int endPoint) :
            base(runtimeModel, startPoint, endPoint)
        {           
        }

        protected override void Calculate()
        {
            List<Plate> _path = FindPathAstar();
            if (_path == null || _path.Count < 2)
            {
                Debug.Log("путя нет ");
                path = new[] { startPoint };
                return;
            }
                if (_path == null)
            {
                return;
            }

          Debug.Log(_path.Count);
         
            path = ConverterPathToVectors(_path).ToArray();
        }
        private List<Plate> FindPathAstar()
        {

            Debug.Log("A*");
            Plate startPlate = new Plate(startPoint.x, startPoint.y);
            Plate endPlate = new Plate(endPoint.x, endPoint.y);

            Debug.Log($"Start {startPoint.x}  {startPoint.y} END {endPoint.x}   {endPoint.y}");
            List<Plate> openList = new List<Plate> { startPlate };
            List<Plate> closedList = new List<Plate>();

            while (openList.Count > 0)
            {
                Plate currentPlate = openList[0];

                foreach (var plate in openList)
                {
                    if (plate.Value < currentPlate.Value)
                        currentPlate = plate;
                }

                openList.Remove(currentPlate);
                closedList.Add(currentPlate);
                // возможно +1
                if (currentPlate.X == endPlate.X && currentPlate.Y == endPlate.Y)
                {
                    List<Plate> _path = new List<Plate>();

                    while (currentPlate != null)
                    {
                        _path.Add(currentPlate);
                        currentPlate = currentPlate.Parent;
                    }
                    _path.Reverse();
                    
                    return _path;
                }

                for (int i = 0; i < dx.Length; i++)
                {
                    int newX = currentPlate.X + dx[i];
                    int newY = currentPlate.Y + dy[i];

                    Vector2Int nextPlatePlace = new Vector2Int(newX, newY); 

                    if (runtimeModel.IsTileWalkable(nextPlatePlace))
                    {
                        Plate neighbor = new Plate(newX, newY);

                        if (closedList.Contains(neighbor))
                            continue;

                        neighbor.Parent = currentPlate;
                        neighbor.CalculateEstimate(endPlate.X, endPlate.Y);
                        neighbor.CalculateValue();

                        if (closedList.Contains(neighbor) || openList.Contains(neighbor))
                            continue;
                        openList.Add(neighbor);
                    }
                }

            }
            return null;
        }
        private Vector2Int[] ConverterPathToVectors(List<Plate> path)
        {
            Vector2Int[] result = new Vector2Int[path.Count];
            for (int i = 0; i < path.Count; i++)
            {
                result[i] = path[i].ConverterToVector2Int();
            }
            return result;
        }
}
}


