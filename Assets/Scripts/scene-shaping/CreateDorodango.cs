using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace scene_shaping
{
    public class CreateDorodango : MonoBehaviour
    {
        [SerializeField] private int clickCountToChangeShape = 6;
        [SerializeField] private int clickCountToMoveTarget = 3;
        [SerializeField] private GameObject[] shapeObjects;
        [SerializeField] private CountDownTimer timer;
        [SerializeField] private Camera mainCamera;

        [SerializeField] private ClickTarget clickTarget;

        private int currentShapeIndex = 0;

        private void Start()
        {
            SetActiveShapeByIndex(currentShapeIndex);

            // clickTarget がクリックされるたびに発火する Observable
            var targetClickedEvent = this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Where(_ =>
                {
                    var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                    return Physics.Raycast(ray, out var hit, Mathf.Infinity) && hit.collider == clickTarget.collider;
                });

            // clickCountToMoveTarget 回に1回実行
            targetClickedEvent.Buffer(clickCountToMoveTarget)
                .Subscribe(_ => { clickTarget.OnTargetCleared(); });

            targetClickedEvent.Buffer(clickCountToChangeShape)
                .Take(shapeObjects.Length - 1) // 変形は length-1 回
                .Subscribe(_ => { SetActiveShapeByIndex(++currentShapeIndex); },
                    () =>
                    {
                        clickTarget.gameObject.SetActive(false);
                        timer.Stop();
                    });

            timer.TimeUpEvent.Subscribe(_ => clickTarget.gameObject.SetActive(false));
        }

        /// shapeObjects のうち，指定された index のもののみ表示する
        private void SetActiveShapeByIndex(int index)
        {
            for (var i = 0; i < shapeObjects.Length; i++) shapeObjects[i].SetActive(i == index);
        }
    }
}
