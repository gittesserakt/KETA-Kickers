using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RoomLayoutGrid<TRoom>
{
    public TRoom room { get; }


    public int[,] tiles;

    public Vector2Int position { get; }

    public bool isCorridor { get; }

    public RoomLayoutGrid(TRoom room, Vector2Int position, int[,] tiles, bool isCorridor = true)
    {
        this.room = room;
        this.position = position;
        this.tiles = tiles;
        this.isCorridor = isCorridor;
    }
}
