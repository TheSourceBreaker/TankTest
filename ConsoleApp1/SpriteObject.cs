using System;
using Raylib;
using static Raylib.Raylib;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class SpriteObject : SceneObject
    {
        public Texture2D texture = new Texture2D();
        public Image image = new Image();
        public List<SceneObject> corners = new List<SceneObject>();
        public AABB boxCollider = new AABB();
        public Color color = Color.GREEN;

        public float Width
        {
            get { return texture.width; }
        }
        public float Height
        {
            get { return texture.height; }
        }

        public SpriteObject()
        {
            corners.Add(new SceneObject());
            corners.Add(new SceneObject());
            corners.Add(new SceneObject());
            corners.Add(new SceneObject());

            corners[0].Translate(5, 5);
            corners[1].Translate(-5, 5);
            corners[2].Translate(5, -5);
            corners[3].Translate(-5, -5);

            AddChild(corners[0]);
            AddChild(corners[1]);
            AddChild(corners[2]);
            AddChild(corners[3]);
        }

        public override void OnUpdate(float deltaTime)
        {
            List<Vector3> cornerpos = new List<Vector3>();
            cornerpos.Add(corners[0].Position);
            cornerpos.Add(corners[1].Position);
            cornerpos.Add(corners[2].Position);
            cornerpos.Add(corners[3].Position);
            boxCollider.Fit(cornerpos);
        }

        public void Load(string filename)
        {
            Image img = LoadImage(filename);
            texture = LoadTextureFromImage(img);
        }
        public override void OnDraw()
        {
            float rotation = (float)Math.Atan2(globalTransform.m2, globalTransform.m1);

            DrawTextureEx(texture, new Vector2(globalTransform.m7, globalTransform.m8),
                rotation * (float)(180.0f / Math.PI), 1, Color.WHITE);

            DrawCircle((int)corners[0].GlobalTransform.m7, (int)corners[0].GlobalTransform.m8, 1, color);
            DrawCircle((int)corners[1].GlobalTransform.m7, (int)corners[1].GlobalTransform.m8, 1, color);
            DrawCircle((int)corners[2].GlobalTransform.m7, (int)corners[2].GlobalTransform.m8, 1, color);
            DrawCircle((int)corners[3].GlobalTransform.m7, (int)corners[3].GlobalTransform.m8, 1, color);


            
            boxCollider.OnDraw();
        }

    }
}
