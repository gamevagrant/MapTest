using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using LitJson;
//地图缓存器
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

    private static string root = Application.persistentDataPath+"/MapCache";//地图数据存储根目录
    private static string filterFile = Application.persistentDataPath + "/filter";//访问帅选器数据存储位置
    private IVisitFilter visitFilter;
    private DirectoryInfo directoryInfo;
    private int cacheLimit;//本地地图节点存储限制

    private void Awake()
    {
        Debug.Log("缓存文件存储在" + root);
        

        cacheLimit = MapSettings.cacheLimit;
        directoryInfo = new DirectoryInfo(root);
        initFilter();
        
    }

    //初始化 访问次数筛选器
    void initFilter()
    {
        string path = filterFile;
        visitFilter = new VisitFilter();
        FileInfo fi = new FileInfo(path);

        FileInfo[] files = directoryInfo.GetFiles();
        List<string> names = new List<string>();
        foreach(FileInfo file in files)
        {
            names.Add(file.Name);
        }
        if(fi.Exists)
        {
            string str = ReadData(path);

           
            visitFilter.Deserialize(str,names);
        }else
        {
            visitFilter.Deserialize("[]",names);
        }
    }

    object IDataCacher.GetData(string name)
    {
        Debug.Log("从本地缓存加载："+name);
        string path = GetPath(name);
        string json = ReadData(path);
        if(string.IsNullOrEmpty(json))
        {
            return null;
        }
        visitFilter.Visit(name);
        return JsonMapper.ToObject<MapItemData>(json);
    }

    void IDataCacher.SaveData(string name,object data)
    {
        Debug.Log("缓存：" + name);
        string path = GetPath(name);
        FileInfo[] files = directoryInfo.GetFiles();
        if(files.Length>=cacheLimit)
        {
            string id = visitFilter.GetLastOne();
            FileInfo fi = new FileInfo(GetPath(id));
            Debug.Log(string.Format("地图数据超过上限[{0}]移除[{1}]",cacheLimit,fi.Name));
            if(fi.Exists)
            {
                
                fi.Delete();
            }
        }
       
       visitFilter.Visit(name);
        string json = JsonMapper.ToJson(data);
        WriteData(path,json);

        WriteData(filterFile,visitFilter.Serialize());

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

        FileStream fs = new FileStream(path,FileMode.Create);

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
