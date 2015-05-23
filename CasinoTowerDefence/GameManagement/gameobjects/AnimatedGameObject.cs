using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class AnimatedGameObject : SpriteGameObject
{
    protected Dictionary<string,Animation> animations;

    public AnimatedGameObject(int layer = 0, string id = "")
        : base("", layer, id)
    {
        animations = new Dictionary<string, Animation>();
    }

    public void LoadAnimation(string assetname, string id, bool looping, 
                              float frametime = 0.1f)
    {
        Animation anim = new Animation(assetname, looping, frametime);
        animations[id] = anim;        
    }

    public void PlayAnimation(string id)
    {
        if (sprite == animations[id])
            return;
        if (sprite != null)
            animations[id].Mirror = sprite.Mirror;
        animations[id].Play();
        sprite = animations[id];
        origin = new Vector2(sprite.Width / 2, sprite.Height / 2);        
    }

    public override void Update(GameTime gameTime)
    {
        if (sprite == null)
            return;
        if(animations.Count> 0)
            Current.Update(gameTime);
        
        base.Update(gameTime);
    }

    public bool IsAnimationPlaying(string id)
    {
        return sprite == animations[id];
    }

    public Animation Current
    {
        get { return sprite as Animation; }
    }
}