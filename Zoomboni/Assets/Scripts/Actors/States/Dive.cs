using UnityEngine;

public class Dive : State
{

    [SerializeField] private AudioSource sfxLoop;
    [SerializeField] protected ParticleSystem fxLoop;

    [SerializeField] private State stateLand;
    [SerializeField] private State stateWallKick;

    [SerializeField] private float GRAVITY = 1.0f;
    [SerializeField] private float ACCELERATION = 1.0f;
    [SerializeField] private float FRICTION = 1.0f;

    [SerializeField] private float MODEL_TURN_SPEED = 4.0f;

    [SerializeField] private string anim;
    public override void Enter(Component arg)
    {
        sfxLoop.Play();
        fxLoop.Play();
        player.SetAnimation(anim);
    }

    public override void Exit()
    {
        sfxLoop.Stop();
        fxLoop.Stop();
    }

    public override void GraphicsUpdate()
    {
        player.UpdateContainerForModelRotation(MODEL_TURN_SPEED * Time.deltaTime);
    }

    public override void PhysicsUpdate()
    {
        Vector3 velocity = new Vector3(player.cc.velocity.x, player.cc.velocity.y, player.cc.velocity.z);

        velocity = ApplyGravity(velocity, GRAVITY);
        velocity = ApplyAcc(velocity, ACCELERATION);
        velocity = ApplyFriction(velocity, FRICTION);

        player.cc.Move(velocity * Time.deltaTime);
    }

    public override void TransitionCheck()
    {
        if (CheckSlide())
        {
            stateMachine.Change(stateLand);
        }
        if (CanWallKick() && player.inputKick.WasPressedThisFrame())
        {
            stateMachine.Change(stateWallKick);
        }
    }

}
