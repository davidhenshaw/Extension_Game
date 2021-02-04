using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "HasPower", menuName = "State Machines/Conditions/Has Power")]
public class HasPowerSO : StateConditionSO
{
	protected override Condition CreateCondition() => new HasPower();
}

public class HasPower : Condition
{
    PowerSink powerSink;
    bool powerLost = false;
	public override void Awake(StateMachine stateMachine)
	{
        powerSink = stateMachine.GetComponentInChildren<PowerSink>();
	}
		
	protected override bool Statement()
	{
		return !powerLost;
	}

    void OnPowerLost()
    {
        powerLost = true;
    }

    public override void OnStateEnter()
    {
        powerSink.OnPowerLost.AddListener(this.OnPowerLost);
    }

    public override void OnStateExit()
    {
        powerLost = false;
        powerSink.OnPowerLost.RemoveListener(this.OnPowerLost);
    }
}
