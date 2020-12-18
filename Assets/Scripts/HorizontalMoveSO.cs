using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "HorizontalMove", menuName = "State Machines/Actions/Horizontal Move")]
public class HorizontalMoveSO : StateActionSO
{
	protected override StateAction CreateAction() => new HorizontalMove();
}

public class HorizontalMove : StateAction
{
	public override void Awake(StateMachine stateMachine)
	{
	}
		
	public override void OnUpdate()
	{
	}
	
	// public override void OnStateEnter()
	// {
	// }
	
	// public override void OnStateExit()
	// {
	// }
}
