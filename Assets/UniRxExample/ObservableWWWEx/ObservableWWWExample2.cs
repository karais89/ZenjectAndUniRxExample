using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UniRxExample.ObservableWWWEx
{
    public class ObservableWWWExample2 : MonoBehaviour
    {
        public Button button;
        public Image image;
        private readonly string resourceURL = "http://torisoup.net/unirx-examples/resources/sampletexture.png";

        // Start is called before the first frame update
        void Start()
        {
            button.OnClickAsObservable()
                .First()
                .SelectMany(ObservableWWW.GetWWW(resourceURL))
                .Select(www => Sprite.Create(www.texture, new Rect(0, 0, 400, 400), Vector2.zero))
                .Subscribe(sprite =>
                {
                    image.sprite = sprite;
                    button.interactable = false;
                }, Debug.LogError);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}