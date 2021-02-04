using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "SetAnimationBool", menuName = "State Machines/Actions/Set Animation Bool")]
public class SetAnimationBoolSO : StateActionSO
{
    [SerializeField] string variable;
    [SerializeField] bool setTo;
    [SerializeField] bool toggleOnExit;
	protected override StateAction CreateAction() => new SetAnimationBool(variable, setTo, toggleOnExit);
}

public class SetAnimationBool : StateAction
{
    Animator animator;
    string variable;
    bool setTo;
    bool toggleOnExit;

    public SetAnimationBool(string v, bool value, bool toggle)
    {
        variable = v;
        setTo = value;
        toggleOnExit = toggle;
    }

	public override void Awake(StateMachine stateMachine)
	{
        animator = stateMachine.GetComponent<Animator>();
	}
		
	public override void OnUpdate()
	{
	}

    public override void OnStateEnter()
    {
        animator.SetBool(variable, setTo);
    }

    public override void OnStateExit()
    {
        if(toggleOnExit)
            animator.SetBool(variable, !setTo);
    }
}
