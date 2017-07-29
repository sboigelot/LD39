    using System.Collections.Generic;
    using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    public class TilePrototype
    {
        [XmlAttribute]
        public string Name;

        public TileConnectivity TileConnectivities;

        [XmlAttribute("TileConnectivity")]
        public int TileConnectivitiesSave
        {
            get { return (int) TileConnectivities; }
            set { TileConnectivities = (TileConnectivity)value; }
        }

        [XmlAttribute]
        public string AnimationName;

        [XmlAttribute]
        public int BuildWaterCost;

        [XmlAttribute]
        public int Frequency;

        [XmlElement("ItemSpawnProbability")]
        public List<SpawnProbability> ItemSpawnProbabilities;

        [XmlElement("MonsterSpawnProbability")]
        public List<SpawnProbability> MonsterSpawnProbabilities;

        [XmlElement("WeaponSpawnProbability")]
        public List<SpawnProbability> WeaponSpawnProbabilities;
    }
}