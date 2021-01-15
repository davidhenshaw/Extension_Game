using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "DampJumpVector", menuName = "State Machines/Actions/Damp Jump Vector")]
public class DampJumpVectorSO : StateActionSO
{
    [SerializeField] [Range(0, 1)] float _jumpDamp;
    protected override StateAction CreateAction() => new DampJumpVector(_jumpDamp);
}

public class DampJumpVector : StateAction
{
    MoveController moveCtrl;
    float dampFactor;

    public DampJumpVector(float jumpDamp)
    {
        dampFactor = jumpDamp;
    }

    public override void Awake(StateMachine stateMachine)
	{
        moveCtrl = stateMachine.GetComponent<MoveController>();
    }
		
	public override void OnUpdate()
	{
        if (Input.GetButtonUp("Jump"))
        {
            //dampen vertical velocity
            Vector2 newVel = moveCtrl.GetVelocity();
            newVel.y = newVel.y * dampFactor;

            moveCtrl.SetVelocity(newVel);
        }
    }
	
}
