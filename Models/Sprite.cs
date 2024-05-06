using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Sprite
{
    public Vector2 Position;
    public Vector2 Size;
    protected Texture2D _texture;

    protected Sprite(Texture2D texture, Vector2 position, Vector2 size)
    {
        Position = position;
        Size = size;
        _texture = texture;
    }

    public Sprite(Vector2 position, Vector2 size)
    {
        Position = position;
        Size = size;


        Size = new(40, 62);
        Position = new(100, 222);
    }

    public virtual void Load()
    {

    }

    public virtual void Update(GameTime gameTime)
    {
    }

    public virtual void Draw()
    {
        Globals.SpriteBatch.Draw(_texture, new Rectangle(100, (int)Position.Y, (int)Size.X, (int)Size.Y), Color.White);
    }
}