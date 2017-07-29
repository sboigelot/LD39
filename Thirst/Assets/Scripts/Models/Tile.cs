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

            var monsterProto = PrototypeManager.FindMonsterPrototype(selectedSpawn.Name);
            if (monsterProto == null)
            {
                return;
            }

            Monster = new Monster
            {
                PrototypeName = monsterProto.Name,
                AnimationName = monsterProto.AnimationName,
                Health = monsterProto.BaseHealth
            };
        }
    }
}