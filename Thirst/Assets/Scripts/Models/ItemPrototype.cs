using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    public class ItemPrototype
    {
        [XmlAttribute]
        public string Name;

        [XmlAttribute]
        public string AnimationName;

        [XmlAttribute]
        public int GainHealth;

        [XmlAttribute]
        public int GainWater;
    }
}