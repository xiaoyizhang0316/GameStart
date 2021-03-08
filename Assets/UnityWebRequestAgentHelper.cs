//------------------------------------------------------------
// Game Framework v3.x
// Copyright © 2013-2018 Jiang Yin. All rights reserved.
// Homepage: http://gameframework.cn/
// Feedback: mailto:jiangyin@gameframework.cn
//------------------------------------------------------------

using System;
using UnityEngine; 
using System.Collections.Generic;
using System.Text;
 
  
#if UNITY_5_4_OR_NEWER
using UnityEngine.Networking;
#else
using UnityEngine.Experimental.Networking;
#endif 
using UnityEngine.EventSystems;


/// <summary>
/// 使用 UnityWebRequest 实现的 Web 请求代理辅助器。
/// </summary>
public class UnityWebRequestAgentHelper : WebRequestAgentHelperBase, IDisposable
{
    private UnityWebRequest m_UnityWebRequest = null;

    private bool m_Disposed = false;

    private Action<object, byte[]> m_WebRequestAgentHelperCompleteEventHandler = null;
    private Action<object, string> m_WebRequestAgentHelperErrorEventHandler = null;

    /// <summary>
    /// Web 请求代理辅助器完成事件。
    /// </summary>
    public override event Action<object, byte[]> WebRequestAgentHelperComplete
    {
        add
        {
            m_WebRequestAgentHelperCompleteEventHandler += value;
        }
        remove
        {
            m_WebRequestAgentHelperCompleteEventHandler -= value;
        }
    }

    /// <summary>
    /// Web 请求代理辅助器错误事件。
    /// </summary>
    public override event Action<object, string> WebRequestAgentHelperError
    {
        add
        {
            m_WebRequestAgentHelperErrorEventHandler += value;
        }
        remove
        {
            m_WebRequestAgentHelperErrorEventHandler -= value;
        }
    }

    /// <summary>
    /// 通过 Web 请求代理辅助器发送请求。
    /// </summary>
    /// <param name="webRequestUrl">要发送的远程地址。</param>
    /// <param name="userData">用户自定义数据。</param>
    public override UnityWebRequest Request(string webRequestUrl, SortedDictionary<string, string> userData = null , HttpType httpType = HttpType.Get )
    {
        
        if (httpType == HttpType.Get )
        {
            if (userData == null)
            {
                m_UnityWebRequest = UnityWebRequest.Get(webRequestUrl);
            }
            else
            {
                m_UnityWebRequest = UnityWebRequest.Get(CreateFullURL(webRequestUrl, userData));
            }
        }
        else
        {
         m_UnityWebRequest = UnityWebRequest.Post(webRequestUrl,CreateWWWForm(userData));
          //  m_UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

        }
        m_UnityWebRequest.timeout = 15;

#if UNITY_2017_2_OR_NEWER
        m_UnityWebRequest.SendWebRequest();
#else
            m_UnityWebRequest.Send();
#endif
        return m_UnityWebRequest;
    }

    /// <summary>
    /// 通过 Web 请求代理辅助器发送请求。
    /// </summary>
    /// <param name="webRequestUri">要发送的远程地址。</param>
    /// <param name="postData">要发送的数据流。</param>
    /// <param name="userData">用户自定义数据。</param>
    public override void Request(string webRequestUri, byte[] postData, object userData)
    {
        if (m_WebRequestAgentHelperCompleteEventHandler == null || m_WebRequestAgentHelperErrorEventHandler == null)
        {
            Debug.LogError("Web request agent helper handler is invalid.");
            return;
        }

        m_UnityWebRequest = UnityWebRequest.Post(webRequestUri, Utilities.Converter.GetString(postData));
#if UNITY_2017_2_OR_NEWER
        m_UnityWebRequest.SendWebRequest();
#else
            m_UnityWebRequest.Send();
#endif
    }

    /// <summary>
    /// 重置 Web 请求代理辅助器。
    /// </summary>
    public override void Reset()
    {
        if (m_UnityWebRequest != null)
        {
            m_UnityWebRequest.Dispose();
            m_UnityWebRequest = null;
        }
    }

    /// <summary>
    /// 释放资源。
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// 释放资源。
    /// </summary>
    /// <param name="disposing">释放资源标记。</param>
    private void Dispose(bool disposing)
    {
        if (m_Disposed)
        {
            return;
        }

        if (disposing)
        {
            if (m_UnityWebRequest != null)
            {
                m_UnityWebRequest.Dispose();
                m_UnityWebRequest = null;
            }
        }

        m_Disposed = true;
    }
   
    private void Update()
    {
 
    }
    /// <summary>
    /// 创建完整Get URL地址 
    /// </summary>
    /// <param name="URL"></param>
    /// <param name="keyAndValues"></param>
    /// <returns></returns>
   public  string CreateFullURL(string URL, IDictionary<string, string> keyAndValues)
    {
        StringBuilder keyAndValue = new StringBuilder();
        if (keyAndValues != null)
        {
            foreach (var item in keyAndValues)
            {
                if (keyAndValue == null)
                {
                    keyAndValue.Append(item.Key);
                    keyAndValue.Append("=");
                    keyAndValue.Append(item.Value);
                }
                else
                {
                    keyAndValue.Append("&");
                    keyAndValue.Append(item.Key);
                    keyAndValue.Append("=");
                    keyAndValue.Append(item.Value);
                }
            }
        }
        if (keyAndValue != null)
        {
            URL += "?" + keyAndValue;
        }
        return URL;
    }
    /// <summary>
    /// 创建WWWForm表单
    /// </summary>
    /// <param name="keyAndValues"></param>
    /// <param name="sendBytesList"></param>
    /// <returns></returns>
    protected WWWForm CreateWWWForm(IDictionary<string, string> keyAndValues )
    {
        WWWForm form = new WWWForm();
        if (keyAndValues != null)
        {
            foreach (var item in keyAndValues)
            {
                form.AddField(item.Key, item.Value);
            }
        }
      
        return form;
    }
 
}
