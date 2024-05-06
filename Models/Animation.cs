using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class AnimatedSprite : Sprite
{
    private readonly int _frameRefreshRate;
    private readonly Dictionary<string, List<Rectangle>> _sourceRectangles;
    private Rectangle _sourceRectangle;
    private readonly Dictionary<string, Texture2D> _textures;
    private readonly int _numberOfHorizontalSprites;
    private string _currentSpritesheetPath;

    private float _fpsCounter = 0;
    private int _frameIndex = 0;
    private bool _isIdle = true;

    public AnimatedSprite(Vector2 position, Vector2 size, int frameRefreshRate, string[] spritesheetPaths, int numberOfHorizontalSprites) : base(position, size)
    {
        _frameRefreshRate = frameRefreshRate;
        _numberOfHorizontalSprites = numberOfHorizontalSprites;

        _textures = new();
        _sourceRectangles = new();

        for (int i = 0; i < spritesheetPaths.Length; i++)
        {
            Texture2D texture = Globals.Content.Load<Texture2D>(spritesheetPaths[i]);
            _textures[spritesheetPaths[i]] = texture;

            int spriteWidth = texture.Width / numberOfHorizontalSprites;
            _sourceRectangles[spritesheetPaths[i]] = new();

            for (int j = 0; j < numberOfHorizontalSprites; j++)
            {
                _sourceRectangles[spritesheetPaths[i]].Add(new(new Point(j * spriteWidth, 0), new Point(spriteWidth, texture.Height)));
            }
        }

        _currentSpritesheetPath = spritesheetPaths[0];
        SetTexture(_currentSpritesheetPath);
    }

    public override void Update(GameTime gameTime)
    {
        _fpsCounter++;

        if (_fpsCounter > _frameRefreshRate)
        {
            _fpsCounter = 0;
            _frameIndex++;

            if (_frameIndex > _sourceRectangles[_currentSpritesheetPath].Count - 1)
            {
                _frameIndex = 0;
            }
        }

        if (_isIdle)
        {
            _frameIndex = 0;
        }

        _sourceRectangle = _sourceRectangles[_currentSpritesheetPath][_frameIndex];
    }

    public override void Draw()
    {
        Globals.SpriteBatch.Draw(_texture, new Rectangle(100, (int)Position.Y, (int)Size.X, (int)Size.Y), _sourceRectangle, Color.White);
    }

    public void SetTexture(string spritesheetPath)
    {
        _currentSpritesheetPath = spritesheetPath;
        _texture = _textures[spritesheetPath];
    }

    public void SetIdleState(bool isIdle)
    {
        _isIdle = isIdle;
    }
}