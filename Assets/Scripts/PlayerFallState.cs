using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    private readonly int FallHash = Animator.StringToHash("Fall");
    private const float CrossFadeDuration = 0.1f;

    public PlayerFallState(PlayerStateMachine StateMachine) : base(StateMachine) { }

    public override void Enter()
    {
        StateMachine._velocity.y = 0f;

        StateMachine.Animator.CrossFadeInFixedTime(FallHash, CrossFadeDuration);
    }

    public override void Tick()
    {
        ApplyGravity();
        Move();

        if (StateMachine.Controller.isGrounded)
        {
            StateMachine.SwitchState(new PlayerMoveState(StateMachine));
        }
    }

    public override void Exit() { }
}