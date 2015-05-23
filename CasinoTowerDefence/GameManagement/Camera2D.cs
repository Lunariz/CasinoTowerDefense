using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

public class Camera2D 
{
    protected float zoom, rotation;
    protected Vector2 position, prevPos, origin, velocity;
    protected Rectangle bounds, limits;
    protected Viewport viewport;
    public Camera2D(Viewport viewport) 
    {
        Bounds = new Rectangle(viewport.X, viewport.Y, viewport.Width, viewport.Height);
        zoom = 1.0f;
        rotation = 0f;
        position = Vector2.Zero;
        prevPos = position;
        velocity = Vector2.Zero; 
        Origin = new Vector2(viewport.Width / 2.0f, viewport.Height / 2.0f);
        limits = Rectangle.Empty;
        this.viewport = viewport;
    }

    public void Update(GameTime gameTime)
    {
        velocity = (position - prevPos) / (float)gameTime.ElapsedGameTime.TotalSeconds;
        prevPos = position;
    }
    
    public void LookAt(Vector2 lookPos)
    {
        Origin = lookPos;
        position = Origin - new Vector2(GameEnvironment.Screen.X, GameEnvironment.Screen.Y) / 2;
        //Debug.Print("origin: " + Origin + ", topleft: " + position + ", lookPos: " + lookPos);
    }

    public float Zoom 
    {
        get { return zoom; }
        set { zoom = value; }
    }
    public Vector2 Position 
    {
        get { return position; }
        set { position = value; }
    }

    public Vector2 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }
    
    public Vector2 Origin
    {
        get { return origin; }
        set
        {
            if (limits != null)
            {
                float posX, posY;
                posX = MathHelper.Clamp(value.X, limits.X + (GameEnvironment.Screen.X / 2) / zoom, limits.X + limits.Width - (GameEnvironment.Screen.X) / zoom);
                posY = MathHelper.Clamp(value.Y, limits.Y + (GameEnvironment.Screen.Y / 2) / zoom, limits.Y + limits.Height - (GameEnvironment.Screen.Y) / zoom);
                origin = new Vector2(posX, posY);
            }
            else
            {
                origin = value;
            }
        }
    }

    public float Rotation
    {
        get { return rotation; }
        set { rotation = value; }
    }

    public Rectangle Bounds
    {
        get { return bounds; }
        set { bounds = value; }
    }

    public Rectangle Limits
    {
        get { return limits; }
        set { limits = value; }
    }

    public Matrix TransformMatrix
    { 
        get {
            return
                Matrix.CreateScale(Zoom) *
                Matrix.CreateTranslation(new Vector3(-Position, 0.0f)) *
                // Uncomment next line for parallax or something idk
                //Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) * 
                Matrix.CreateRotationZ(Rotation);
        }
    }
}
