using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsWalking", menuName = "State Machines/Conditions/Is Walking")]
public class IsWalkingSO : StateConditionSO
{
	protected override Condition CreateCondition() => new IsWalking();
}

public class IsWalking : Condition
{
    MoveController moveCtrl;
    float _minVelocity = 0.09f;
	public override void Awake(StateMachine stateMachine)
	{
        moveCtrl = stateMachine.GetComponent<MoveController>();
	}
		
	protected override bool Statement()
	{
        float xVel = Mathf.Abs(moveCtrl.GetVelocity().x);
        Debug.Log(xVel);

        //return true;
		return xVel > _minVelocity;
	}
	
	// public override void OnStateEnter()
	// {
	// }
	
	// public override void OnStateExit()
	// {
	// }
}
