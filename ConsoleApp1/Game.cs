using System;
using System.Diagnostics;
using Raylib;
using static Raylib.Raylib;
namespace ConsoleApp1
{
    class Game
    {
        readonly Stopwatch stopwatch = new Stopwatch();

        readonly SceneObject tankObject = new SceneObject();
        readonly SceneObject turretObject = new SceneObject();
        //SceneObject RootObject = new SceneObject();
        readonly SceneObject bulletObject = new SceneObject();

        readonly SpriteObject tankSprite = new SpriteObject();
        readonly SpriteObject turretSprite = new SpriteObject();
        readonly SpriteObject bulletSprite = new SpriteObject();

        private long currentTime = 0;
        private long lastTime = 0;
        private float timer = 0;
        private int fps = 1;
        private int frames;

        public float Tx = 0;
        public float Ty = 0;


        private float deltaTime = 0.005f;

        public void Init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;


            tankSprite.Load("tankBlue_outline.png");  //This loads the image of the tank's body
            tankSprite.SetRotate(-90 * (float)(Math.PI / 180.0f));  //This tells command to have ths image rotated by -90 degrees.
            tankSprite.SetPosition(-tankSprite.Width / 2.0f, tankSprite.Height / 2.0f); //This sets the tank's starting position to 
                                                                                        //it's sprite's width and height.


            turretSprite.Load("barrelBlue.png");    //This loads the image of the tank's barrel
            turretSprite.SetRotate(-90 * (float)(Math.PI / 180.0f));  //This tells command to have ths image rotated by -90 degrees.
            turretSprite.SetPosition(turretSprite.Width / 1.5f, turretSprite.Width / 2.0f);//This sets the tank's starting position to 
                                                                                           //it's sprite's width and height.

            bulletSprite.Load("bulletBlue.png");
            bulletSprite.SetRotate(90 * (float)(Math.PI / 180.0f));
            //bulletSprite.SetPosition(turretSprite.Width + Tx, turretSprite.Height + Ty);

            tankObject.AddChild(tankSprite); //This makes "tankObject" become the parent of "tankSprite"
            tankObject.AddChild(turretObject); //This makes "tankObject" become the parent of "turretObject"
            turretObject.AddChild(turretSprite); //This makes "turretObject" become the parent of "tankSprite"


            bulletObject.AddChild(bulletSprite); //This makes "bulletObject" become the parent of "bulletSprite"


            tankObject.SetPosition(GetScreenWidth() / 2.0f, GetScreenHeight() / 2.0f); //This sets the tankObject's position to the 
                                                                                       //Screen's Width and Height.
        }

        public void Shutdown() //Used to shutdown the test when called.
        { }

        public void Update() //This is constantly Updating.
        {
            currentTime = stopwatch.ElapsedMilliseconds;
            deltaTime = (currentTime - lastTime) / 1000.0f;

            timer += deltaTime;
            if (timer >= 1)
            {
                fps = frames;
                frames = 0;
                timer -= 1;
            }
            frames++;

            if (IsKeyDown(KeyboardKey.KEY_A))   //Key A
            {
                tankObject.Rotate(-deltaTime);
            }

            if (IsKeyDown(KeyboardKey.KEY_D))   //Key D
            {
                tankObject.Rotate(deltaTime);
            }

            if (IsKeyDown(KeyboardKey.KEY_W))   //Key W
            {
                Vector3 facing = new Vector3(tankObject.GlobalTransform.m1, tankObject.GlobalTransform.m2, 1) * deltaTime * 100;

                tankObject.Translate(facing.x, facing.y);
            }

            if (IsKeyDown(KeyboardKey.KEY_S))   //Key S
            {
                Vector3 facing = new Vector3(tankObject.GlobalTransform.m1, tankObject.GlobalTransform.m2, 1) * deltaTime * -100;

                tankObject.Translate(facing.x, facing.y);
            }

            if (IsKeyDown(KeyboardKey.KEY_Q))   //Key Q
            {
                turretObject.Rotate(-deltaTime);
            }

            if (IsKeyDown(KeyboardKey.KEY_E))   //Key E
            {
                turretObject.Rotate(deltaTime);
            }

            if (IsKeyDown(KeyboardKey.KEY_SPACE)) //Key SpaceBar
            {
                Vector3 facing = new Vector3(turretObject.LocalTransform.m1, turretObject.LocalTransform.m2, 1) * deltaTime * 100;
                // This creates a Vector3 who's "A" is equal to turretObject's localtransform of m1 multiplied by deltaTime and Speed,
                // "B" is equal to turretObject's localtransform of m2 multiplied by deltaTime and Speed,
                // and "C" is equal to 1 multiplied by deltaTime and Speed.


                //bulletObject.SetPosition(0, 0);

                Tx = bulletObject.LocalTransform.m7 + bulletObject.LocalTransform.m1;
                                                                                                // This sets "Tx" to equal the bulletObject's localTransform of m7
                                                                                                // plus the bulletObject's localTransform of m1.
                Ty = bulletObject.LocalTransform.m8 + bulletObject.LocalTransform.m2;
                                                                                                // This sets "Ty" to equal the bulletObject's localTransform of m8
                                                                                                // plus the bulletObject's localTransform of m2.

                bulletObject.Translate(facing.x, facing.y); //Translates "bulletObject" towards the new Vector3's points for both of its X and Y.

                turretObject.AddChild(bulletObject); //This makes "turretObject" the parent of "bulletObject"

                bulletObject.SetPosition(Tx, Ty); //Set's bulletObject's Position to (0,0)

                turretObject.RemoveChild(bulletObject); //Removes Child "bulletObject" from its Parent "turretObject"

                bulletObject.UpdateTransform(); //Initiates UpdateTransform


                bulletObject.Draw(); //Initiates Draw
            }

            tankObject.Update(deltaTime);
            lastTime = currentTime;
        }

        public void Draw()
        {
            BeginDrawing(); //Begins the creation of sprites
            ClearBackground(Color.LIGHTGRAY); //Colors the Background to a specified setting
            DrawText(fps.ToString(), 10, 10, 12, Color.RED); //Creates visible text
            tankObject.Draw(); //Creates and displays anything inside of "tankObject"
            EndDrawing(); //Ends the creation of sprites
        }
    }
    
}
