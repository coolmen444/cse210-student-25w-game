using Raylib_cs;

public class Player : GameObject
{

    private int _lives = 3;

    public Player(int x, int y, int width, int height, Color color) : base(x, y, width, height, color)
    {

    }
    
    public override void Draw()
    {
        Raylib.DrawRectangle(_x, _y, _width, _height, _color);
    }

    public override void Move(int x, int y)
    {
        _x += x;
        _y += y;

        if (_x < 0)
        {
            _x = 0;
        }
        

        if (_x + _width > GameManager.SCREEN_WIDTH)
        {
            _x = GameManager.SCREEN_WIDTH - _width;
        }
    }

    public int GetLives()
    {
        return _lives;
    }

    public void RemoveLife()
    {
        _lives -= 1;
    }

    public bool IsDead()
    {
        return _lives <= 0;
    }
}