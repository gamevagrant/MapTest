using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//地图节点
public class MapItem :IMapItem
{
    private const int UPDATE_INTERVAL = 200;//数据更新间隔200s
    private IDataCacher cacher;
    private IDataLoader loader;

    
   

    private MapItemData _data;
    public MapItemData Data
    {
        get
        {
            return _data;
        }
    }

    private Vector2 _size;
    public Vector2 Size
    {
        get
        {
            return _size;
        }
    }

    private Vector2Int _index;
    public Vector2 Index
    {
        get
        {
            return _index;
        }
    }

    public void SetData(Vector2Int index, Vector2 size)
    {
        this._index = index;
        this._size = size;
    }

    void IMapItem.Load()
    {
        string name = _index.x.ToString() + "_" + _index.y.ToString();
        object obj = cacher.GetData(name);
        if(obj!=null)
        {
            MapItemData itemData = obj as MapItemData;
            if(GameUtils.Timestamps - itemData.updateTimeStamp<UPDATE_INTERVAL)
            {
                _data = itemData;
                return;
            }
        }

        loader.Load(name, (ret, res) => {

            if(ret)
            {
                _data = res as MapItemData;
                _data.isNull = false;
                cacher.SaveData(name, _data);
            }else
            {
                _data = new MapItemData();
                _data.x = _index.x;
                _data.y = _index.y;
                _data.isNull = true;
            }
        });

    }

    void IMapItem.SetCacherAndLoader(IDataCacher cacher, IDataLoader loader)
    {
        this.cacher = cacher;
        this.loader = loader;
    }

    
}
