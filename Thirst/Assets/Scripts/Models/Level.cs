using System.Collections.Generic;
using Assets.Scripts.Managers;

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
    }
}