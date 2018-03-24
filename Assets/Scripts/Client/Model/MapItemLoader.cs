using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//地图数据加载器
public class MapItemLoader :MonoBehaviour, IDataLoader
{
    private static MapItemLoader _instance;
    public static MapItemLoader Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject go = new GameObject("Loader");
                _instance = go.AddComponent<MapItemLoader>();
            }
            return _instance;
        }
    }

    private Queue<Action> queue = new Queue<Action>();
    private bool isLoading = false;

    public void Update()
    {
        if (queue.Count > 0 && !isLoading)
        {
            Action act = queue.Dequeue();
            if (act != null)
            {
                act();
            }
        }
    }

    void IDataLoader.Load(string url, Action<bool, object> onComplate)
    {
        queue.Enqueue(() => 
        {
            isLoading = true;
            MapServer.Instance.GetMapItemData(url, (ret,res) =>
            {
                Debug.Log(string.Format("从服务器加载[{0}]数据",url));
                if(ret)
                {
                    string[] xy = url.Split('_');
                    MapItemData data = new MapItemData
                    {
                        data = res,
                        updateTimeStamp = GameUtils.Timestamps,
                        x = int.Parse(xy[0]),
                        y = int.Parse(xy[1]),


                    };

                    onComplate(true, data);
                    
                }else
                {
                    onComplate(false, null);
                }
               
                isLoading = false;
            });



        });
    }
}
