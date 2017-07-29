using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    public class WeaponPrototype
    {
        [XmlAttribute]
        public string Name;

        [XmlAttribute]
        public string AnimationName;

        [XmlAttribute]
        public string GainAttack;
    }
}