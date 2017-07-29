﻿using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    public class SpawnProbability
    {
        [XmlAttribute]
        public string Name;

        [XmlAttribute]
        public float Probability;
    }
}