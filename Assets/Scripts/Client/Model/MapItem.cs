using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            _data = obj as MapItemData;
            if(GameUtils.Timestamps - _data.updateTimeStamp<UPDATE_INTERVAL)
            {
                return;
            }
        }

        loader.Load(name, (ret, res) => {

            if(ret)
            {
                _data = res as MapItemData;
                cacher.SaveData(name, _data);
            }
        });

    }

    void IMapItem.SetCacherAndLoader(IDataCacher cacher, IDataLoader loader)
    {
        this.cacher = cacher;
        this.loader = loader;
    }

    
}
