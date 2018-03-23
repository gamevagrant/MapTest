using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItemData  {

    public int data;//数据
    public int visits;//浏览次数
    public int updateTimeStamp;//数据更新时的时间戳
    public int x;
    public int y;
    public string Id
    {
        get
        {
            return x.ToString() + "_" + y.ToString();
        }
    }
}
