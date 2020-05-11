/*
 * @Author: l hy 
 * @Date: 2020-05-11 16:09:01 
 * @Description: 编辑器工具类
 */
 
using System;
using UnityEditor;

public class EditorUtil {

    /// <summary>
    /// 清空控制台信息
    /// </summary>
    public static void clearConsole () {
        Type log = typeof (EditorWindow).Assembly.GetType ("UnityEditor.LogEntries");

        var clearMethod = log.GetMethod ("Clear");
        clearMethod.Invoke (null, null);
    }
}