using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

public class AssetManager
{
    protected ContentManager contentManager;

    public AssetManager(ContentManager Content)
    {
        this.contentManager = Content;
    }

    public Texture2D GetSprite(string assetName)
    {
        if (assetName == "")
            return null;
        return contentManager.Load<Texture2D>(assetName);
    }

    public SpriteFont GetFont(string assetName)
    {
        if (assetName == "")
            return null;
        return contentManager.Load<SpriteFont>(assetName);
    }

    public Song GetMusic(string assetname)
    {
        return contentManager.Load<Song>(assetname);
    }

    public void PlaySound(string assetName)
    {
        SoundEffect snd = contentManager.Load<SoundEffect>(assetName);
        snd.Play();
    }

    public void PlayMusic(string assetName, bool repeat = true)
    {
        MediaPlayer.IsRepeating = repeat;
        MediaPlayer.Play(contentManager.Load<Song>(assetName));
    }

    public void PlayMusic(Song song, bool repeat = true)
    {
        MediaPlayer.IsRepeating = repeat;
        MediaPlayer.Play(song);
    }

    public bool IsMusicPlaying(float seconds)
    {
        return MediaPlayer.PlayPosition.Seconds < seconds;
    }

    public void StopMusic()
    {
        MediaPlayer.Stop();
    }

    public ContentManager Content
    {
        get { return contentManager; }
    }
}