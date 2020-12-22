using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ApplyJumpVector", menuName = "State Machines/Actions/Apply Jump Vector")]
public class ApplyJumpVectorSO : StateActionSO
{
    [SerializeField] float _jumpForce;
    [SerializeField] [Range(0,1)] float _jumpDamp;
	protected override StateAction CreateAction() => new ApplyJumpVector(_jumpForce, _jumpDamp);
}

public class ApplyJumpVector : StateAction
{
    Vector2 jump;
    MoveController moveCtrl;
    float dampFactor;

    public ApplyJumpVector(float jumpForce, float jumpDamp) : base()
    {
        jump = new Vector2(0, jumpForce);
        dampFactor = jumpDamp;
    }


	public override void Awake(StateMachine stateMachine)
	{
        moveCtrl = stateMachine.GetComponent<MoveController>();
	}
		
	public override void OnUpdate()
	{
        if(Input.GetButtonUp("Jump"))
        {
            //dampen vertical velocity
            Vector2 newVel = moveCtrl.GetVelocity();
            newVel.y = newVel.y * dampFactor;

            moveCtrl.SetVelocity(newVel);
        }
	}

    public override void OnStateEnter()
    {
        moveCtrl.ApplyForce(jump);
    }

    // public override void OnStateExit()
    // {
    // }
}
