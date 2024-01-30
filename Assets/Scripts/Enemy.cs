using System;
using System.Numerics;
using System.Threading;

class Enemy
{
    private Vector2 position;
    private Vector2 velocity;
    private int stopTime;  // 5 sekunders stopp
    private DateTime startTime;

    public Enemy(float x, float y, float speed)
    {
        position = new Vector2(x, y);
        velocity = new Vector2(speed, 0);
        stopTime = 5;
        startTime = DateTime.Now;
    }

    public void Move()
    {
        // R�r fienden i den aktuella riktningen
        position += velocity;

        // Kolla om det �r dags att stanna
        if ((DateTime.Now - startTime).TotalSeconds >= stopTime)
        {
            // V�lj en ny riktning och �terst�ll starttiden
            velocity.X = -velocity.X;  // Invertera riktningen
            startTime = DateTime.Now;
        }
    }

    public Vector2 GetPosition()
    {
        return position;
    }
}

class Program
{
    static void Main()
    {
        // Initialisera fienden
        Enemy enemy = new Enemy(x: 50, y: 10, speed: 5);

        // Spel-loop
        while (true)
        {
            // Uppdatera fiendens position
            enemy.Move();

            // Rensa konsolen
            Console.Clear();

            // Rita fienden
            Console.SetCursorPosition((int)enemy.GetPosition().X, (int)enemy.GetPosition().Y);
            Console.Write("E");

            // S�tt en kort f�rdr�jning
            Thread.Sleep(100);
        }
    }
}
