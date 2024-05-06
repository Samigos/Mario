using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Map
{
    public static readonly int TILE_SIZE = 60;

    public readonly int[,] tiles = {
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 1, 0, 0, 0, 0, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 1, 0, 0, 2, 2, 1, 1, 1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
        { 1, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1 },
    };

    private List<Texture2D> _textures;
    public List<Rectangle?> _rectangles;

    public void Load()
    {
        _textures = new List<Texture2D> {
            Globals.Content.Load<Texture2D>("Images/tile1"),
            Globals.Content.Load<Texture2D>("Images/tile2")
        };

        SetUpRectangles();
    }

    public void SetUpRectangles()
    {
        _rectangles = new();

        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                int tileType = tiles[i, j] - 1;

                if (tileType >= 0)
                {
                    _rectangles.Add(
                        new Rectangle(
                            j * TILE_SIZE,
                            i * TILE_SIZE,
                            TILE_SIZE,
                            TILE_SIZE
                        )
                    );
                }
                else
                {
                    _rectangles.Add(null);
                }
            }
        }
    }

    public void Draw(int xOffset)
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                int tileType = tiles[i, j] - 1;

                if (tileType >= 0)
                {
                    Texture2D texture = _textures[tileType];
                    int index = (i * tiles.GetLength(1)) + j;

                    if (_rectangles[index] != null)
                    {
                        Rectangle rect = (Rectangle)_rectangles[index];
                        rect.X -= xOffset;

                        Globals.SpriteBatch.Draw(
                            texture,
                            rect,
                            Color.White
                        );
                    }
                }
            }
        }
    }
}