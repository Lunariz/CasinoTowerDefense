using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class GameStateManager : IGameLoopObject
{
    Dictionary<string, IGameLoopObject> gameStates;
    IGameLoopObject currentGameState;

    public GameStateManager()
    {
        gameStates = new Dictionary<string, IGameLoopObject>();
        currentGameState = null;
    }

    public void AddGameState(string name, IGameLoopObject state)
    {
        gameStates[name] = state;
    }

    public IGameLoopObject GetGameState(string name)
    {
        return gameStates[name];
    }

    public void SwitchTo(string name)
    {
        if (gameStates.ContainsKey(name))
            currentGameState = gameStates[name];
        else
            throw new KeyNotFoundException("Could not find game state: " + name);
    }

    public IGameLoopObject CurrentGameState
    {
        get
        {
            return currentGameState;
        }
    }

    public void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.O))
        {
            GameEnvironment.DebugOn = !GameEnvironment.DebugOn;
        }
        if (currentGameState != null)
            currentGameState.HandleInput(inputHelper);
    }

    public void Update(GameTime gameTime)
    {
        if (currentGameState != null)
            currentGameState.Update(gameTime);
        //if(currentGameState != GetGameState("playingState") && currentGameState != GetGameState("gameOverState") && currentGameState != GetGameState("levelFinishedState"))
        //{
            GameEnvironment.Camera.Origin = new Vector2(GameEnvironment.Screen.X, GameEnvironment.Screen.Y) / 2;
        //}
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (currentGameState != null)
            currentGameState.Draw(gameTime, spriteBatch);
    }

    public void Reset()
    {
        if (currentGameState != null)
            currentGameState.Reset();
    }
}
