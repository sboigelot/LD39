
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers;
using UnityEngine;

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
    }
}