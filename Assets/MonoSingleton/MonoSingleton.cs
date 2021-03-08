using UnityEngine;
using System.Collections;

namespace IOIntensiveFramework.MonoSingleton
{
	public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        public static string typeName;
        private static T my = null;
		public static T My {
			get {
				if (my == null)
				{
					my = FindObjectOfType(typeof(T)) as T;
					if (my == null)
					{
						my = new GameObject("_" + typeof(T).Name).AddComponent<T>();
						//DontDestroyOnLoad(My);
					}
					//if (My == null)
					//	Console.LogError("Failed to create My of " + typeof(T).FullName + ".");
                    typeName = my.GetType().ToString();
                }
				return my;
			}
		}

        void OnApplicationQuit() { if (my != null) my = null; }

        public static T CreateInstance()
        {
            if (My != null) My.OnCreate();
            return My;
        }

        protected virtual void OnCreate()
        {

        }
    }
}