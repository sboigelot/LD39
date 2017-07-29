    using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    public class MermaidPrototype
    {
        [XmlAttribute]
        public int BaseAttack;

        [XmlAttribute]
        public int BaseHealth;

        [XmlAttribute]
        public int BaseWater;

        [XmlAttribute]
        public string BaseWeaponName;

        [XmlAttribute]
        public int MaxHealth;

        [XmlAttribute]
        public int MaxWater;

        [XmlAttribute]
        public string AnimationName;
    }
}