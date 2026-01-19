using UnityEngine;

public class Airborne : State
{

    [SerializeField] private State stateSlide;

    [SerializeField] private float GRAVITY = 1.0f;
    [SerializeField] private float ACCELERATION = 1.0f;
    [SerializeField] private float FRICTION = 1.0f;
    public override void Enter(Component arg)
    {
        player.containerForModel.transform.localScale = new Vector3(0.8f, 1.3f, 0.8f);
    }

    public override void Exit()
    {

    }

    public override void GraphicsUpdate()
    {

    }

    public override void PhysicsUpdate()
    {
        Vector3 velocity = new Vector3(player.cc.velocity.x, player.cc.velocity.y, player.cc.velocity.z);

        velocity = ApplyGravity(velocity, GRAVITY);


        player.cc.Move(velocity * Time.deltaTime);
    }

    public override void TransitionCheck()
    {
        bool isPlayerCloseToGround = CheckGround();

        if (isPlayerCloseToGround)
        {
            stateMachine.Change(stateSlide, this);
        }
    }
}
