using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "YVelocityLessThan", menuName = "State Machines/Conditions/YVelocityLessThan")]
public class YVelocityLessThanSO : StateConditionSO
{
    [SerializeField] bool absoluteValue;
    [SerializeField] float threshold;
    protected override Condition CreateCondition() => new YVelocityLessThan(threshold, absoluteValue);
}

public class YVelocityLessThan : Condition
{
    bool absoluteValue;
    float threshold;
    MoveController moveCtrl;

    public YVelocityLessThan()
    {

    }

    public YVelocityLessThan(float v, bool abs)
    {
        threshold = v;
        absoluteValue = abs;
    }

    public override void Awake(StateMachine stateMachine)
    {
        moveCtrl = stateMachine.GetComponent<MoveController>();
    }

    protected override bool Statement()
    {
        float yVel = absoluteValue ? Mathf.Abs(moveCtrl.GetVelocity().y) : moveCtrl.GetVelocity().y;

        return yVel < threshold;
    }

    // public override void OnStateEnter()
    // {
    // }

    // public override void OnStateExit()
    // {
    // }
}
