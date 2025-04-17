using UnityEngine;

public enum TileType
{
    Empty,
    Floor,
    Wall,
    Door
}

[System.Serializable]
public class RoomLayout
{
    public int width;
    public int height;
    public TileType[,] tiles;

    public RoomLayout(int width, int height)
    {
        this.width = width;
        this.height = height;
        tiles = new TileType[width, height];
    }

    public RoomLayout(TileType[,] tiles)
    {
        width = tiles.GetLength(0);
        height = tiles.GetLength(1);
        this.tiles = new TileType[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                this.tiles[x, y] = tiles[x, y];
            }
        }
    }
}