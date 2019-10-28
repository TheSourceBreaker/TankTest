using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Raylib;
using static Raylib.Raylib;

namespace ConsoleApp1
{
    class Program
    {

        static void Main()
        {

            // Initialization


            Game game = new Game();

            InitWindow(640, 480, "Transformation Demonstration");

            SetTargetFPS(60);

            game.Init();


            while (!WindowShouldClose())    // Detect window close button or ESC key
            {
                game.Update();
                game.Draw();

            }

            game.Shutdown();

            CloseWindow(); // Close window and OpenGL context


        }

    }
}
