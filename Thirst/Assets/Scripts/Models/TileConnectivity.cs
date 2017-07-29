using System;

namespace Assets.Scripts.Models
{
    [Flags]
    public enum DirectionalConnectivity
    {
        None = 0,
        Left = 1,
        Right = 2,
        Up = 4,
        Down = 8
    }
}