using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "DoNothing", menuName = "State Machines/Actions/Do Nothing")]
public class DoNothingSO : StateActionSO
{
	protected override StateAction CreateAction() => new DoNothing();
}

public class DoNothing : StateAction
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
