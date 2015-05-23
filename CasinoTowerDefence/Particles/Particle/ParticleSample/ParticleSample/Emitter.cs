using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class Emitter
{
    List<ModifierBase> modifiers;
    List<Particle> particles;

    string particleAssetName;
    protected Color beginColor1, beginColor2, endColor1, endColor2;
    public Vector2 Position;
    protected Vector2 beginDirection;
    ShapeBase spawnShape;
    public float particlesPerSecond;
    float particlesToSpawn;
    protected float startVelocity;
    int layer;
    protected byte beginAlpha1, beginAlpha2, endAlpha1, endAlpha2;
    float minBeginSize, maxBeginSize, minEndSize, maxEndSize;
    protected float minBeginRotation, maxBeginRotation, minEndRotation, maxEndRotation;

    public float lifeSpanLower = 3, lifeSpanUpper = 10;
    public void SetLifeSpanRange(float lower, float upper)
    {
        lifeSpanLower = lower;
        lifeSpanUpper = upper;
    }



    public ShapeBase SpawnShape
    {
        get { return spawnShape; }
        set { spawnShape = value; }
    }

    public Vector2 BeginDirection
    {
        set { beginDirection = value; }
    }

    public float StartVelocity
    {
        set { startVelocity = value; }
    }

    public void SetSizeRange(float minBeginSize, float maxBeginSize, float minEndSize, float maxEndSize)
    {
        this.minBeginSize = minBeginSize;
        this.maxBeginSize = maxBeginSize;
        this.minEndSize = minEndSize;
        this.maxEndSize = maxEndSize;
    }

    public void Rotate(int speed)
    {

    }

    public void SetColorRange(Color beginColor1, Color beginColor2, Color endColor1, Color endColor2)
    {
        this.beginColor1 = beginColor1;
        this.beginColor2 = beginColor2;
        this.endColor1 = endColor1;
        this.endColor2 = endColor2;
    }

    public void SetAlphaRange(byte beginAlpha1, byte beginAlpha2, byte endAlpha1, byte endAlpha2)
    {
        this.beginAlpha1 = beginAlpha1;
        this.beginAlpha2 = beginAlpha2;
        this.endAlpha1 = endAlpha1;
        this.endAlpha2 = endAlpha2;
    }

    public void SetRotationRange(float minBeginRotation, float maxBeginRotation, float minEndRotation, float maxEndRotation)
    {
        this.minBeginRotation = minBeginRotation;
        this.maxBeginRotation = maxBeginRotation;
        this.minEndRotation = minEndRotation;
        this.maxEndRotation = maxEndRotation;
    }

    public Emitter(string particleAssetName, int layer)
    {
        this.layer = layer;
        this.particleAssetName = particleAssetName;
        particlesPerSecond = 1500;
        Position = new Vector2(300, 300);
        startVelocity = 50;
        modifiers = new List<ModifierBase>();
        particles = new List<Particle>();
        beginDirection = Vector2.Zero;
        modifiers.Add(new BasicModifier());

        SpawnShape = new PointShape();

        minBeginSize = 0;
        maxBeginSize = 0;
        minEndSize = 0;
        maxEndSize = 0;


        beginColor1 = Color.White;
        endColor1 = Color.White;
        beginColor2 = Color.White;
        endColor2 = Color.White;

        beginAlpha1 = 255;
        endAlpha1 = 255;

        beginAlpha2 = 255;
        endAlpha2 = 255;
    }

    public void HandleInput(InputHelper inputHelper)
    {

    }


    public void Update(float timePassed)
    {

        particlesToSpawn += particlesPerSecond * timePassed;
        Random random = new Random();

        while (particlesToSpawn > 0)
        {
            Vector2 velocity;
            if (beginDirection == Vector2.Zero)
            {
                 velocity = new Vector2((float)random.NextDouble() - 0.5f, (float)random.NextDouble() - 0.5f);
            }
            else
            {
                velocity = beginDirection;
            }

            velocity = velocity * startVelocity;

            Particle p = new Particle(Position + SpawnShape.GetRandomPosition(), velocity, particleAssetName, beginColor1, 0);

            p.Layer = layer;

            p.TimeToLive = MathHelper.Lerp(lifeSpanLower, lifeSpanUpper, (float)random.NextDouble());

            p.BeginSize = MathHelper.Lerp(minBeginSize, maxBeginSize, (float)random.NextDouble());
            p.EndSize = MathHelper.Lerp(minEndSize, maxEndSize, (float)random.NextDouble());

            p.BeginColor = Color.Lerp(beginColor1, beginColor2, (float)random.NextDouble());
            p.EndColor = Color.Lerp(endColor1, endColor2, (float)random.NextDouble());

            p.BeginAlpha = (byte)MathHelper.Lerp(beginAlpha1, beginAlpha2, (float)random.NextDouble());
            p.EndAlpha = (byte)MathHelper.Lerp(endAlpha1, endAlpha2, (float)random.NextDouble());

            p.BeginRotation = MathHelper.Lerp(minBeginRotation, maxBeginRotation, (float)random.NextDouble());
            p.EndRotation = MathHelper.Lerp(minEndRotation, maxEndRotation, (float)random.NextDouble());

            particles.Add(p);

            particlesToSpawn--;
        }

        foreach (ModifierBase m in modifiers)
            m.Update(timePassed, particles);

        List<Particle> stillAlive = new List<Particle>(particles.Count);
        foreach (Particle p in particles)
        {
            if (p.IsAlive)
                stillAlive.Add(p);
        }

        particles = stillAlive;

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);

        foreach (Particle p in particles)
            p.Draw(spriteBatch);

        //spriteBatch.End();
    }

    public void AddModifier(ModifierBase modifier)
    {
        modifiers.Add(modifier);
    }
}
