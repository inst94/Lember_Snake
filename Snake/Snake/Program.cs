using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.SetBufferSize(120, 120);

            Walls walls = new Walls(80, 25);
            walls.Draw();

            Point p = new Point(1, 3, '*');
            /*p1.x = 1;
            p1.y = 3;
            p1.sym = '*';*/
            Snake snake = new Snake(p, 4, Direction.RIGHT);
            snake.Draw();

            Score score = new Score(75, 1);
            score.UpdateScore();

            FoodCreator foodCreator = new FoodCreator(70, 25, '$');
            Point food = foodCreator.CreateFood();
            food.Draw(ConsoleColor.Green);

            FoodCreator badfoodCreator = new FoodCreator(50, 10, '#');
            Point badfood = badfoodCreator.CreateFood();
            badfood.Draw(ConsoleColor.Red);

            System.Media.SoundPlayer eat_good = new System.Media.SoundPlayer("..//..//sounds/good.wav");
            System.Media.SoundPlayer eat_bad = new System.Media.SoundPlayer("..//..//sounds/bad.wav");


            while (true)
            {
                if (walls.IsHit(snake) || snake.IsHitTail())
                {
                    break;
                }
                if (snake.Eat(food))
                {
                    Score.score += 1;
                    score.UpdateScore();
                    food = foodCreator.CreateFood();
                    food.Draw(ConsoleColor.Green);
                    eat_good.Play();
                }
                if (snake.NOEat(badfood))
                {
                    Score.score -= 1;
                    score.UpdateScore();
                    badfood = badfoodCreator.CreateFood();
                    badfood.Draw(ConsoleColor.Red);
                    eat_bad.Play();
                }
                if(Score.score==-1)
                {
                    Console.Clear();
                    Console.SetCursorPosition(50, 10);
                    Console.Write("Game Over");
                }
                else
                {
                    snake.Move();
                }
                Thread.Sleep(100);
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.HandleKey(key.Key);
                }
            }
        }
    }
}
