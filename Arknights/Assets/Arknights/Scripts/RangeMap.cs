using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeMap : MonoBehaviour
{
    public static Transform[,] Tile;
    public static int MaxSizeX = 6;
    public static int MaxSizeY = 10;
    void Awake()
    {
        int Count = 0;
        Tile = new Transform[MaxSizeX + 1, MaxSizeY + 1];
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                Tile[i, j] = transform.GetChild(Count);

                Count++;

            }
        }
    }
}
