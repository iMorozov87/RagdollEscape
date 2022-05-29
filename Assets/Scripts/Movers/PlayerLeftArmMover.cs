public class PlayerLeftArmMover : CharacterMover
{   
    private void FixedUpdate()
    {
        MoveTowardsTheTarget(Effector, Target.position );
        if (Effector.position ==Target.position)
        {
            enabled = false;
        }
    }    
}
