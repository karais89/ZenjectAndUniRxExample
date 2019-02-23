# [Unite17] 유니티에서차세대프로그래밍을 UniRx 소개 및 활용

- [초기 슬라이드 자료 [160402_데브루키_박민근] UniRx 소개 - SlideShare](https://www.slideshare.net/agebreak/160402-unirx)
- [최신 슬라이드 자료](https://www.slideshare.net/agebreak/unite17-unirx)
- [예제 소스](https://github.com/TORISOUP/UniRxExamples)
- [유니티 웹 실행 예제](http://torisoup.net/unirx-examples/)
- [Reactive Programming 이란?](https://www.slideshare.net/jongwookkim/ndc14-rx-functional-reactive-programming)

## [마우스의 더블 클릭 판정] 구현 하실 수 있나요?

How? (기존 방법)
- 최후에 클릭 했을 때부터 일정 시간 이내라면 더블 클릭 판정
- 클릭 횟수 변수와 타이머 변수를 필드에 정의
- Update() 내에 판정 처리를 구현

**귀찮다!!!!!!**

이것을 UniRx를 사용하면 단지 **몇줄**로 끝낼 수 있습니다.

```csharp
var clickStream = Observable.EveryUpdate()
    .Where(_ => Input.GetMouseButtonDown(0));

clickStream.Buffer(clickStream.Throttle(TimeSpan.FromMilliseconds(200)))
    .Where(x => x.Count >= 2)
    .SubscribeToText(text, x => $"DoubleClick detected!\n Count:{x.Count}");
```

단지 이것뿐!!

실제의 사용예를 통해서 UniRx의 대단함과 편리함을 전하고 싶다.

대상 : LINQ는 사용할 수 있는 레벨 (프로그래밍 초보자에게는 조금 어려울지도)

## 1. UniRx란?

- Reactive Extensions for Unity
- MIT 라이센스 공개
- AssetStore or Github에서 다운로드 가능 (무료)

### Reactive Extension

Functional Reactive Programming을 C#에서 구현하기 위한 라이브러리

- LINQ to Events
- 시작은 Microsoft Reseach가 개발 (.NET)
- 최근 들어 유행의 조짐이 오고 있다
    - 다양한 언어로 이식되고 있음
        - RxJS, RxJava, ReactiveCocoa, RxPy, RxLua, UniRx..
    - 어떤 언어라도 Rx의 개념은 같다

### UniRx
- .NET용 Rx은 유니티의 Mono에서는 사용 불가
    - Mono의 .NET 버전 문제
    - 그리고 .NET용 Rx는 무겁고 크다
- UniRx
    - Unity(C#)에서 사용할 수 있게 만든 가볍고 빠른 유니티 전용 Rx
    - Unity 전용으로 사용할 수 있는 Rx 스트림들을 제공
    - 일본에서 개발 (일본어로 된 문서가 많다)

## 2. UniRx가 무엇이 편리한가?
- UniRx를 사용 하면 **시간**의 취급이 굉장히 간단해 집니다.

### 시간이 결정하는 처리의 예
- 이벤트의 기다림
    - 마우스 클릭이나 버튼의 입력 타이밍에 무언가를 처리 할 때   
- 비동기 처리
    - 다른 스레드에서 통신을 하거나, 데이터를 로드할 때
- 시간 측정이 판정에 필요한 처리
    - 홀드, 더블클릭의 판정
- 시간 변화하는 값의 감시
    - False->True가 되는 순간에 1회만 처리하고 싶을 때

이런 처리를 Rx를 사용하면 상당히 간결하게 작성 가능합니다.

### 버튼이 클릭되면 화면에 표시

```csharp
public Button button;
public Text text;

private void Start()
{
    button.onClick // Unity가 제공하는 클릭 이벤트
        .AsObservable() // 이벤트를 스트림으로 변경
        .Subscribe(_ => text.text += "Clicked\n"); // 스트림의 구독 (최종적으로 무엇을 할것인가를 작성)
}
```

#### 스트림

- [이벤트가 흐르는 파이프] 같은 이미지
    - 어렵게 말하자면, [타임라인에 배열되어 있는 이벤트의 시퀀스]
    - 분기 되거나 합쳐지는게 가능하다
- 코드 안에서는 IObservable<T>로 취급된다
    - LINQ에서 IEnumerable<T>에 해당

![UniRx Stream](images/unirx_stream.png)

#### 스트림에 흐르는 이벤트<메시지>

메시지는 3종류가 있다
- OnNext
    - 일반적으로 사용되는 메시지
    - 보통은 이것을 사용한다
- OnError
    - 에러 발생시에 예외를 통지하는 메시지
- OnCompleted
    - 스트림이 완료되었음을 통지하는 메시지

#### 스트림과 버튼 클릭

버튼은 **[클릭 때에 이벤트를 스트림에 보낸다]** 라고 생각하는 것이 가능하다

![UniRx Button Stream](images/unirx_button_stream.png)

#### Subscribe (스트림의 구독)

- 스트림의 말단에서 메시지가 올때 무엇을 할 것인지를 정의 한다
- 스트림은 Subscribe 된 순간에 생성 된다
    - 기본적으로 Subscribe하지 않는 한 스트림은 동작하지 않는다
    - Subscribe 타이밍에 의해서 결과가 바뀔 가능성이 있다
- OnError, OnComplete가 오면 Subscribe는 종료된다.

![UniRx Subscribe Stream](images/unirx_subscribe_stream.png)

#### Subscribe와 메시지
- Subscribe는 오버로드로 여러 개 정의되어 있어서, 용도에 따라 사용하는게 좋다
    - OnNext만
    - OnNext & OnCompleted
    - OnNext & OnError & OnCompleted

## 3. 스트림을 사용하는 메리트와 예
## 4. 자주 사용하는 오퍼레이터 설명
## 5. Unity에서의 실용 사례 4가지
## 6. 정리