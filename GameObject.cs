using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

public abstract class GameObject
{
    protected int _x;
    protected int _y;
    protected int _width;
    protected int _height;
    protected Color _color;

    public GameObject(int x, int y, int width, int height, Color color)
    {
        _x = x;
        _y = y;
        _width = width;
        _height = height;
        _color = color;
    }   

    public GameObject(int x, int y, int width, int height)
    {
        _x = x;
        _y = y;
        _width = width;
        _height = height;
    }

    public virtual Rectangle GetRectangle()
    {
        return new Rectangle(_x, _y, _width, _height);
    }

    public abstract void Draw();

    public virtual void Move(int x, int y)
    {
        _x += x;
        _y += y;
    }

    public int GetX()
    {
        return _x;
    }

    public int GetY()
    {
        return _y;
    }
}