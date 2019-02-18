# MV(R)P

## http://www.gamecodi.com/board/zboard-id-GAMECODI_Talkdev-no-3991-z-1.htm

UniRx사용한지는 한 1년이 넘어가는 사용자입니다.

UniRx에서 권장하는 MVRP를 사용하는것 기준으로는

UI 표현을 위한 로직이 Presenter에,
기획 데이터의 초기화 결과물중, Subscribe할 데이터들을 ReactiveProperty＜T＞로 두고 사용합니다.
Dictionary → ReactiveDictionary＜T＞
List → ReactiveCollection＜T＞
를 사용합니다.

ReactiveCollection은 List와는 조금 틀려서 일부 기능이 없을 수 있는데, 이는 LINQ로 보완해서 사용합니다.
최근 IL2CPP로 오면서 이 부분에 있어서는 많은 문제가 해결은 되었지만(더불어 다른 문제가 생기고 있는점은... 음;),
Linq사용이 불안하시면, UniLinq라는 라이브러리를 검색해봐주시면 도움이 되실것 같습니다.
위 UniLinq는 NUnit을 통한 기기에서의 Unittest가 가능하게끔 테스트된 코드가 300 case넘게 설정이 되어있습니다.
현재까지는 Linq로 인하여 중국쪽 단말들이나, iOS에서의 운용에서도 문제된 부분이 없습니다.

우선... 가령 gold값을 운용한다고 하면,
ReactiveProperty＜int＞ UserData.Instance.Gold; 를 두고,

Presenter에 UnityEngine.UI.Text goldView;를 둔다고 한다면,
Gold.Subscribe(gold=>goldView.text=gold); 혹은 Gold.SubscribeToText(goldView); 를 초기화하는 메소드에 두어서
Network 통신 관련 handler등에서 UserData.Instance.Gold.Value = ?; 갱신을 수행하였을때 View에 반영되는 형태로 두게합니다.

실 프로젝트에서는 이보다는 복잡한 내용들이 있긴하지만... 간단하게 사례를 올린다면 위 정도 패턴일것 같습니다.

MVRP패턴을 따르게 되면 구현 전반적으로 원패턴의 구현이 안정적으로 이루어질 수 있게됩니다.
다른 작업자의 작업물에 있어서도 해당 프레임워크에서의 제약사항을 고수하게 두면, 
개인간에 다소 스타일차가 벌어지는 부분이 있더라도 작업물 이관시에 큰 무리 없이 넘겨받을 수 있게됩니다.

uFrame이라는 UniRx를 사용하는 MVVM프레임워크가 있는데, 처음 여기서 Rx를 접했다가 현재는 사용하지 않고 있습니다.
(UniRx페이지에서 나온대로 아무래도 너무 복잡해지는 감이 있었습니다)
최근에 에디터를 포함해 완전히 소스 공개가 되었다고 (이전에는 일부가 dll) 합니다.

1년정도 써보고 느끼는건 아무래도 내부가 event driven이다보니, promise pattern을 통한 구현이라,
callback hell을 회피하면서도 코드로직으로 인한 퍼포먼스 저하는 상당부분 덜어내고 있다는 생각을 하고 있습니다.

함수형 프로그래밍이 가능해지긴 하는데, 이 부분은 어디까지나 stream운용에 국한해서 사용하고 있고,
Coroutine이 적합한 상황에서는 Coroutine을 사용합니다.

IEnumerator F(); 에 대해 F().ToObservable().Subscribe()하는 형태로 coroutine내부에서 throw를 하더라도 subscribe의 2인수로 exception을 받아 처리할 수 있습니다. 혹은 Observable.FromCoroutine＜T＞((ob,canceller)＝＞...); 로도 Coroutine을 오갈 수 있으나, 이 경우 Coroutine내에서 Observer에 대하여 OnNext, OnError, OnComplete의 주기를 신경써주셔야합니다.

UniRx는 5버전대에 들어가면서 iOS의 AOT support(Mono2x지원)을 종료했습니다.
(다만 5버전대로 들어가면 rx의 어마어마한 callstack이 상당부분 경감됩니다.)
이 부분 참고하시는게 좋을듯하고, 
경험상 UniRx의 Rx도 그렇지만, 다른 진영의 Rx (가령 RxSwift, RxJs, RxJava)의 코드를 참고하시는게 도움이 많이 되시지 않나 싶습니다.

구체적인 적용 사례에 대해서 고민하고 계시다면, 해당 내용을 알려주시면 같이 고민해드리겠습니다.

### Q&A

#### Q.1

MVP패턴 기준으로 Presenter는 로직을 담당하고 View 와 Model을 연결 시켜주기 위해서라면 View(UILabel, Animation 등)를 참조하며 Update 나 Corutine 사용하기 위해 Monobehaviour를 상속받고 Gameobject에 컴포넌트로 박혀 사용이 되나요?

#### A.1

View는 MonoBehaviour로 구현하고 있습니다. Presenter는 MonoBehaviour가 아닌 형태로 구현할 수도 있겠지만, MonoBehaviour로 구현합니다. View는 SerializableField에 참조하는 형태로 구현합니다. 단 독자적인 Metaprogramming기법을 통해 Component reference상단에 코드로 각 객체의 경로를 모두 코드상에 서술하고있으나, Build전에 MonoBehaviour의 SerializableField을 통해 Serialize되는 형태로 구현합니다. (이편이 Deserialization중 가장 빠릅니다)

#### Q.2
 
그렇다면 Model 은 Monobehaviour 를 상속받지 않고 C# 기본 클래스로 생성해서 데이터만 정의하게 되나요?(로직 구현이 없을테니...최대한 경량화..?) 

#### A.2

Model은 MonoBehaviour로 구현하면 안됩니다. 10만개의 MonoBehaviour를 생성하면 유니티는 약 100MB의 메모리를 사용합니다. 유니티가 버티질 못합니다.

#### Q.3

Presenter가 View와 Model을 둘다 멤버나 참조로 가지고 있는 모습은 패턴에 어긋나나요? (Presenter 의 멤버이다 보니 한번에 관리가 되어 편해 보이나.. Gameobject에 컴포넌트 놓고 구성 변수들만 클래스화(Model) 되어잇지 다른 차이점이 없는것 같아 맞는 방법인지 의문이네요.)

#### A.3

Presenter는 1에서 서술했듯, "참조" 형태로 View를 연결하고 있으며, 통상 Model은 초기화 코드에서 코드로 참조합니다. 물론 구현형태에 따라 Model을 "참조" 할수도 있겠으나, 저는 그렇게 구현하진 않습니다.

#### Q.4

Model(데이터 클래스)은 싱글턴이나 특정 위치에(게임매니저 등) 위치하여 외부 입력이나 작용으로 의해 데이터가 변하게 된다면 변경된 Model이 해당 Presenter로 변경이 되엇다 알려주는 것이 아닌, 변경된 데이터를 Presenter는 구독(UniRX일 경우, 아니라면 Delegate..?)을 하여 변경된 정보에 따른 내용 반영을 View에다가 해주는것인가요?

#### A.4

Model은 정확하게는 데이터 클래스를 말하는게 아니라 각 데이터 그 자체를 말합니다(int gold;수준의) 배치는 프로젝트의 스타일과 여타 상황에 따라 달라질 수 있겠지만, 통상은 그렇습니다. 보통 데이터의 변경을 알려주고.. 라는 부분은 Rx가 없는 상황 하에서라면, event를 구현하여 변수마다.. 만드는 것이 맞겠습니다. 다만 Rx를 사용하면 이런 복잡한 절차가 ReactiveProperty로 끝납니다 (간편해지는 장점이 있습니다). 다만 Presenter에서 이를 Subscribe하고 있는데, 이 ReactiveProperty가 통채로 바뀌게되면 당연히 구독이 안되게되니 이런데서 주의가 필요합니다.

#### Q.5

Presenter와 Model을 상호간 참조하며 작용한다던가.. 뭐 이런 방법이 옳지 않다면 기능이 추가될때마다 Delegate를 사용해야 하는 건가요..?(아직 시도해보기 전이라 왠지 비효율적이라는 생각이 언뜻 드네요..)

#### A.5

말씀하시는대로 Presenter에서 가령 ModelManager같은 클래스가 있다고 하면, ModelManager에서는 event(delegate)를 제공하고, 데이터 그 자체를 들고있으면서 Presenter는 이에 대해 이벤트 구독을 수행하여, 데이터의 변경을 감지하면 View에 feedback하는 형태로 구현합니다. (패턴이론상 이렇긴 하지만... 정말 이걸 하실거라면 ReactiveProperty를 사용한후 이를 subscribe하는 형태를 추천드립니다. 데이터가 아닌 어떤 함수화 형태라면 IObservable＜T＞을 반환하는 메소드형태도 있습니다.) 

만약 model manager에서 view로 직접 참조하는 구조를 가지게되면 model manager는 해당 view에 대해 종속적이게 되고 더이상 MVP라 부를 수 없는 구조가 됩니다. 간단하게 말하면 View가 프로젝트에서 삭제되더라도 ModelManager는 아무렇지도 않게 컴파일 되어야 패턴이 준수된것입니다. 더불어 View와 Model의 작업 책임자는 다른경우가 많은데, 인터페이스 없이 직접 발송해주는 형태를 가진 경우, View의 책임자는 Model의 책임자에게 대하여 약간의 구현을 위해 작업을 의뢰할 수 밖에 없는 형태가 됩니다 (이 시점에서 이미 좋은 구현패턴이 아닙니다)

물론 이런 패턴들을 준수하며 작성한다는것은 조금씩의 손이 더 가게 되어있습니다. Singleton하나를 작성하더라도 (shared)Instance를 작성하기 위한 약간의 번거로움이 존재하듯이요. 다만 이런 구현형태에는 나름대로의 위와같은 이유가 있다는점을 이해하신다면 와닿는 부분이 있지 않을까 생각합니다.

#### Q.6

음.. 코루틴을 사용할려고 보니..

Model에다가 시간에 따른 변화를 모두 이벤트로 구현 한다음.. Presenter에서 코루틴을 구현하여 코루틴 내부에 Model에 지정해준 이벤트 들을 각각 호출하는 식으로 구현이 되나요..?

이해 했다고 생각햇는데 해보니 막상 뭔가 이상해서ㅠㅠ 이래서 Rx를 쓰는건지..싶네요..

#### A.6

음.. 단순히 데이터의 변화를 통보받는데 있어서는 코루틴은 적합하지 않는거같습니다;

Rx의 IObservable이 data의 변화에 대한 감시자(Observer)의 감시 대상물(Observable)이라 한다면, 
Coroutine은 비동기의 순차처리에 적합한 기능입니다. (가령 A를 한다음 B를 하여, C를 순차적으로 처리해야할때)
따라서 Coroutine으로 데이터의 감시를 한다 하는 부분은 Update로 구현하는것과 큰 차이가 없습니다.

가령 Rx를 안쓰는 형태로 구현을 한다면

-----

```
// Model (Non-Rx)

public class LoginAPI : SomeAPIBases 
{
public void Fetch() 
{
...
(int gold) =＞ 
{ // some callbacks
UserData.Instance.Gold = gold;
};
}
}


public class ShopPurchaseAPI : SomeAPIBases 
{
public void Fetch() 
{
...
(int gold) =＞ 
{ // some callbacks
UserData.Instance.gold = gold;
};
}
}


public class UserData : Singleton＜UserData＞ 
{
private int _gold;
public event Action＜int＞ onGoldChanged;

int Gold
{
set 
{
this._gold = value;
onGoldChanged.Invoke(value);
}
get 
{
return this._gold;
}
}
}
...

// HeaderPresenter
...
public UnityEngine.UI.Text goldLabel;

public void OnEnable() 
{
UserData.Instance.onGoldChanged += UpdateGold;
}

public void OnDisable() 
{
UserData.Instance.onGoldChanged -= UpdateGold;
}

public void UpdateGold(int updatedGold) 
{
this.goldLabel.text = string.Format("{N:0}",updatedGold);
}
```

-----

이걸 Rx를 쓴다면


```
public class LoginAPI : SomeAPIBases 
{
public void Fetch() 
{
...
(int gold) =＞ 
{ // some callbacks
UserData.Instance.gold.Value = gold;
};
}
}


public class ShopPurchaseAPI : SomeAPIBases 
{
public void Fetch() 
{
...
(int gold) =＞ 
{ // some callbacks
UserData.Instance.gold.Value = gold;
};
}
}


public class UserData : Singleton＜UserData＞ 
{
public ReactiveProperty＜int＞ gold = new ReactiveProperty＜int＞();
}

// HeaderPresenter
...
public UnityEngine.UI.Text goldLabel;
public IDisposable subscription = null;

public void Start() 
{
UserData.Instance.gold.Subscribe((int updatedGold) =＞ 
{
this.goldLabel.text = string.Format("{N:0}",updatedGold);
}).AddTo(this); // AddTo는 IDisposable을 MonoBehaviour가 Destroy될때 같이 Dispose(); 호출 하게끔 해주는 static extension입니다.
}
```