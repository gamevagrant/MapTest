using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSettings {

	public static Vector2 dispaySize = new Vector2(350, 200);//视口大小
    public static Vector2 itemSize = new Vector2(100, 100);//每张图片的大小
	public static Vector2Int preloadRange = new Vector2Int(1,1);//预加载区域
	public static int cacheLimit = 20;//缓存数量上限
}
