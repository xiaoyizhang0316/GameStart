using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UpdateVersion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
        Debug.Log("保存");
        BuildJson buildJson = new BuildJson();
        buildJson.versionsIndex = Application.version;
        string accoutjson = JsonUtility.ToJson(buildJson);
         
        string encode =accoutjson;
        FileStream file = new FileStream( Directory.GetParent(Directory.GetParent(Application.dataPath)+"") 
                                         + "\\Build.json", FileMode.Create);
        byte[] bts = System.Text.Encoding.UTF8.GetBytes(encode);
        file.Write(bts, 0, bts.Length);
        if (file != null)
        {
            file.Close();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
