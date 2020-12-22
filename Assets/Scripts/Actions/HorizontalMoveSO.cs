using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "HorizontalMove", menuName = "State Machines/Actions/Horizontal Move")]
public class HorizontalMoveSO : StateActionSO
{
    [SerializeField] float _acceleration;
    [SerializeField] float _deceleration;
    [SerializeField] float _maxVel;

    protected override StateAction CreateAction()
    {
        MovementInfo mInfo = new MovementInfo() {
            acceleration = _acceleration,
            deceleration = _deceleration,
            maxVelocity = _maxVel
        };

        return new HorizontalMove(mInfo);
    }
}

public class HorizontalMove : StateAction
{
    MoveController moveCtrl;
    AnimatorController animCtrl;
    MovementInfo _movementInfo;

    public HorizontalMove(MovementInfo mInfo)
    {
        _movementInfo = mInfo;
    }

	public override void Awake(StateMachine stateMachine)
	{
        moveCtrl = stateMachine.GetComponent<MoveController>();
        animCtrl = stateMachine.GetComponent<AnimatorController>();
	}

    public override void OnStateEnter()
    {
        animCtrl.Animator.SetBool(animCtrl.WALKING_BOOL, true);
    }

    public override void OnUpdate()
    {

    }

    public override void OnFixedUpdate()
	{
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveCtrl.Move(input, _movementInfo);
    }

    public override void OnStateExit()
    {
        //moveCtrl.KillHorizontalVelocity();
        animCtrl.Animator.SetBool(animCtrl.WALKING_BOOL, false);
    }
}
