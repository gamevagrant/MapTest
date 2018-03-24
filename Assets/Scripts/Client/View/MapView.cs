using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapView : MonoBehaviour {

    public GameObjectPool pool;
    private Dictionary<Vector2Int, GameObject> dic = new Dictionary<Vector2Int, GameObject>();
    private Vector2 lastPos;//上一次视口的位置
    private Vector2 curPos;//当前视口位置
    // Use this for initialization
    private void Awake()
    {
        EventDispatcher.instance.AddEventListener("UpdateMap", OnUpdateMapHandle);//监听地图数据变化
    }

    private void OnDestroy()
    {
        EventDispatcher.instance.RemoveEventListener("UpdateMap", OnUpdateMapHandle);
    }

    private void Update()
    {
        curPos = (transform as RectTransform).anchoredPosition;
        if(curPos!=lastPos)
        {
            //发送视口坐标变化
            EventDispatcher.instance.DispatchEvent(new BaseEvent("Move",new Vector2(-curPos.x, curPos.y) ));
            lastPos = curPos;
        }
    }

    private void OnUpdateMapHandle(BaseEvent evt)
    {
        string log = "";
        Rect displayRect = (Rect)evt.datas[0] ;
        Dictionary<Vector2Int, IMapItem> removeItems = evt.datas[1] as Dictionary<Vector2Int, IMapItem>;
        Dictionary<Vector2Int, IMapItem> addItems = evt.datas[2] as Dictionary<Vector2Int, IMapItem>;

        //移除在视口中不可见的地图块
        foreach(Vector2Int key in removeItems.Keys)
        {
            if(dic.ContainsKey(key))
            {
                dic[key].SetActive(false);
                dic.Remove(key);
                log += key;
            }
        }
        //增加新的可见的地图块
        foreach(Vector2Int key in addItems.Keys)
        {
            MapItemView item = pool.getIdleTarget<MapItemView>();
            item.SetData(addItems[key]);
            if(!dic.ContainsKey(key))
            {
                dic.Add(key, item.gameObject);
            }
        }

        if (log != "")
        {
            Debug.Log(log);
        }
    }


}
