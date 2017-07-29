    using System.Collections.Generic;

namespace Assets.Scripts.Models
{
    public class TilePrototype
    {
        public string AnimationName;
        public int BuildWaterCost;
        public int Frequency;
        public List<SpawnProbability> ItemSpawnProbabilities;

        public List<SpawnProbability> MonsterSpawnProbabilities;
        public string Name;
        public TileConnectivity TileConnectivitys;
        public List<SpawnProbability> WeaponSpawnProbabilities;
    }
}