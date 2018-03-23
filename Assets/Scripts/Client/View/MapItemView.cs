using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapItemView : MonoBehaviour {

    public Text text;
    public IMapItem item;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(item!=null && item.Data!=null && item.Data.data.ToString()!=text.text)
        {
            text.text = item.Data.data.ToString();
            text.text = item.Data.Id;
        }
	}

    public void SetData(IMapItem item)
    {
        this.item = item;
        item.Load();
        Vector2 index = item.Index;
        Vector2 size = item.Size;

        (transform as RectTransform).sizeDelta = size;
        (transform as RectTransform).anchoredPosition = new Vector2(index.x * size.x + size.x/2,-index.y * size.y - size.y/2);

    }
}
