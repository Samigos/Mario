using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Sprite
{
    public Vector2 Position;
    public Vector2 Size;
    protected Texture2D _texture;
    public Vector2 _velocity = Vector2.Zero;
    protected int _gravity = 45;
    protected bool IsGravityEnabled = false;
    protected bool _isOnGround = false;

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

    #region Inheritable Methods
    protected void ApplyGravity(GameTime gameTime)
    {
        if (!IsGravityEnabled) return;

        if (_isOnGround)
        {
            _velocity.Y = 0;
            return;
        }

        _velocity.Y += _gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    #endregion
}