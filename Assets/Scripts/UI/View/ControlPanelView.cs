using System;
using System.Collections.Generic;
using System.Linq;
using Abstractions;
using UnityEngine;
using UnityEngine.UI;


public class ControlPanelView : MonoBehaviour
{
    public event Action<ICommandExecutor> OnClick = (executor => { }); 
    
    [SerializeField] private Button attackButton;
    [SerializeField] private Button moveButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button unitProduceButton;
    [SerializeField] private Button patrolButton;

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
                typeof(CommandExecutorBase<ICancelCommand>),
                cancelButton
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

    public void SetButtons(params ICommandExecutor[] executors)
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
                    button.onClick.AddListener(() => OnClick.Invoke(executors[i1]));
                }
            }
        }
    }
}
