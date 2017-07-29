using System;

namespace Assets.Scripts.Models
{
    [Flags]
    public enum TileConnectivity
    {
        Up = 0x1,
        Down = 0x2,
        Left = 0x4,
        Right = 0x8
    }
}