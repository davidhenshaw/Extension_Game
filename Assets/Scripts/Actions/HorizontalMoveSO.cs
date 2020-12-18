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
    MoveController moveCtrl;
    
	public override void Awake(StateMachine stateMachine)
	{
        moveCtrl = stateMachine.GetComponent<MoveController>();
	}

    public override void OnUpdate()
    {
        //Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //moveCtrl.Move(input);
    }

    public override void OnFixedUpdate()
	{
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveCtrl.Move(input);
    }

    // public override void OnStateEnter()
    // {
    // }

    public override void OnStateExit()
    {
        moveCtrl.KillHorizontalVelocity();
    }
}
