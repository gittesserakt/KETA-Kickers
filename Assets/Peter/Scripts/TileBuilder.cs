using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class TileBuilder : MonoBehaviour
{
    [SerializeField] private LayoutGrid<int> layoutGrid;
    [SerializeField] private GameObject tile;
    void Start()
    {
        int[,] shape = new int[3, 3]
{
    { 1, 1, 1 },
    { 1, 1, 1 },
    { 1, 1, 1 }
};
        List<RoomLayoutGrid<int>> list = new List<RoomLayoutGrid<int>>();
        list.Add(new RoomLayoutGrid<int>(1, new Vector2Int(1, 1), shape));
        list.Add(new RoomLayoutGrid<int>(2, new Vector2Int(4, 5), shape));



        layoutGrid = new LayoutGrid<int> (list);

        foreach (RoomLayoutGrid<int> room in layoutGrid.rooms)
        {
            for (int x = 0; x < room.tiles.GetLength(0); x++)
            {
                for (int y = 0; y < room.tiles.GetLength(1); y++)
                {
                    if (room.tiles[x,y] >= 1)
                        Instantiate(tile, new Vector3(room.position.x + x, 0, room.position.y + y), Quaternion.identity);
                }
            }
        }
    }
}
