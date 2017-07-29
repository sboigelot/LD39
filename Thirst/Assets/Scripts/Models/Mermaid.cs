using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Controllers;
using Assets.Scripts.Managers;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Models
{
    public class Mermaid
    {
        public int Attack;

        public List<string> CardsInHand;
        public int Health;
        public int WaterLevel;
        public string WeaponName;

        public int X;
        public int Y;

        public string CurrentAnimation = "SDAnim";

        public Mermaid()
        {
            CardsInHand = new List<string>();
        }

        public void DrawCards(int upToHandCount)
        {
            while (CardsInHand.Count < upToHandCount)
            {
                DrawCard();
            }
        }

        private void DrawCard()
        {
            var allCards = PrototypeManager.Instance.TilePrototypes;

            int sumFrequencies = allCards.Sum(c => c.Frequency);
            int randomFrequency = Random.Range(0, sumFrequencies);

            int cardIndex = 0;
            while (randomFrequency >= 0)
            {
                randomFrequency -= allCards[cardIndex].Frequency;
                cardIndex++;
            }

            var pickedCard = allCards[cardIndex - 1];
            CardsInHand.Add(pickedCard.Name);
        }

        public void Discard(string tileName)
        {
            var i = CardsInHand.IndexOf(tileName);
            if (i >= 0)
            {
                CardsInHand.RemoveAt(i);
            }
        }

        public Tile UseCard(string cardName)
        {
            var tileProto = PrototypeManager.FindTilePrototype(cardName);
            if (tileProto == null)
            {
                return null;
            }

            var newTile = new Tile
            {
                PrototypeName = tileProto.Name,
                AnimationName = tileProto.AnimationName
            };

            newTile.TriggerMonsterSpawnProbability(tileProto);
            newTile.TriggerWeaponSpawnProbability(tileProto);
            newTile.TriggerItemSpawnProbability(tileProto);

            Discard(cardName);
            DrawCards(3);
            HandPanelController.Instance.Redraw();

            return newTile;
        }

        public void Move(int mx, int my)
        {
            //TODO check if move in bounds
            //TODO check if move valid

            //TODO eventually trigger combat
             
            var oldPositionTile =
                MapController.
                Instance.
                TileControllers.
                FirstOrDefault(tc => tc.X == X && tc.Y == Y);

            //IF no combat -> move
            X += mx;
            Y += my;

            if (oldPositionTile != null)
            {
                oldPositionTile.Redraw();
            }

            //TODO trigger take weapon / items.

            var newPositionTile =
                MapController.
                Instance.
                TileControllers.
                FirstOrDefault(tc => tc.X == X && tc.Y == Y);
            if (newPositionTile != null)
            {
                newPositionTile.Redraw();
            }
        }
    }
}