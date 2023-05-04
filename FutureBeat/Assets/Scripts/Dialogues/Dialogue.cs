using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;     //запись и чтение xml файла
using System.IO;

    [XmlRoot("dialogue")]
    public class Dialogue 
    {

        [XmlElement("text")]
        public string text;

        [XmlElement("npcname")]
        public string npcname;

        [XmlElement("npcspritelink")]
        public string npcspritelink; 

        [XmlElement("node")]
        public Node[] nodes;

        public static Dialogue Load(TextAsset _xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Dialogue));
            StringReader reader = new StringReader(_xml.text);
            Dialogue dial = serializer.Deserialize(reader) as Dialogue;
            return dial;
        }
    }

    [System.Serializable]
    public class Node
    {
        [XmlElement("nodeid")]
        public string NodeId;

        [XmlElement("npctext")]
        public string Npctext;

        [XmlElement("npcspritelink")]
        public string Npcspritelink;

        [XmlElement("audiolink")]
        public string Audio;

        [XmlArray("answers")]
        [XmlArrayItem("answer")]
        public Answer[] answers;
    }

    public class Answer
    {
        [XmlAttribute("tonode")]
        public int nextNode;
        [XmlElement("text")]
        public string text;
        [XmlElement("dialend")]
        public string end;

    }
