using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "YVelocityLessThan", menuName = "State Machines/Conditions/YVelocityLessThan")]
public class YVelocityLessThanSO : StateConditionSO
{
    [SerializeField] float threshold;

    protected override Condition CreateCondition() => new YVelocityLessThan(threshold);
}

public class YVelocityLessThan : Condition
{
    float threshold;
    MoveController moveCtrl;

    public YVelocityLessThan(float v)
    {
        threshold = v;
    }

    public override void Awake(StateMachine stateMachine)
    {
        moveCtrl = stateMachine.GetComponent<MoveController>();
    }

    protected override bool Statement()
    {
        return moveCtrl.GetVelocity().y < threshold;
    }

    // public override void OnStateEnter()
    // {
    // }

    // public override void OnStateExit()
    // {
    // }
}
