using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    private readonly int _moveSpeedHash = Animator.StringToHash("MoveSpeed");
    private readonly int _moveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
    private const float AnimationDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    public PlayerMoveState(PlayerStateMachine StateMachine) : base(StateMachine) { }

    public override void Enter()
    {
        StateMachine._velocity.y = Physics.gravity.y;

        StateMachine.Animator.CrossFadeInFixedTime(_moveBlendTreeHash, CrossFadeDuration);

        StateMachine.InputReader.OnJumpPerformed += SwitchToJumpState;
    }

    public override void Tick()
    {
        if (!StateMachine.Controller.isGrounded)
        {
            StateMachine.SwitchState(new PlayerFallState(StateMachine));
        }

        CalculateMoveDirection();
        FaceMoveDirection();
        Move();

        StateMachine.Animator.SetFloat(_moveSpeedHash, StateMachine.InputReader.MoveComposite.sqrMagnitude > 0f ? 1f : 0f, AnimationDampTime, Time.deltaTime);
    }

    public override void Exit()
    {
        StateMachine.InputReader.OnJumpPerformed -= SwitchToJumpState;
    }

    private void SwitchToJumpState()
    {
        StateMachine.SwitchState(new PlayerJumpState(StateMachine));
    }
}