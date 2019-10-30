using System;
using System.Diagnostics;
using System.Collections.Generic;
using Raylib;
using static Raylib.Raylib;

namespace ConsoleApp1
{
    class Game
    {
        readonly Stopwatch stopwatch = new Stopwatch();

        readonly SceneObject tankObject = new SceneObject();
        readonly SceneObject turretObject = new SceneObject();

        SceneObject boxObject = new SceneObject();
        SpriteObject boxSprite = new SpriteObject();

        readonly SpriteObject tankSprite = new SpriteObject();
        readonly SpriteObject turretSprite = new SpriteObject();

        private long currentTime = 0;
        private long lastTime = 0;
        private float timer = 0;
        private int fps = 1;
        private int frames;

        public float Tx = 0;
        public float Ty = 0;

        public List<SceneObject> bulletObjects = new List<SceneObject>();
        public List<SpriteObject> bulletSprites = new List<SpriteObject>();

        private float deltaTime = 0.005f;

        public void Init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;


            tankSprite.Load("tankBlue_outline.png");  //This loads the image of the tank's body
            tankSprite.SetRotate(-90 * (float)(Math.PI / 180.0f));  //This tells command to have ths image rotated by -90 degrees.
            tankSprite.SetPosition(-tankSprite.Width / 2.0f, tankSprite.Height / 2.0f); //This sets the tank's starting position to 
                                                                                        //it's sprite's width and height.

            tankSprite.corners[0].SetPosition(tankSprite.Width / 2 + 41, tankSprite.Height / 2 + 40);
            tankSprite.corners[1].SetPosition(-tankSprite.Width / 2 + 41, tankSprite.Height / 2 + 40);
            tankSprite.corners[2].SetPosition(tankSprite.Width / 2 + 41, -tankSprite.Height / 2 + 40);
            tankSprite.corners[3].SetPosition(-tankSprite.Width / 2 + 41, -tankSprite.Height / 2 + 40);


            turretSprite.Load("barrelBlue.png");    //This loads the image of the tank's barrel
            turretSprite.SetRotate(-90 * (float)(Math.PI / 180.0f));  //This tells command to have ths image rotated by -90 degrees.
            turretSprite.SetPosition(turretSprite.Width / 1.5f, turretSprite.Width / 2.0f);//This sets the tank's starting position to 
                                                                                           //it's sprite's width and height.

            turretSprite.corners[0].SetPosition(turretSprite.Width / 2, turretSprite.Height / 2);
            turretSprite.corners[1].SetPosition(-turretSprite.Width / 2 + 10, turretSprite.Height / 2 + 10);
            turretSprite.corners[2].SetPosition(turretSprite.Width / 2 + 8, -turretSprite.Height / 2 + 1);
            turretSprite.corners[3].SetPosition(-turretSprite.Width / 2 + 10, -turretSprite.Height / 2 + 10);


            boxSprite.SetPosition(boxSprite.Width / 2, boxSprite.Height / 2);

            boxSprite.corners[0].SetPosition(40, 40);
            boxSprite.corners[1].SetPosition(-40, 40);
            boxSprite.corners[2].SetPosition(40, -40);
            boxSprite.corners[3].SetPosition(-40, -40);


            tankObject.AddChild(tankSprite); //This makes "tankObject" become the parent of "tankSprite"
            tankObject.AddChild(turretObject); //This makes "tankObject" become the parent of "turretObject"
            turretObject.AddChild(turretSprite); //This makes "turretObject" become the parent of "tankSprite"
            boxObject.AddChild(boxSprite);

            boxObject.SetPosition(500,240);
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

            if (IsKeyPressed(KeyboardKey.KEY_SPACE)) //Key SpaceBar
            {
                SceneObject bulletObject = new SceneObject();
               
                SpriteObject bulletSprite = new SpriteObject();


                bulletSprites.Add(bulletSprite);
                bulletObject.AddChild(bulletSprite);
                turretObject.AddChild(bulletObject);

                bulletSprite.Load("bulletBlue.png");
                bulletSprite.SetRotate(90 * (float)(Math.PI / 180.0f));
                                                  
                bulletObject.SetPosition(55f, -6);

                bulletSprite.corners[0].SetPosition(bulletSprite.Width, bulletSprite.Height);
                bulletSprite.corners[1].SetPosition(-bulletSprite.Width + 9, bulletSprite.Height);
                bulletSprite.corners[2].SetPosition(bulletSprite.Width, -bulletSprite.Height + 25);
                bulletSprite.corners[3].SetPosition(-bulletSprite.Width + 10, -bulletSprite.Height + 25);

                tankObject.UpdateTransform();

                bulletObjects.Add(bulletObject);

                turretObject.RemoveChild(bulletObject);
                bulletObject.UpdateTransform();
            }
            tankObject.Update(deltaTime);

            boxObject.Update(deltaTime);

            for (int i = 0; i < bulletObjects.Count; i++)
            {
                Vector3 facing = new Vector3(bulletObjects[i].LocalTransform.m1, bulletObjects[i].LocalTransform.m2, 1) * deltaTime * 250;
                bulletObjects[i].Translate(facing.x, facing.y);
                bulletObjects[i].Update(deltaTime);
            }

           
            for (int i = 0; i < bulletSprites.Count; i++)
            {

                if (bulletSprites[i].boxCollider.Overlaps(boxSprite.boxCollider))
                {
                    boxSprite.color = Color.YELLOW;
                    
                    break;
                }
                else
                {
                    boxSprite.color = Color.BLUE;

                }
            }

            lastTime = currentTime;
        }

        public void Draw()
        {
            BeginDrawing(); //Begins the creation of sprites
            ClearBackground(Color.BROWN); //Colors the Background to a specified setting
            for(int i = 0; i < bulletSprites.Count; i++)
            {
                bulletSprites[i].Draw();
            }
            boxObject.Draw();
            tankObject.Draw(); //Creates and displays anything inside of "tankObject"
            DrawText(fps.ToString(), 10, 10, 12, Color.RED); //Creates visible text
            EndDrawing(); //Ends the creation of sprites
        }
    }
    
}
