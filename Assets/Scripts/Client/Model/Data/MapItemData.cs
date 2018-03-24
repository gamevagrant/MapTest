using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//地图节点数据
public class MapItemData  {

    public int data;//数据
    public int visits;//浏览次数
    public int updateTimeStamp;//数据更新时的时间戳
    public int x;//x坐标
    public int y;//y坐标
    public bool isNull;//是否是空数据
    public string Id
    {
        get
        {
            return x.ToString() + "_" + y.ToString();
        }
    }
}
