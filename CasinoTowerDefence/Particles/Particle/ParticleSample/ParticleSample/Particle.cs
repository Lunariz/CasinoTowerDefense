using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


public class Particle
{
    SpriteSheet particleTexture;

    public float TimeLived = 0;
    public float TimeToLive;

    public Vector2 Position;
    public Vector2 Velocity;

    public Color BeginColor;
    public Color EndColor;
    public Color Color;

    public float BeginSize;
    public float EndSize;
    public float Size;

    public byte BeginAlpha;
    public byte EndAlpha;
    public byte Alpha;

    public float BeginRotation;
    public float EndRotation;
    public float Rotation;

    public float Layer;

    public bool IsAlive
    { get { return TimeLived < TimeToLive; } }

    public float LifePhase
    { get { return TimeLived / TimeToLive; } }

    public Particle(Vector2 position, Vector2 velocity, string particleAssetName, Color beginColor, float beginSize)
    {
        this.Position = position;
        this.Velocity = velocity;
        this.particleTexture = new SpriteSheet(particleAssetName);

        BeginSize = beginSize;
        EndSize = 20;

        BeginColor = beginColor;
        EndColor = Color.Transparent;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        //particleTexture.Draw(spriteBatch, this.Position, Color, Layer, particleTexture.Center, 255, Size / 50, Size / 50, Rotation);
        Color.A = Alpha;
        spriteBatch.Draw(particleTexture.Sprite, new Rectangle((int)(Position.X - Size / 2), (int)(Position.Y - Size / 2), (int)Size, (int)Size), particleTexture.Sprite.Bounds, Color, Rotation, particleTexture.Center, SpriteEffects.None, Layer);
    }
}
