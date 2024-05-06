using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

class Globals
{
    public static GraphicsDeviceManager Graphics;
    public static SpriteBatch SpriteBatch;
    public static ContentManager Content;
    public static Vector2 WindowSize { get => new(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight); }
}