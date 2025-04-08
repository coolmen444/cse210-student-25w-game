using Raylib_cs;
using System.Numerics;

public class Treasure : GameObject
{  
    private int _fallSpeed;
    private int _value;
    private Random _rand = new Random();
    private Texture2D _texture;
    
    public Treasure(int x, int y, int width, int height, Texture2D texture, int value) : base(x, y, width, height)
    {
        _fallSpeed= _rand.Next(2, 5);
        _value = value;
        _texture = texture;

    }

    public int GetFallSpeed()
    {
        return _fallSpeed;
    }

    public int GetScoreValue()
    {
        return _value + 1; // +1 so the value is not 0 
    }

     public override void Draw()
     {
        Raylib.DrawTexturePro(
        _texture,
        new Rectangle(0, 0, _texture.Width, _texture.Height),   // Source: use the entire texture
        new Rectangle(_x, _y, _width, _height),                    // Destination: draw it with the object's width & height
        new Vector2(0, 0),                                         // Origin: top-left corner (no offset)
        0f,                                                        // No rotation
        Color.White                                               // No tint
    );
     }
}