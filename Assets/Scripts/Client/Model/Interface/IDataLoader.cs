using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface IDataLoader {


    void Load(string url, Action<bool,object> onComplate);
}
