using System;
using System.Collections.Generic;
using UnityEngine;

public class LayoutGrid <TRoom>
{
    public List<RoomLayoutGrid<TRoom>> rooms { get; }

    public LayoutGrid(List<RoomLayoutGrid<TRoom>> rooms)
    {
        this.rooms = rooms;
    }
}
