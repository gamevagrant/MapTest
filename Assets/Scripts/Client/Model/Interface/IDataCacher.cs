using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataCacher
{
    /// <summary>
    /// 缓存数据
    /// </summary>
    /// <param name="data"></param>
    void SaveData(string name,object data);
    /// <summary>
    /// 从缓存中取得数据
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    object GetData(string name);

}
