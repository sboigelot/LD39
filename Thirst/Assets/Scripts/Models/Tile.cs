using System;
using System.Linq;
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
            var sumProb = tileProto.MonsterSpawnProbabilities.Sum(p => p.Probability);

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
                selectedSpawn = tileProto.MonsterSpawnProbabilities[selectedIndex];
                randomProb -= selectedSpawn.Probability;
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
                Health = prototype.BaseHealth
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
            //TODO
        }

        public void TakeItem()
        {
            //TODO
        }
    }
}