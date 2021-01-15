using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "YVelocityCompare", menuName = "State Machines/Conditions/YVelocity Compare")]
public class YVelocityCompareSO : StateConditionSO
{
    [SerializeField] bool absoluteValue;
    [SerializeField] CompareType compareType;
    [SerializeField] float threshold;
    protected override Condition CreateCondition() => new YVelocityCompare(threshold, absoluteValue, compareType);
}

public enum CompareType
{
    LessThan, GreaterThan, Equals
}

public class YVelocityCompare : Condition
{
    bool absoluteValue;
    float threshold;
    CompareType compareType;
    MoveController moveCtrl;

    public YVelocityCompare(float v, bool abs, CompareType ct)
    {
        threshold = v;
        absoluteValue = abs;
        compareType = ct;
    }

    public override void Awake(StateMachine stateMachine)
    {
        moveCtrl = stateMachine.GetComponent<MoveController>();
    }

    protected override bool Statement()
    {
        float yVel = absoluteValue ? Mathf.Abs(moveCtrl.GetVelocity().y) : moveCtrl.GetVelocity().y;
        bool result = false;

        switch(compareType)
        {
            case CompareType.GreaterThan:
                result = yVel > threshold;
                break;

            case CompareType.LessThan:
                result = yVel < threshold;
                break;

            case CompareType.Equals:
                result = yVel == threshold;
                break;
        }

        return result;
    }

    // public override void OnStateEnter()
    // {
    // }

    // public override void OnStateExit()
    // {
    // }
}
