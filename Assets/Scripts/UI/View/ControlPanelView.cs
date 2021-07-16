using System;
using System.Collections.Generic;
using System.Linq;
using Abstractions;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


public class ControlPanelView : MonoBehaviour
{
    public event Action<ICommandExecutor, ICommandQueue> OnClick = (executor, queue) => { }; 
    
    [SerializeField] private Button attackButton;
    [SerializeField] private Button moveButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button unitProduceButton;
    [SerializeField] private Button patrolButton;

    public IObservable<Unit> AttackClickObservable => attackButton.onClick.AsObservable();
    public IObservable<Unit> MoveClickObservable => moveButton.OnClickAsObservable();
    public IObservable<Unit> CancelClickObservable => cancelButton.OnClickAsObservable();
    public IObservable<Unit> UnitProduceObservable => unitProduceButton.OnClickAsObservable();
    public IObservable<Unit> PatrolButtonObservable => patrolButton.OnClickAsObservable();
    
    private Dictionary<Type, Button> _switchDictionary;
    
    // Start is called before the first frame update
    void Start()
    {
        _switchDictionary = new Dictionary<Type, Button>()
        {
            {
                typeof(CommandExecutorBase<IAttackCommand>),
                attackButton
            },
            {
                typeof(CommandExecutorBase<IMoveCommand>),
                moveButton
            },
            {
                typeof(CommandExecutorBase<ICreateUnitCommand>),
                unitProduceButton
            },
            {
                typeof(CommandExecutorBase<IPatrolCommand>),
                patrolButton
            }
        };
        ClearButtons();
    }

    public void ClearButtons()
    {
        foreach (var sch in _switchDictionary)
        {
            sch.Value.gameObject.SetActive(false);
            sch.Value.onClick.RemoveAllListeners();
        }
    }

    public void SetButtons(ICommandQueue commandQueue, params ICommandExecutor[] executors)
    {
        if (executors != null && executors.Length > 0)
        {
            for (int i = 0; i < executors.Length; i++)
            {
                Button button = _switchDictionary.FirstOrDefault(x => x.Key.IsInstanceOfType(executors[i])).Value;
                if (button != null)
                {
                    button.gameObject.SetActive(true);
                    var i1 = i;
                    button.onClick.AddListener(() => OnClick.Invoke(executors[i1], commandQueue));
                }
            }
        }
        cancelButton.onClick.AddListener(OnCancel.Invoke);
    }

    private void OnDestroy()
    {
        ClearButtons();
        _switchDictionary.Clear();
        _switchDictionary = null;
        cancelButton.onClick.RemoveAllListeners();
    }

    public event Action OnCancel = () => {};
}
