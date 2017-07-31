using System;
using System.Linq;
using Assets.Scripts.Controllers;
using Assets.Scripts.Managers;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Models
{
    public class Tile
    {
        public string PrototypeName;
        public string AnimationName;
        public string Item;
        public Monster Monster;
        public string Weapon;

        public void TriggerMonsterSpawnProbability(TilePrototype tileProto)
        {
            var difficultyModifier = .025f * (GameManager.Instance.Difficulty - 1);

            var sumProb = tileProto.MonsterSpawnProbabilities.
                Sum(p => p.GetProbabilityIncreasedBy(difficultyModifier));

            if (Math.Abs(sumProb) < 0.0001f)
            {
                return;
            }

            var randomProb = Random.Range(0f, 1f);
            if (randomProb >= sumProb)
            {
                return;
            }

            var reFocusProb = randomProb * sumProb;
            SpawnProbability selectedSpawn = null;
            var selectedIndex = 0;
            while (reFocusProb >= 0)
            {
                selectedSpawn = tileProto.MonsterSpawnProbabilities[selectedIndex];
                reFocusProb -= selectedSpawn.GetProbabilityIncreasedBy(difficultyModifier);
                selectedIndex++;
            }

            var prototype = PrototypeManager.FindMonsterPrototype(selectedSpawn.Name);
            if (prototype == null)
            {
                return;
            }

            Monster = new Monster
            {
                PrototypeName = prototype.Name,
                AnimationName = prototype.AnimationName,
                Health = prototype.BaseHealth,
                Attack = prototype.Strength,
                PortraitAnimationName = prototype.PortraitAnimationName
            };
        }

        public void TriggerWeaponSpawnProbability(TilePrototype tileProto)
        {
            var sumProb = tileProto.WeaponSpawnProbabilities.Sum(p => p.Probability);

            if (Math.Abs(sumProb) < 0.0001f)
            {
                return;
            }

            var randomProb = Random.Range(0f, 1f);
            if (randomProb >= sumProb)
            {
                return;
            }

            SpawnProbability selectedSpawn = null;
            var selectedIndex = 0;
            while (randomProb >= 0)
            {
                selectedSpawn = tileProto.WeaponSpawnProbabilities[selectedIndex];
                randomProb -= selectedSpawn.Probability;
                selectedIndex++;
            }

            var prototype = PrototypeManager.FindWeaponPrototype(selectedSpawn.Name);
            if (prototype == null)
            {
                return;
            }

            Weapon = prototype.Name;
        }

        public void TriggerItemSpawnProbability(TilePrototype tileProto)
        {
            var sumProb = tileProto.ItemSpawnProbabilities.Sum(p => p.Probability);

            if (Math.Abs(sumProb) < 0.0001f)
            {
                return;
            }

            var randomProb = Random.Range(0f, 1f);
            if (randomProb >= sumProb)
            {
                return;
            }

            SpawnProbability selectedSpawn = null;
            var selectedIndex = 0;
            while (randomProb >= 0)
            {
                selectedSpawn = tileProto.ItemSpawnProbabilities[selectedIndex];
                randomProb -= selectedSpawn.Probability;
                selectedIndex++;
            }

            var prototype = PrototypeManager.FindItemPrototype(selectedSpawn.Name);
            if (prototype == null)
            {
                return;
            }

            Item = prototype.Name;
        }

        public void TakeWeapon()
        {
            if (string.IsNullOrEmpty(Weapon))
            {
                return;
            }

            //TODO confirmation swap

            var mermaid = GameManager.Instance.Level.Mermaid;
            mermaid.WeaponName = Weapon;
            Weapon = null;
        }

        public void TakeItem()
        {
            if (string.IsNullOrEmpty(Item))
            {
                return;
            }

            var proto = PrototypeManager.FindItemPrototype(Item);
            Item = null;
            if (proto == null)
            {
                return;
            }

            var mermaid = GameManager.Instance.Level.Mermaid;
            mermaid.WaterLevel = Math.Min(
                PrototypeManager.Instance.MermaidPrototype.MaxWater,
                mermaid.WaterLevel + proto.GainWater);

            mermaid.Health = Math.Min(
                            PrototypeManager.Instance.MermaidPrototype.MaxHealth,
                            mermaid.Health + proto.GainHealth);

            if (proto.GainCard > 0)
            {
                mermaid.DrawCards(mermaid.CardsInHand.Count + proto.GainCard);
                HandPanelController.Instance.Redraw();
            }
        }
    }
}