using Raylib_cs;

public class Bomb : GameObject
{
    private int _radius;    
    private int _fallSpeed;
    private Random _rand = new Random();

    public Bomb(int x, int y, int radius, Color color) : base(x, y, radius * 2, radius * 2, color)
    {
        _radius = radius;
        _fallSpeed= _rand.Next(2, 7);
    }

    public override void Draw()
    {
       Raylib.DrawCircle(_x, _y, _radius, _color);
    }

    public int GetFallSpeed()
    {
        return _fallSpeed;
    }

    public override Rectangle GetRectangle()
    {
        // This helps re-center the collision box
        return new Rectangle(_x - _radius, _y - _radius, _radius * 2, _radius * 2);
    }
}