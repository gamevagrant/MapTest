using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    private Vector2 dispaySize = new Vector2(350, 200);//视口大小
    private Vector2 itemSize = new Vector2(100, 100);//每张图片的大小
    private Map map;

    private void Awake()
    {
        EventDispatcher.instance.AddEventListener("Move", OnMoveHandle);
    }

    private void OnDestroy()
    {
        EventDispatcher.instance.RemoveEventListener("Move", OnMoveHandle);
    }
    // Use this for initialization
    void Start () {
        StartCoroutine(MapServer.Instance.GetMapData("['2_3','3_4','6_4']", (string json) =>
        {
            Debug.Log(json);
        }));



        map = new Map(new Rect(Vector2.zero, dispaySize), itemSize);

    }
	
    private void OnMoveHandle(BaseEvent evt)
    {
        Vector2 pos = (Vector2)evt.datas[0];
        map.Move(pos);
    }
}
