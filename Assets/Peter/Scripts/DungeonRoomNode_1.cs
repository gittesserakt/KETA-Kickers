using UnityEngine;

public class DungeonRoomNode<TRoom>
{
    public TRoom Room { get; }
    public int Id { get; }
    public string R{ get; }

    public DungeonRoomNode(int id, TRoom room)
    {
        Id = id;
        Room = room;
    }
}
