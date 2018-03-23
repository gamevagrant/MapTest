using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItemFactory {

	public IMapItem GetMapItem(Vector2Int index,Vector2 size)
    {
        IMapItem item = new MapItem();
        item.SetCacherAndLoader(MapItemCache.Instance, MapItemLoader.Instance);
        item.SetData(index, size);
        return item;
    }
}
