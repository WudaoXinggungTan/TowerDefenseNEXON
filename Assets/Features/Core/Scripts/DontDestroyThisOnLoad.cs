using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.Core.Scripts
{
    public class DontDestroyThisOnLoad : MonoBehaviour
    {
        #region Variables

        [HideInInspector] public string objectID;

        #endregion

        #region Private Methods

        private void Awake()
        {
            objectID = name + transform.position.ToString() + transform.eulerAngles.ToString();
        }

        private void Start()
        {
            for (int i = 0; i < FindObjectsByType<DontDestroyThisOnLoad>(FindObjectsInactive.Include, FindObjectsSortMode.None).Length; i++)
            {
                if (FindObjectsByType<DontDestroyThisOnLoad>(FindObjectsInactive.Include, FindObjectsSortMode.None)[i] != this)
                {
                    if (FindObjectsByType<DontDestroyThisOnLoad>(FindObjectsInactive.Include, FindObjectsSortMode.None)[i].objectID == objectID)
                    {
                        Destroy(gameObject);
                    }
                }
            }

            DontDestroyOnLoad(gameObject);
        }

        #endregion
    }
}