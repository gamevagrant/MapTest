using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//访问筛选器接口
public interface IVisitFilter {

	//访问
	void Visit(string id);

	//获取一个最不常访问的项
	string GetLastOne();

	//序列化
	string Serialize();
	//反序列化
	void Deserialize(string data,List<string> ids);

}
