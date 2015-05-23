using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;



public abstract class ModifierBase
{

    public abstract void Update(float timePassed, List<Particle> Particles);
}


public class BasicModifier : ModifierBase
{
    public override void Update(float timePassed, List<Particle> Particles)
    {
        foreach (Particle p in Particles)
        {
            p.TimeLived += timePassed;

            p.Position += p.Velocity * timePassed;

            p.Color = Color.Lerp(p.BeginColor, p.EndColor, p.LifePhase);

            p.Size = MathHelper.Lerp(p.BeginSize, p.EndSize, p.LifePhase);

            p.Alpha = (byte)MathHelper.Lerp(p.BeginAlpha, p.EndAlpha, p.LifePhase);

            p.Rotation = MathHelper.Lerp(p.BeginRotation, p.EndRotation, p.LifePhase);
        }
    }
}

public class BoxedBoundsModifier : ModifierBase
{
    Vector2 maxBounds, minBounds;

    public BoxedBoundsModifier(Vector2 maxBounds, Vector2 minBounds)
        : base()
    {
        this.maxBounds = maxBounds;
        this.minBounds = minBounds;
    }

    public override void Update(float timePassed, List<Particle> Particles)
    {
        foreach (Particle p in Particles)
        {
            if (p.Position.X >= maxBounds.X)
                p.Velocity.X = -Math.Abs(p.Velocity.X)*0.75f;
            if (p.Position.X <= minBounds.X)
                p.Velocity.X = Math.Abs(p.Velocity.X)*0.75f;
            if (p.Position.Y >= maxBounds.Y)
                p.Velocity.Y = -Math.Abs(p.Velocity.Y)*0.75f;
            if (p.Position.Y <= minBounds.Y)
                p.Velocity.Y = Math.Abs(p.Velocity.Y)*0.75f;
        }
    }
    
}
    

public class GravityModifier : ModifierBase
{
    Vector2 force;

    public GravityModifier(Vector2 direction, float force)
        : base()
    {
        direction.Normalize();
        this.force = direction * force;
    }

    public override void Update(float timePassed, List<Particle> Particles)
    {
        foreach (Particle p in Particles)
        {
            p.Velocity += force * timePassed;
        }
    }
}

public class MouseForceModifier : ModifierBase
{
    Vector2 force;
    float pullForce;

    public MouseForceModifier(float force = 1)
        : base()
    {
        pullForce = force;

    }

    public override void Update(float timePassed, List<Particle> Particles)
    {
        MouseState state = Mouse.GetState();

        Vector2 mousePos = new Vector2(state.X, state.Y);

        foreach (Particle p in Particles)
        {
            force = mousePos - p.Position;
            force *= pullForce;
            MathHelper.Lerp(0, pullForce, p.LifePhase);
            p.Velocity += force * timePassed;
        }
    }
}
