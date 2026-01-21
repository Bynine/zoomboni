using UnityEngine;

public class Brake : Slide
{

    [SerializeField] private State stateSlide;

    [SerializeField] private ParticleSystem fx;

    public override void Enter(Component statePrior)
    {
        base.Enter(statePrior);
        fx.Play();
    }

    public override void Exit()
    {
        base.Exit();
        fx.Stop();
    }

    public override void TransitionCheck()
    {

        if (!CheckGround())
        {
            stateMachine.Change(stateAirborne);
        }

        if (!player.inputSlide.IsPressed())
        {
            stateMachine.Change(stateSlide, this);
        }
    }

}