using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {

    private Rect displayRange;
    private Vector2 itemSize;
    private Dictionary<Vector2Int, IMapItem> displayItems;//显示列表
    private Dictionary<Vector2Int, IMapItem> addDisplayItems;//新增加元素的列表
    private Dictionary<Vector2Int, IMapItem> removeDisplayItems;//需要移除的元素的列表

    private MapItemFactory factory;
    

    public Map(Rect displayRange,Vector2 itemSize)
    {
        this.displayRange = displayRange;
        this.itemSize = itemSize;
        factory = new MapItemFactory();
        displayItems = new Dictionary<Vector2Int, IMapItem>();
    }

    public void Move(Vector2 target)
    {
        removeDisplayItems = new Dictionary<Vector2Int, IMapItem>(displayItems);
        addDisplayItems = new Dictionary<Vector2Int, IMapItem>();

        displayRange.center = target;
        RectInt numRect = GetNumRect();//换算成在显示区域内图片节点的位置信息

        Vector2Int index = numRect.min;
        string log = "";
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

    
    //得到显示区域内地图索引的范围
    private RectInt GetNumRect()
    {
        RectInt rect = new RectInt();
        rect.min = new Vector2Int(Mathf.FloorToInt(displayRange.min.x/itemSize.x), Mathf.FloorToInt(displayRange.min.y / itemSize.y));
        rect.max = new Vector2Int(Mathf.FloorToInt(displayRange.max.x / itemSize.x), Mathf.FloorToInt(displayRange.max.y / itemSize.y));
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
