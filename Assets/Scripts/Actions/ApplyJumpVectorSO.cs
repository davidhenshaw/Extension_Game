using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ApplyJumpVector", menuName = "State Machines/Actions/Apply Jump Vector")]
public class ApplyJumpVectorSO : StateActionSO
{
	protected override StateAction CreateAction() => new ApplyJumpVector();
}

public class ApplyJumpVector : StateAction
{
    public ApplyJumpVector() : base()
    {
        
    }

    Vector2 jump;
    MoveController moveCtrl;
	public override void Awake(StateMachine stateMachine)
	{
        moveCtrl = stateMachine.GetComponent<MoveController>();
        jump = new Vector2(0, 5);
	}
		
	public override void OnUpdate()
	{
	}

    public override void OnStateEnter()
    {
        moveCtrl.ApplyForce(jump);
    }

    // public override void OnStateExit()
    // {
    // }
}
