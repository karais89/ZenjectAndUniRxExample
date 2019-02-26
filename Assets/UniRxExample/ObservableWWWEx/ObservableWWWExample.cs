using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Assets.UniRxExample.ObservableWWWEx
{
    public class ObservableWWWExample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            ObservableWWW.Get("http://torisoup.net/index.html")
                .Subscribe(result => Debug.Log(result));
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}