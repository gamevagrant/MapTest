using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMapItem {

    MapItemData Data { get; }
    
    Vector2 Size { get; }

    Vector2 Index { get; }

    /// <summary>
    /// 设置缓存器和加载器
    /// </summary>
    /// <param name="cacher"></param>
    /// <param name="loader"></param>
    void SetCacherAndLoader(IDataCacher cacher,IDataLoader loader);

    /// <summary>
    /// 加载数据
    /// </summary>
    /// <param name="index"></param>
    void Load();
    /// <summary>
    /// 设置数据
    /// </summary>
    /// <param name="index"></param>
    /// <param name="size"></param>
    void SetData(Vector2Int index, Vector2 size);
}
