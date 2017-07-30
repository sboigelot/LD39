using System;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class Level
    {
        public Mermaid Mermaid;

        public Tile[,] Tiles;

        public Level()
        {
            Tiles = new Tile[9, 14];

            var protoMermaid = PrototypeManager.Instance.MermaidPrototype;
            Mermaid = new Mermaid
            {
                CardsInHand = new List<string>(),
                Health = protoMermaid.MaxHealth,
                Attack = protoMermaid.BaseAttack,
                WaterLevel = protoMermaid.BaseWater,
                WeaponName = protoMermaid.BaseWeaponName,
                X = 7, //TODO randomize
                Y = 3 //TODO randomize
            };

            Tiles[Mermaid.Y, Mermaid.X] = Mermaid.UseCard("StatupCard");
            Mermaid.DrawCards(3);
        }

        public bool TileExist(int x, int y)
        {
            if (Tiles == null)
            {
                return false;
            }

            if (y < 0 || y >= Tiles.GetLength(0))
            {
                return false;
            }

            if (x < 0 || x >= Tiles.GetLength(1))
            {
                return false;
            }

            return Tiles[y, x] != null;
        }

        public bool TileHasWallTo(int xTileOri, int yTileOri, int xTileDes, int yTileDes)
        {
            if (!TileExist(xTileOri, yTileOri))
            {
                return true;
            }

            int yDiff = yTileOri - yTileDes;
            int xDiff = xTileOri - xTileDes;
            if (Math.Abs(yDiff) > 1 ||
                Math.Abs(xDiff) > 1 &&
                Math.Abs(yDiff) + Math.Abs(yDiff) <= 1)
            {
                return true;
            }

            var oriConnectivity = DirectionalConnectivity.None;
            if (yDiff == -1)
                oriConnectivity |= DirectionalConnectivity.Down;
            if (yDiff == 1)
                oriConnectivity |= DirectionalConnectivity.Up;
            if (xDiff == -1)
                oriConnectivity |= DirectionalConnectivity.Right;
            if (xDiff == 1)
                oriConnectivity |= DirectionalConnectivity.Left;

            var oriTile = Tiles[yTileOri, xTileOri];
            var oriProto = PrototypeManager.FindTilePrototype(oriTile.PrototypeName);
            if (oriProto == null)
            {
                return true;
            }

            return (oriProto.DirectionalConnectivity & oriConnectivity) != oriConnectivity;
        }
    }
}