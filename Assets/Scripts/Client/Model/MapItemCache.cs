using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using LitJson;

public class MapItemCache :MonoBehaviour, IDataCacher
{
    private static MapItemCache _instance;
    public static MapItemCache Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject go = new GameObject("Cacher");
                _instance = go.AddComponent<MapItemCache>();
            }
            return _instance;
        }
    }

    private static string root = Application.persistentDataPath+"/MapCache";

    private void Start()
    {
        Debug.Log("缓存文件存储在" + root);
    }
    object IDataCacher.GetData(string name)
    {
        string path = GetPath(name);
        string json = ReadData(path);
        if(string.IsNullOrEmpty(json))
        {
            return null;
        }

        return JsonMapper.ToObject<MapItemData>(json);
    }

    void IDataCacher.SaveData(string name,object data)
    {
        Debug.Log("write:" + name);
        string path = GetPath(name);
        string json = JsonMapper.ToJson(data);
        WriteData(path,json);
    }

    private string GetPath(string name)
    {
        return root + "/" + name;

    }

    private void WriteData(string path,string data)
    {

        FileInfo fi = new FileInfo(path);
        DirectoryInfo dir = fi.Directory;
        if (!dir.Exists)
        {
            dir.Create();
        }

        FileStream fs = new FileStream(path,FileMode.OpenOrCreate);

        StreamWriter sw = new StreamWriter(fs);

        sw.Write(data);
        sw.Close();
        fs.Close();
    }

    private string ReadData(string path)
    {
        FileInfo fi = new FileInfo(path);
        if(!fi.Exists)
        {
            return "";
        }
        FileStream fs = new FileStream(path, FileMode.Open);

        StreamReader sr = new StreamReader(fs,System.Text.Encoding.UTF8);

        string str = sr.ReadToEnd();
        sr.Close();
        fs.Close();

        return str;
    }
}
