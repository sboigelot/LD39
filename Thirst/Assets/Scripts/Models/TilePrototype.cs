using System.Collections.Generic;
using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    public class TilePrototype
    {
        [XmlAttribute]
        public string Name;

        public DirectionalConnectivity DirectionalConnectivity;

        [XmlAttribute("DirectionalConnectivity")]
        public string DirectionalConnectivitySave
        {
            set
            {
                DirectionalConnectivity = DirectionalConnectivity.None;
                if (value.Contains("L"))
                    DirectionalConnectivity |= DirectionalConnectivity.Left;
                if (value.Contains("R"))
                    DirectionalConnectivity |= DirectionalConnectivity.Right;
                if (value.Contains("U"))
                    DirectionalConnectivity |= DirectionalConnectivity.Up;
                if (value.Contains("D"))
                    DirectionalConnectivity |= DirectionalConnectivity.Down;
            }
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