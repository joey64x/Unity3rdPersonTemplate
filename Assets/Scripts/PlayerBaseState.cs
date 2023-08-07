using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected readonly PlayerStateMachine StateMachine;

    protected PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.StateMachine = stateMachine;
    }

    protected void CalculateMoveDirection()
    {
        Vector3 cameraForward = new(StateMachine.MainCamera.forward.x, 0, StateMachine.MainCamera.forward.z);
        Vector3 cameraRight = new(StateMachine.MainCamera.right.x, 0, StateMachine.MainCamera.right.z);

        Vector3 moveDirection = cameraForward.normalized * StateMachine.InputReader.MoveComposite.y +
                                cameraRight.normalized * StateMachine.InputReader.MoveComposite.x;

        StateMachine._velocity.x = moveDirection.x * StateMachine.MovementSpeed;
        StateMachine._velocity.z = moveDirection.z * StateMachine.MovementSpeed;
    }

    protected void FaceMoveDirection()
    {
        Vector3 faceDirection = new(StateMachine._velocity.x, 0f, StateMachine._velocity.z);

        if (faceDirection == Vector3.zero)
            return;

        StateMachine.transform.rotation = Quaternion.Slerp(StateMachine.transform.rotation,
            Quaternion.LookRotation(faceDirection), StateMachine.LookRotationDampFactor * Time.deltaTime);
    }

    protected void ApplyGravity()
    {
        if (StateMachine._velocity.y > Physics.gravity.y)
        {
            StateMachine._velocity.y += Physics.gravity.y * Time.deltaTime;
        }
    }

    protected void Move()
    {
        StateMachine.Controller.Move(StateMachine._velocity * Time.deltaTime);
    }
}