using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private readonly int JumpHash = Animator.StringToHash("Jump");
    private const float CrossFadeDuration = 0.1f;

    public PlayerJumpState(PlayerStateMachine StateMachine) : base(StateMachine) { }

    public override void Enter()
    {
        StateMachine._velocity = new Vector3(StateMachine._velocity.x, StateMachine.JumpForce, StateMachine._velocity.z);

        StateMachine.Animator.CrossFadeInFixedTime(JumpHash, CrossFadeDuration);
    }

    public override void Tick()
    {
        ApplyGravity();

        if (StateMachine._velocity.y <= 0f)
        {
            StateMachine.SwitchState(new PlayerFallState(StateMachine));
        }

        FaceMoveDirection();
        Move();
    }

    public override void Exit() { }
}