using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsGrounded", menuName = "State Machines/Conditions/Is Grounded")]
public class IsGroundedSO : StateConditionSO
{
    [SerializeField] LayerMask walkableLayers;
	protected override Condition CreateCondition() => new IsGrounded(walkableLayers);


}

public class IsGrounded : Condition
{
    Collider2D groundCollider;
    int walkableLayers;

    public IsGrounded(LayerMask layerMask)
    {
        walkableLayers = layerMask;
    }

	public override void Awake(StateMachine stateMachine)
	{
        groundCollider = stateMachine.GetComponent<PlayerSensorHandler>().GetGroundCollider();
	}
		
	protected override bool Statement()
	{
		return groundCollider.IsTouchingLayers(walkableLayers);
	}
	
	// public override void OnStateEnter()
	// {
	// }
	
	// public override void OnStateExit()
	// {
	// }
}
