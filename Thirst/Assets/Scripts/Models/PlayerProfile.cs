using System;
using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    public class PlayerProfile : ICloneable
    {
        [XmlAttribute]
        public string Name { get; set; }

        public PlayerProfile()
        {
            
        }

        public object Clone()
        {
            return new PlayerProfile
            {
                Name = Name
            };
        }
    }
}