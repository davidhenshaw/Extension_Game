using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "HasLandedOnLayer", menuName = "State Machines/Conditions/Has Landed On Layer")]
public class HasLandedOnLayerSO : StateConditionSO
{
    [SerializeField] LayerMask layerMask;
	protected override Condition CreateCondition() => new HasLandedOnLayer(layerMask);
}

public class HasLandedOnLayer : Condition
{
    Collider2D groundCollider;
    LayerMask walkableLayers; //The layers that the player can jump from
    bool prev = false;
    bool curr = false;

    public HasLandedOnLayer(LayerMask layerMask)
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

        return curr && !prev;   //return true if you were previously touching ground but currently are not
    }

    // public override void OnStateEnter()
    // {
    // }

    // public override void OnStateExit()
    // {
    // }
}
