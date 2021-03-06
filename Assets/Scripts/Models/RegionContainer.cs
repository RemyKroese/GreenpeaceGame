﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[Serializable]
public class RegionContainer
{
    [XmlArray("Regions"), XmlArrayItem("Region")]
    public List<MapRegion> regions { get; private set; }

    public RegionContainer() { }

    public RegionContainer(List<MapRegion> regions)
    {
        this.regions = regions;
    }

    public void Save()
    {
        try
        {
            XmlSerializer writer = new XmlSerializer(typeof(RegionContainer));
            Debug.Log("Serializing Regions");
            var path = Application.dataPath + "/Resources/Regions.xml";
            FileStream file = File.Create(path);
            writer.Serialize(file, this);
            file.Close();
            Debug.Log("Serialization finished");
        }

        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public static RegionContainer Load()
    {
        //resources reading for build
        TextAsset textAsset = (TextAsset)Resources.Load("Regions");
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(textAsset.text);
        XmlSerializer serializer = new XmlSerializer(typeof(RegionContainer));
        StringReader reader = new StringReader(xml.OuterXml);
        return serializer.Deserialize(reader) as RegionContainer;
    }
}

