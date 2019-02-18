using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ButtonModel
{
    public ReactiveProperty<int> PushCounter { get; private set; }

    public ButtonModel()
    {
        //PushCounter = new ReactiveProperty<int>(); // 초기화
        //PushCounter.Value = 0;
        PushCounter = new ReactiveProperty<int>(0); // 초기화
    }

    // 카운터 처리
    public void PushCount()
    {
        this.PushCounter.Value++; // 여기서 이벤트가 통지된다
    }
}
