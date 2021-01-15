using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsWalking", menuName = "State Machines/Conditions/Is Walking")]
public class IsWalkingSO : StateConditionSO
{
    [SerializeField] float minWalkingVelocity = 0.15f;
	protected override Condition CreateCondition() => new IsWalking(minWalkingVelocity);
}

public class IsWalking : Condition
{
    MoveController moveCtrl;
    float _minVelocity;

    public IsWalking(float min)
    {
        _minVelocity = min;
    }

	public override void Awake(StateMachine stateMachine)
	{
        moveCtrl = stateMachine.GetComponent<MoveController>();
	}
		
	protected override bool Statement()
	{
        float xVel = Mathf.Abs(moveCtrl.GetVelocity().x);
        float inputAxis = Mathf.Abs(Input.GetAxisRaw("Horizontal"));

        return (xVel > _minVelocity) || (inputAxis > Mathf.Epsilon);
	}
	
	// public override void OnStateEnter()
	// {
	// }
	
	// public override void OnStateExit()
	// {
	// }
}
