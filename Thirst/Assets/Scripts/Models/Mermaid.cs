
using System;
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
            CardsInHand.RemoveAt(i);
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
            //TODO: newTile.TriggerWeaponSpawnProbability(tileProto, newTile);
            //TODO: newTile.TriggerItemSpawnProbability(tileProto, newTile);

            GameManager.Instance.Level.Mermaid.Discard(cardName);
            GameManager.Instance.Level.Mermaid.DrawCards(3);
            HandPanelController.Instance.Redraw();

            return newTile;
        }
    }
}