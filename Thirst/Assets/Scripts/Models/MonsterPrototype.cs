using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    public class MonsterPrototype
    {
        [XmlAttribute]
        public string Name;

        [XmlAttribute]
        public string AnimationName;

        [XmlAttribute]
        public int BaseHealth;

        [XmlAttribute]
        public int Strength;
    }
}