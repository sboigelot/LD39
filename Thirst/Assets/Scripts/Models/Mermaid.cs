using System.Collections.Generic;
using System.Diagnostics;
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

            if (!ConsumeWater(tileProto.BuildWaterCost))
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

        public bool ConsumeWater(int amount)
        {
            WaterLevel--;

            if (WaterLevel <= 0)
            {
                GameController.Instance.GameOver(GameOverReason.Thirst);
                return false;
            }
            return true;
        }

        public void Move(int mx, int my)
        {
            int newX = X + mx;
            int newY = Y + my;

            if (newX < 0 || newX >= GameManager.Instance.Level.Tiles.GetLength(1))
            {
                Debug.WriteLine("Invalid move - X out of boundary");
                return;
            }

            if (newY < 0 || newY >= GameManager.Instance.Level.Tiles.GetLength(0))
            {
                Debug.WriteLine("Invalid move - Y out of boundary");
                return;
            }

            var newTile = GameManager.Instance.Level.Tiles[newY, newX];
            if (newTile == null)
            {
                Debug.WriteLine("Invalid move - no tile");
                return;
            }
            
            var level = GameManager.Instance.Level;
            if (level.TileHasWallTo(X, Y, newX, newY) ||
                level.TileHasWallTo(newX, newY, X, Y))
            {
                Debug.WriteLine("Invalid move - wall");
                return;
            }

            if (newTile.Monster != null)
            {
                Debug.WriteLine("Invalid move - combat monster");
                CombatMonster(newTile);

                if (!ConsumeWater(1))
                {
                    Debug.WriteLine("Invalid move - no more water");
                }
                return;
            }

            var oldPositionTile =
                MapController.
                Instance.
                TileControllers.
                FirstOrDefault(tc => tc.X == X && tc.Y == Y);

            //IF no combat -> move
            X = newX;
            Y = newY;

            if (oldPositionTile != null)
            {
                oldPositionTile.Redraw();
            }
            
            var newPositionTile =
                MapController.
                Instance.
                TileControllers.
                FirstOrDefault(tc => tc.X == X && tc.Y == Y);
            
            if (newPositionTile == null)
            {
                return;
            }

            if (X == GameManager.Instance.Level.ExitLocationX &&
                Y == GameManager.Instance.Level.ExitLocationY)
            {
                GameController.Instance.GameOver(GameOverReason.Victory);
            }
            else
            {
                newPositionTile.Tile.TakeWeapon();
                newPositionTile.Tile.TakeItem();
            }

            if (!ConsumeWater(1))
            {
                Debug.WriteLine("Invalid move - no more water");
                return;
            }

            newPositionTile.Redraw();
        }

        private void CombatMonster(Tile monsterTile)
        {
            var monster = monsterTile.Monster;
            
            //deal monster attack to health
            var monsterProto = PrototypeManager.FindMonsterPrototype(monster.PrototypeName);
            Health -= monsterProto.Strength;

            if (Health <= 0)
            {
                GameController.Instance.GameOver(GameOverReason.Wound);
                return;
            }

            //deal primary attack to monster
            monster.Health -= Attack;

            //deal weapon attack to monster
            if (!string.IsNullOrEmpty(WeaponName))
            {
                var proto = PrototypeManager.FindWeaponPrototype(WeaponName);
                monster.Health -= proto.GainAttack;
            }

            if (monster.Health <= 0)
            {
                monsterTile.Monster = null;
                var tileController =
                    MapController.
                        Instance.
                        TileControllers.
                        FirstOrDefault(tc => tc.Tile == monsterTile);
                if (tileController != null)
                {
                    tileController.Redraw();
                }
            }
        }

        public void BinCard(string cardName)
        {
            var tileProto = PrototypeManager.FindTilePrototype(cardName);
            if (tileProto == null)
            {
                return;
            }

            if (!ConsumeWater(tileProto.BuildWaterCost))
            {
                return;
            }
            
            Discard(cardName);
            DrawCards(3);
        }
    }
}