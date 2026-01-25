using UnityEngine;

public class WallKick : State {

    [SerializeField] private State stateExitGround;    
    [SerializeField] private State stateExitAir;

    [SerializeField] private Timer duration;

    [SerializeField] private float kickStrength;
    [SerializeField] private string anim;
    
    [SerializeField] private ParticleSystem fxStart;
    [SerializeField] private ParticleSystem fxTrailOptional;
    
    [SerializeField] private AudioSource sfxEnterOptional;
    [SerializeField] private AudioSource sfxLoopOptional;

    private bool shouldIApplyStartForce = false;

    public override void Enter(Component arg) {
        duration.Reset();

        fxStart.transform.position = player.transform.position; //create particle effect at player
        fxStart.Play();

        //if optional stuff exitsts, play it
        if (fxTrailOptional) fxTrailOptional.Play();
        if (sfxEnterOptional) sfxEnterOptional.Play();
        if (sfxLoopOptional) sfxLoopOptional.Play();
        shouldIApplyStartForce = true; //means "does stuff on frame 1
    }

    public override void Exit() {
        if (fxTrailOptional) fxTrailOptional.Stop();
        if (sfxLoopOptional) sfxLoopOptional.Stop();
    }

    public override void GraphicsUpdate() {
        player.SetAnimation(anim, true);
    }

    public override void PhysicsUpdate() {
        Vector3 velocity = player.cc.velocity;

        if (shouldIApplyStartForce) {
            
            
            shouldIApplyStartForce = false;
        }
        else {
            velocity = ApplyAcc(velocity, ACC);
            velocity = ApplyFriction(velocity, FRIC);
            velocity = ApplyGravity(velocity, GRAV);
            velocity = ApplyForce(velocity);
        }

        player.cc.Move(velocity * Time.deltaTime);
    }

    public override void TransitionCheck() {
        if (!duration.IsActive()) {
            if (CheckGround())
            {
                stateMachine.Change(stateExitGround);
            }
            else
            {
                stateMachine.Change(stateExitAir);
            }
        }
    }

}