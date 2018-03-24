using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
/// <summary>
/// 模拟服务器数据提供
/// </summary>
public class MapServer:MonoBehaviour {

    private static MapServer _instance;
    public static MapServer Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject go = new GameObject("server");
                _instance = go.AddComponent<MapServer>();

            }
            return _instance;
        }
    }

    private List<List<int>> data = new List<List<int>>();
    private int row = 10;
    private int col = 10;
    public MapServer()
    {
        string log = "";
        for(int i =0;i<row;i++)
        {
            List<int> list = new List<int>();
            for(int j =0;j<col;j++)
            {
                int num = j + i;
                list.Add(num);
                log += num.ToString() + " ";
            }
            data.Add(list);
            log += "\n";
        }
        Debug.Log(log);
    }

    /// <summary>
    /// 模拟获取数据
    /// </summary>
    /// <param name="json"></param>
    /// <param name="onGetData"></param>
    /// <returns></returns>
    public IEnumerator GetMapData(string json,Action<string> onGetData)
    {
        List<string> list = JsonMapper.ToObject<List<string>>(json);
        Dictionary<string, int> res = new Dictionary<string, int>();
        for(int i=0;i<list.Count;i++)
        {
            string key = list[i];
            string[] xy = key.Split('_');
            int num = data[int.Parse(xy[1])][int.Parse(xy[0])];
            res.Add(key, num);
        }
        yield return new WaitForSeconds(1);
        onGetData(JsonMapper.ToJson(res));
    }

    public void GetMapItemData(string id, Action<bool,int> onGetData)
    {
        string[] xy = id.Split('_');
        Vector2Int index = new Vector2Int(int.Parse(xy[0]),int.Parse(xy[1]));
        if(index.x>= 0&&index.x<col && index.y>=0&&index.y<row)
        {
            int num = data[index.x][index.y];
            StartCoroutine(SendData(num,onGetData));

        }else
        {
            onGetData(false,-1);
        }
       
    }

    /**
    模拟服务器返回数据0.2秒延迟
     */
    private IEnumerator SendData(int num,Action<bool,int> onGetData)
    {
        yield return null;
        yield return null;
        yield return null;
        onGetData(true,num);

    }
}
