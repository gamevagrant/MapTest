using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameUtils {

    public static int Timestamps
    {
        get
        {
            return DateTimeToTimestamp(DateTime.Now);
        }
    }

    /// <summary>  
    /// 将c# DateTime时间格式转换为Unix时间戳格式  
    /// </summary>  
    /// <param name="time">时间</param>  
    /// <returns>long</returns>  
    public static int DateTimeToTimestamp(System.DateTime time)
    {
        System.DateTime startTime = System.TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
        int t = (int)(time - startTime).TotalSeconds;   //除10000调整为13位      
        return t;
    }
}
