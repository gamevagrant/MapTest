using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class VisitFilter : IVisitFilter 
{

	private List<string> list;


	public VisitFilter()
	{
		list = new List<string>();
	}

	public void Visit(string id)
	{
		if(list.Contains(id))
		{
			list.Remove(id);
		}
		list.Add(id);
	}

	public string GetLastOne()
	{
		string id = "";
		if(list.Count>0)
		{
			id = list[0];
			list.RemoveAt(0);
		}
		return id;
	}

	public string Serialize()
	{
		string str = JsonMapper.ToJson(list);
		return str;
	}
	public void Deserialize(string data,List<string> ids)
	{
		list = JsonMapper.ToObject<List<string>>(data);
		List<string> newList = ids;
		List<string> removeList = new List<string>(list);

		foreach(string id in list)
		{
			foreach(string nid in newList)
			{
				if(id == nid)
				{
					removeList.Remove(nid);
					break;
				}
			}
		}

		foreach(string id in removeList)
		{
			list.Remove(id);
		}

		foreach(string id in newList)
		{
			if(!list.Contains(id))
			{
				list.Add(id);
			}
		}
		

	}

}
