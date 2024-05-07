using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

class Player : AnimatedSprite
{

    private bool isIdle = true;

    private readonly int _speed = 300;
    private readonly int _jumpSpeed = 1000;

    public Direction direction = Direction.RIGHT;

    public Player(Vector2 position, Vector2 size, int frameRefreshRate, string[] spritesheetPaths, int numberOfHorizontalSprites) : base(position, size, frameRefreshRate, spritesheetPaths, numberOfHorizontalSprites)
    {
        IsGravityEnabled = true;
    }

    public override void Load()
    {
    }

    public void Update(GameTime gameTime, Map map)
    {
        base.Update(gameTime);

        ApplyGravity(gameTime);
        ApplyInputMovement(gameTime);
        ApplyCollisions(map);
    }

    public override void Draw()
    {
        base.Draw();
    }

    private void ApplyCollisions(Map map)
    {
        Vector2 newPosition = Position + _velocity;
        _isOnGround = false;

        foreach (Rectangle rect in map._rectangles.Where(obj => obj?.Intersects(new Rectangle((int)newPosition.X, (int)newPosition.Y, (int)Size.X, (int)Size.Y)) == true))
        {
            Rectangle intersection = Rectangle.Intersect(new Rectangle((int)newPosition.X, (int)newPosition.Y, (int)Size.X, (int)Size.Y), rect);

            if (intersection.Width < intersection.Height)
            {
                if (newPosition.X < rect.X)
                    newPosition.X -= intersection.Width;
                else
                    newPosition.X += intersection.Width;
            }
            else // Vertical collision
            {
                if (newPosition.Y > 0) // Falling
                {
                    newPosition.Y -= intersection.Height;
                    _isOnGround = true;
                }

                if (newPosition.Y < 0) // Jumping up
                {
                    newPosition.Y += intersection.Height;
                }

                break;
            }
        }

        Position = newPosition;
    }

    private void ApplyInputMovement(GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();
        isIdle = false;

        if (keyboardState.IsKeyDown(Keys.D))
        {
            _velocity.X = _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            direction = Direction.RIGHT;
            SetTexture("Images/Sprites/player_spritesheet_right");
        }
        else if (keyboardState.IsKeyDown(Keys.A))
        {
            _velocity.X = -_speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            direction = Direction.LEFT;
            SetTexture("Images/Sprites/player_spritesheet_left");
        }
        else
        {
            _velocity.X = 0;
            isIdle = true;
        }

        SetIdleState(isIdle);

        if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Space))
        {
            if (_isOnGround)
            {
                _velocity.Y = -_jumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                _isOnGround = false;
            }
        }
    }
}