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
    string WALKING = "isWalking";

    MoveController moveCtrl;
    AnimatorController animCtrl;
	public override void Awake(StateMachine stateMachine)
	{
        moveCtrl = stateMachine.GetComponent<MoveController>();
        animCtrl = stateMachine.GetComponent<AnimatorController>();
	}

    public override void OnStateEnter()
    {
        animCtrl.Animator.SetBool(WALKING, true);
    }

    public override void OnUpdate()
    {

    }

    public override void OnFixedUpdate()
	{
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveCtrl.Move(input);
    }



    public override void OnStateExit()
    {
        moveCtrl.KillHorizontalVelocity();
        animCtrl.Animator.SetBool(WALKING, false);
    }
}
