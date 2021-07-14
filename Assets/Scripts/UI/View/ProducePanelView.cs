using System;
using Abstractions;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class ProducePanelView : MonoBehaviour
    {
        public IObservable<int> CancelButtonClicks => _cancelButtonClicks;

        [SerializeField] private Slider _productionProgressSlider;
        [SerializeField] private Image _currentUnitImage;

        [SerializeField] private Image[] _images;
        [SerializeField] private GameObject[] _imageHolders;
        [SerializeField] private Button[] _buttons;

        private Subject<int> _cancelButtonClicks = new Subject<int>();

        private IDisposable _unitProductionTaskCt;


        private void Init()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                var index = i;
                _buttons[i].onClick.AddListener(() => _cancelButtonClicks.OnNext(index));
            }
        }

        public void Clear()
        {
            for (int i = 0; i < _images.Length; i++)
            {
                _images[i].sprite = null;
                _imageHolders[i].SetActive(false);
            }
            _productionProgressSlider.gameObject.SetActive(false);
            _currentUnitImage.sprite = null;
            _currentUnitImage.enabled = false;
            _unitProductionTaskCt?.Dispose();
        }

        public void SetTask(IProductionTask task, int index)
        {
            if (task == null)
            {
                _imageHolders[index].SetActive(false);
                _images[index].sprite = null;

                if (index == 0)
                {
                    _productionProgressSlider.gameObject.SetActive(false);
                    _currentUnitImage.sprite = null;
                    _currentUnitImage.enabled = false;
                    _unitProductionTaskCt?.Dispose();
                }
            }
            else
            {
                _imageHolders[index].SetActive(true);
                _images[index].sprite = task.Icon;

                if (index == 0)
                {
                    _productionProgressSlider.gameObject.SetActive(true);
                    _currentUnitImage.sprite = task.Icon;
                    _currentUnitImage.enabled = true;
                    _unitProductionTaskCt = Observable.EveryUpdate()
                        .Subscribe(_ =>
                        {
                            _productionProgressSlider.value = task.TimeProduce / task.TimeProduceMax;
                        });
                }
            }
        }
    }
}