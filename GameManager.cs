using Raylib_cs;

class GameManager
{
    public const int SCREEN_WIDTH = 800;
    public const int SCREEN_HEIGHT = 600;

    private string _title;
    private List<GameObject> _gameObjects = new List<GameObject>();
    private Player _player;
    private int _score = 0;
    private int _bombSpawnTimer = 0;
    private int _treasureSpawnTimer = 0;
    private int _bombSpawnInterval = 180;
    private int _treasureSpawnInterval = 120;
    private bool _gameOver = false;
    private Random _rand = new Random();
    private Texture2D[] _treasureTextures;

    public GameManager()
    {
        _title = "CSE 210 Game";
        _player = null!;
        _treasureTextures = null!;
    }

    /// <summary>
    /// The overall loop that controls the game. It calls functions to
    /// handle interactions, update game elements, and draw the screen.
    /// </summary>
    public void Run()
    {
        Raylib.SetTargetFPS(60);
        Raylib.InitWindow(SCREEN_WIDTH, SCREEN_HEIGHT, _title);
        // If using sound, un-comment the lines to init and close the audio device
        // Raylib.InitAudioDevice();

        InitializeGame();

        while (!Raylib.WindowShouldClose())
        {

            if (!_gameOver) 
            {
                HandleInput();
                ProcessActions();
            }

            if (_gameOver)
            {
                DrawGameOver();
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            DrawElements();

            Raylib.EndDrawing();
        }

        // Raylib.CloseAudioDevice();
        Raylib.CloseWindow();
    }

    /// <summary>
    /// Sets up the initial conditions for the game.
    /// </summary>
    private void InitializeGame()
    {
        Console.WriteLine("Working Directory: " + Directory.GetCurrentDirectory());
        _player = new Player(0, SCREEN_HEIGHT - 10, 50, 10, Color.Blue);
        _gameObjects.Add(_player);

        _treasureTextures = new Texture2D[]
            {
                Raylib.LoadTexture("../../../images/citrine.png"),
                Raylib.LoadTexture("../../../images/amethyst.png"),
                Raylib.LoadTexture("../../../images/sapphire.png"),
                Raylib.LoadTexture("../../../images/ruby.png"),
                Raylib.LoadTexture("../../../images/emerald.png"),
                Raylib.LoadTexture("../../../images/gold.png")
            };
    }

    /// <summary>
    /// Responds to any input from the user.
    /// </summary>
    private void HandleInput()
    {
        if (Raylib.IsKeyDown(KeyboardKey.Right))
        {
            _player.Move(5, 0);
        }
        if (Raylib.IsKeyDown(KeyboardKey.Left))
        {
            _player.Move(-5, 0);
        }
    }

    /// <summary>
    /// Processes any actions such as moving objects or handling collisions.
    /// </summary>
    private void ProcessActions()
    {
        DrawScore();
        DrawLives();
        SpawnTreasureIfNeeded();
        SpawnBombIfNeeded();
        MoveObjects();
        CleanElements();
        ProcessCollisions();
        CheckGameOver();
    }

    /// <summary>
    /// Draws all elements on the screen.
    /// </summary>
    private void DrawElements()
    {
        foreach (GameObject item in _gameObjects)
        {
            item.Draw();
        }
    }

    private void CleanElements()
    {
        for (int i = _gameObjects.Count - 1; i >= 0; i--)
        {
            if (_gameObjects[i] is Bomb bomb && bomb.GetY() > SCREEN_HEIGHT)
            {
                _gameObjects.RemoveAt(i);
            }

            else if (_gameObjects[i] is Treasure treasure && treasure.GetY() > SCREEN_HEIGHT)
            {
                _gameObjects.RemoveAt(i);
            }
        }
    }

    private void ProcessCollisions()
    {
        for (int i = _gameObjects.Count - 1; i >= 0; i--)
        {
            GameObject obj = _gameObjects[i];

            Rectangle objRec = obj.GetRectangle();
            Rectangle playerRec = _player.GetRectangle();

            if (Raylib.CheckCollisionRecs(playerRec, objRec))
            {
                if (obj is Bomb)
                {
                    _gameObjects.RemoveAt(i);
                    _player.RemoveLife();
                    

                }
                else if (obj is Treasure treasure)
                {
                    _gameObjects.RemoveAt(i);
                    _score += treasure.GetScoreValue();

                }
            }
        }
    }

    private void MoveObjects()
    {
        foreach (GameObject obj in _gameObjects)
        {
            if (obj is Bomb bomb)
            {
                bomb.Move(0, bomb.GetFallSpeed());
            }
            if (obj is Treasure treasure)
            {
                treasure.Move(0, treasure.GetFallSpeed());
            }
        }
    }

    private void SpawnBombIfNeeded()
    {
        _bombSpawnTimer++;

         if (_bombSpawnTimer >= _bombSpawnInterval)
        {
            int randomX = _rand.Next(0, SCREEN_WIDTH);

            Bomb bomb = new Bomb(randomX, 0, 9, Color.Red);
            _gameObjects.Add(bomb);

            _bombSpawnTimer = 0;
            _bombSpawnInterval--;
            _treasureSpawnInterval--;
        }

    }

    private void SpawnTreasureIfNeeded()
    {
        _treasureSpawnTimer++;

        if (_treasureSpawnTimer >= _treasureSpawnInterval)
        {
            int randomX = _rand.Next(0, SCREEN_WIDTH);

            int value = _rand.Next(0, 5);
            Texture2D texture;

            switch (value)
            {
                case 0:
                    texture = _treasureTextures[0];
                    break;
                case 1:
                    texture = _treasureTextures[1];
                    break;
                case 2:
                    texture = _treasureTextures[2];
                    break;
                case 3:
                    texture = _treasureTextures[3];
                    break;
                case 4:
                    texture = _treasureTextures[4];
                    break;
                case 5:
                    texture = _treasureTextures[5];
                    break;
                default:
                    texture = _treasureTextures[0];
                break;

            }

            Treasure treasure = new Treasure(randomX, 0, 20, 40, texture, value);
            _gameObjects.Add(treasure);

            _treasureSpawnTimer = 0;
        }
    }

    private void CheckGameOver()
    {
        if (_player.IsDead())
        {
            _gameOver = true;

        }
    }

    private void DrawScore()
    {
    Raylib.DrawText($"Score: {_score}", 5, 5, 30, Color.Black);
    }

    private void DrawLives()
    {
    Raylib.DrawText($"Lives: {_player.GetLives()}", SCREEN_WIDTH - 120, 5, 30, Color.Black);
    }

    private void DrawGameOver()
    {
        Raylib.DrawText("GAME OVER", SCREEN_WIDTH / 2 - 300, SCREEN_HEIGHT / 2 - 50, 100, Color.Red);
        Raylib.DrawText($"Score: {_score}", SCREEN_WIDTH / 2 - 90, SCREEN_HEIGHT / 2 - 100, 50, Color.Black);
    }

}