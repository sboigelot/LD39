using System.Collections.Generic;
using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    public class AnimationPrototype
    {
        [XmlAttribute]
        public string Name;

        [XmlElement("Frame")]
        public List<string> Frames;

        [XmlAttribute]
        public float FrameDelay;
    }
}