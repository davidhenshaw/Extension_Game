using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "HasLeftGround", menuName = "State Machines/Conditions/Has Left Ground")]
public class HasLeftGroundSO : StateConditionSO
{
    [SerializeField] LayerMask walkableLayers;
	protected override Condition CreateCondition() => new HasLeftGround(walkableLayers);
}

public class HasLeftGround : Condition
{
    Collider2D groundCollider;
    LayerMask walkableLayers; //The layers that the player can jump from
    bool prev = false;
    bool curr = false;

    public HasLeftGround(LayerMask layerMask)
    {
        walkableLayers = layerMask;
    }

    public override void Awake(StateMachine stateMachine)
    {
        groundCollider = stateMachine.GetComponent<PlayerSensorHandler>().GetGroundCollider();
    }

    protected override bool Statement()
    {
        prev = curr;
        curr = groundCollider.IsTouchingLayers(walkableLayers);

        return !curr && prev;   //return true if you were previously touching ground but currently are not
    }

    // public override void OnStateEnter()
    // {
    // }

    // public override void OnStateExit()
    // {
    // }
}
