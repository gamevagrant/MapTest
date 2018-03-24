using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {

    public Vector2Int preloadRange;//预加载区域
    private Rect displayRange;//显示区域
    private Vector2 itemSize;//地图块的大小
    private Dictionary<Vector2Int, IMapItem> displayItems;//显示列表
    private Dictionary<Vector2Int, IMapItem> addDisplayItems;//新增加元素的列表
    private Dictionary<Vector2Int, IMapItem> removeDisplayItems;//需要移除的元素的列表

    private MapItemFactory factory;
    

    public Map()
    {
        this.preloadRange = MapSettings.preloadRange;
        this.displayRange = new Rect(Vector2.zero,MapSettings.dispaySize) ;
        this.itemSize = MapSettings.itemSize;
        factory = new MapItemFactory();
        displayItems = new Dictionary<Vector2Int, IMapItem>();
    }

//移动视口位置
    public void Move(Vector2 target)
    {
        removeDisplayItems = new Dictionary<Vector2Int, IMapItem>(displayItems);
        addDisplayItems = new Dictionary<Vector2Int, IMapItem>();

        displayRange.center = target;
        RectInt numRect = GetNumRect();//换算成在显示区域内图片节点的位置信息

        Vector2Int index = numRect.min;
        string log = "";
        //筛选出当前帧和上一帧对比不需要显示和新增需要显示的地图块
        while(index.x<=numRect.max.x && index.y<=numRect.max.y)
        {
            AddDisplayList(index);
            log += index.ToString();
            index = new Vector2Int(index.x+1, index.y);
            if(index.x>numRect.max.x)
            {
                log += "\n";
                index = new Vector2Int(numRect.min.x, index.y+1);
            }
        }
        UpdateDisplayItems();
        //Debug.Log(log);

    }

    
    //获取当前需要显示的地图块的范围
    private RectInt GetNumRect()
    {
        RectInt rect = new RectInt();
        rect.min = new Vector2Int(Mathf.FloorToInt(displayRange.min.x/itemSize.x), Mathf.FloorToInt(displayRange.min.y / itemSize.y)) - preloadRange;
        rect.max = new Vector2Int(Mathf.FloorToInt(displayRange.max.x / itemSize.x), Mathf.FloorToInt(displayRange.max.y / itemSize.y)) + preloadRange;
        return rect;
    }

    
    private void AddDisplayList(Vector2Int index)
    {
        if(!displayItems.ContainsKey(index))
        {
            IMapItem item = factory.GetMapItem(index,itemSize);
            addDisplayItems.Add(index, item);
            displayItems.Add(index, item);
        }else
        {
            removeDisplayItems.Remove(index);
        }
        
    }

    //更新显示列表
    private void UpdateDisplayItems()
    {
        
        foreach(Vector2Int key in removeDisplayItems.Keys)
        {
            if(displayItems.ContainsKey(key))
            {
                displayItems.Remove(key);
            }
           
        }
       
        
        EventDispatcher.instance.DispatchEvent(new BaseEvent("UpdateMap", displayRange, removeDisplayItems,addDisplayItems));
       
    }
}
