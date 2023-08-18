using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCountDown;
    [SerializeField] private FloatReactiveProperty timeLeft;
    private readonly Subject<Unit> stoppedEvent = new();
    public readonly Subject<Unit> TimeUpEvent = new();
    public IReadOnlyReactiveProperty<float> TimeLeft => timeLeft;

    private void Start()
    {
        this.UpdateAsObservable()
            .TakeUntil(stoppedEvent)
            .TakeUntil(TimeUpEvent)
            .Subscribe(_ =>
            {
                timeLeft.Value -= Time.deltaTime;
                if (timeLeft.Value < 0f)
                {
                    timeLeft.Value = 0f;
                    TimeUpEvent.OnNext(Unit.Default);
                }
            });

        TimeLeft.Subscribe(t => textCountDown.text = $"Time: {t:00.00}");
    }

    public void Stop()
    {
        stoppedEvent.OnNext(Unit.Default);
    }
}
