using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "PlayerReset", menuName = "State Machines/Conditions/Player Reset")]
public class PlayerResetSO : StateConditionSO
{
	protected override Condition CreateCondition() => new PlayerReset();
}

public class PlayerReset : Condition
{
    PlayerController playerController;
    bool respawned = false;
	public override void Awake(StateMachine stateMachine)
	{
        playerController = stateMachine.GetComponent<PlayerController>();
        playerController.PlayerReset += OnReset;
    }
		
	protected override bool Statement()
	{
		return respawned;
	}

    void OnReset()
    {
        respawned = true;
    }

    public override void OnStateEnter()
    {
        playerController.PlayerReset += OnReset;
    }

    public override void OnStateExit()
    {
        playerController.PlayerReset -= OnReset;
        respawned = false;
    }
}
