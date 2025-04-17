using System;
using UnityEngine;

[Serializable]
public class DungeonRoom : IEquatable<DungeonRoom>
{
    public string Id;
    public Vector3 Position;

    public DungeonRoom(string id, Vector3 position)
    {
        Id = id;
        Position = position;
    }

    public bool Equals(DungeonRoom other)
    {
        if (other == null) return false;
        return Id == other.Id;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as DungeonRoom);
    }

    public override int GetHashCode()
    {
        return Id != null ? Id.GetHashCode() : 0;
    }

    public override string ToString()
    {
        return $"Room({Id})";
    }
}
