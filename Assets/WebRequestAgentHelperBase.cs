﻿//------------------------------------------------------------
// Game Framework v3.x
// Copyright © 2013-2018 Jiang Yin. All rights reserved.
// Homepage: http://gameframework.cn/
// Feedback: mailto:jiangyin@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Web 请求代理辅助器基类。
/// </summary>
public abstract class WebRequestAgentHelperBase : MonoBehaviour
{
    /// <summary>
    /// Web 请求代理辅助器完成事件。
    /// </summary>
    public abstract event Action<object, byte[]> WebRequestAgentHelperComplete;

    /// <summary>
    /// Web 请求代理辅助器错误事件。
    /// </summary>
    public abstract event Action<object, string> WebRequestAgentHelperError;

    /// <summary>
    /// 通过 Web 请求代理辅助器发送 Web 请求。
    /// </summary>
    /// <param name="webRequestUri">Web 请求地址。</param>
    /// <param name="userData">用户自定义数据。</param>
    public abstract UnityWebRequest Request(string webRequestUri, SortedDictionary<string, string> userData,HttpType httpType  );

    /// <summary>
    /// 通过 Web 请求代理辅助器发送 Web 请求。
    /// </summary>
    /// <param name="webRequestUri">Web 请求地址。</param>
    /// <param name="postData">要发送的数据流。</param>
    /// <param name="userData">用户自定义数据。</param>
    public abstract void Request(string webRequestUri, byte[] postData, object userData);

    /// <summary>
    /// 重置 Web 请求代理辅助器。
    /// </summary>
    public abstract void Reset();
}
public enum HttpType
{
    Get,
    Post,

}