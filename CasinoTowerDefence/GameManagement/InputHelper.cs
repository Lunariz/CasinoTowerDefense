using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

public class InputHelper
{
    protected MouseState currentMouseState, previousMouseState;
    protected KeyboardState currentKeyboardState, previousKeyboardState;
    protected Vector2 scale;

    //xboxcontrol
    protected GamePadState currentGamePadState, previousGamePadState;

    public InputHelper()
    {
        scale = Vector2.One;
    }

    public void Update()
    {
        previousMouseState = currentMouseState;
        previousKeyboardState = currentKeyboardState;

        currentMouseState = Mouse.GetState();
        currentKeyboardState = Keyboard.GetState();

        //xboxcontrol
        previousGamePadState = currentGamePadState;
        currentGamePadState = GamePad.GetState(PlayerIndex.One);
    }

    public Vector2 Scale
    {
        get { return scale; }
        set { scale = value; }
    }
    #region XBOX360 Controller
    public Vector2 LeftThumbStick()
    {
        Vector2 temp = currentGamePadState.ThumbSticks.Left;
        temp.Y *= -1;
        return temp;
    }

    public Vector2 RightThumbStick()
    {
        Vector2 temp = currentGamePadState.ThumbSticks.Right;
        temp.X *= -1f;
        return temp;
    }

    public bool IsControllerConnected()
    {
        if (currentGamePadState.IsConnected)
            return true;
        else
            return false;
    }

    public bool XboxAButtonPressed()
    {
        if (currentGamePadState.IsConnected)
        {
            //Debug.WriteLine("xbox controller aan");
            return currentGamePadState.Buttons.A == ButtonState.Pressed && previousGamePadState.Buttons.A == ButtonState.Released;
        }

        return false;
    }

    public bool XboxBButtonPressed()
    {
        if (currentGamePadState.IsConnected)
        {
            //Debug.WriteLine("xbox controller aan");
            return currentGamePadState.Buttons.B == ButtonState.Pressed && previousGamePadState.Buttons.B == ButtonState.Released;
        }

        return false;
    }

    public bool XboxXButtonPressed()
    {
        if (currentGamePadState.IsConnected)
        {
            //Debug.WriteLine("xbox controller aan");
            return currentGamePadState.Buttons.X == ButtonState.Pressed && previousGamePadState.Buttons.X == ButtonState.Released;
        }

        return false;
    }

    public bool XboxYButtonPressed()
    {
        if (currentGamePadState.IsConnected)
        {
            //Debug.WriteLine("xbox controller aan");
            return currentGamePadState.Buttons.Y == ButtonState.Pressed && previousGamePadState.Buttons.Y == ButtonState.Released;
        }

        return false;
    }

    public bool XboxDUpButtonPressed()
    {
        if (currentGamePadState.IsConnected)
        {
            //Debug.WriteLine("xbox controller aan");
            return currentGamePadState.DPad.Up == ButtonState.Pressed && previousGamePadState.DPad.Up == ButtonState.Released;
        }

        return false;
    }

    public bool XboxDDownButtonPressed()
    {
        if (currentGamePadState.IsConnected)
        {
            //Debug.WriteLine("xbox controller aan");
            return currentGamePadState.DPad.Down == ButtonState.Pressed && previousGamePadState.DPad.Down == ButtonState.Released;
        }

        return false;
    }

    public bool XboxDRightButtonPressed()
    {
        if (currentGamePadState.IsConnected)
        {
            //Debug.WriteLine("xbox controller aan");
            return currentGamePadState.DPad.Right == ButtonState.Pressed && previousGamePadState.DPad.Right == ButtonState.Released;
        }

        return false;
    }

    public bool XboxDLeftButtonPressed()
    {
        if (currentGamePadState.IsConnected)
        {
            //Debug.WriteLine("xbox controller aan");
            return currentGamePadState.DPad.Left == ButtonState.Pressed && previousGamePadState.DPad.Left == ButtonState.Released;
        }

        return false;
    }

    public bool XboxLeftShoulderButtonPressed()
    {
        if (currentGamePadState.IsConnected)
        {
            //Debug.WriteLine("xbox controller aan");
            return currentGamePadState.Buttons.LeftShoulder == ButtonState.Pressed && previousGamePadState.Buttons.LeftShoulder == ButtonState.Released;
        }

        return false;
    }

    public bool XboxRightShoulderButtonPressed()
    {
        if (currentGamePadState.IsConnected)
        {
            //Debug.WriteLine("xbox controller aan");
            return currentGamePadState.Buttons.RightShoulder == ButtonState.Pressed && previousGamePadState.Buttons.RightShoulder == ButtonState.Released;
        }

        return false;
    }

    public bool XboxStartButtonPressed()
    {
        if (currentGamePadState.IsConnected)
        {
            //Debug.WriteLine("xbox controller aan");
            return currentGamePadState.Buttons.Start == ButtonState.Pressed && previousGamePadState.Buttons.Start == ButtonState.Released;
        }

        return false;
    }

    public bool XboxBackButtonPressed()
    {
        if (currentGamePadState.IsConnected)
        {
            //Debug.WriteLine("xbox controller aan");
            return currentGamePadState.Buttons.Back == ButtonState.Pressed && previousGamePadState.Buttons.Back == ButtonState.Released;
        }

        return false;
    }

    public bool XboxBigButtonButtonPressed()
    {
        if (currentGamePadState.IsConnected)
        {
            //Debug.WriteLine("xbox controller aan");
            return currentGamePadState.Buttons.BigButton == ButtonState.Pressed && previousGamePadState.Buttons.BigButton == ButtonState.Released;
        }

        return false;
    }

    public bool RightTriggerPressed()
    {
        if(currentGamePadState.IsConnected)
        {
            if(currentGamePadState.Triggers.Right > 0.5f)
            {
                return true;
            }
        }

        return false;
    }

    public bool LeftTriggerPressed()
    {
        if (currentGamePadState.IsConnected)
        {
            if (currentGamePadState.Triggers.Left > 0.5f)
            {
                return true;
            }
        }

        return false;
    }

    public bool XboxAButtonDown()
    {
        return currentGamePadState.Buttons.A == ButtonState.Pressed;
    }
    #endregion

    #region Mouse
    public Vector2 MousePosition
    {
        get { return new Vector2(currentMouseState.X, currentMouseState.Y) / scale; }
    }

    #region Left Button
    public bool MouseLeftButtonPressed()
    {
        return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
    }

    public bool MouseLeftButtonDown()
    {
        return currentMouseState.LeftButton == ButtonState.Pressed;
    }
    
    public bool MouseLeftButtonReleased()
    {
        return currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed;
    }
    #endregion

    #region Right Button
    public bool MouseRightButtonPressed()
    {
        return currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released;
    }

    public bool MouseRightButtonDown()
    {
        return currentMouseState.RightButton == ButtonState.Pressed;
    }

    public bool MouseRightButtonReleased()
    {
        return currentMouseState.RightButton == ButtonState.Released && previousMouseState.RightButton == ButtonState.Pressed;
    }
    #endregion
    #endregion

    #region Keyboard
    public bool KeyPressed(Keys k)
    {
        return currentKeyboardState.IsKeyDown(k) && previousKeyboardState.IsKeyUp(k);
    }

    public bool IsKeyDown(Keys k)
    {
        return currentKeyboardState.IsKeyDown(k);
    }
    
    public bool KeyReleased(Keys k)
    {
        return currentKeyboardState.IsKeyUp(k) && previousKeyboardState.IsKeyDown(k);
    }

    public bool AnyKeyPressed
    {
        get { return currentKeyboardState.GetPressedKeys().Length > 0 && previousKeyboardState.GetPressedKeys().Length == 0; }
    }
    #endregion
}