using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPresenter : MonoBehaviour
{
    // view (ui)
    public Button button;
    public Text counterText;

    // model
    private ButtonModel buttonModel;
    
    // Start is called before the first frame update
    void Start()
    {
        // 모델 새로 작성
        this.buttonModel = new ButtonModel();
     
        // 누를때마다 카운터 하는 처리 등록
        this.button.OnClickAsObservable() // 버튼을 누를때
            .Subscribe(_ => buttonModel.PushCount()); // 카운터 증가를 등록
        
        // 카운터를 텍스트에 반영
        buttonModel.PushCounter // 카운터가 변경 되었을때
            .Subscribe(countNum => this.counterText.text = countNum.ToString()); // 텍스트 ui 변경
    }
}
