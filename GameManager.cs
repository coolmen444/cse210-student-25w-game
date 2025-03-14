using Raylib_cs;

class GameManager
{
    private int _width;
    private int _height;
    private string _title;

    public GameManager()
    {
        _width = 800;
        _height = 480;
        _title = "CSE 210 Game";
    }

    public void Run()
    {
        InitializeGame();

        Raylib.InitWindow(_width, _height, _title);

        while (!Raylib.WindowShouldClose())
        {
            HandleInput();
            ProcessActions();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            DrawElements();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    private void InitializeGame()
    {

    }

    private void HandleInput()
    {

    }

    private void ProcessActions()
    {

    }

    private void DrawElements()
    {

    }
}