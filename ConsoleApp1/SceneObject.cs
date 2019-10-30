using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ConsoleApp1
{
    class SceneObject
    {
        protected SceneObject parent = null;
        protected List<SceneObject> children = new List<SceneObject>();

        protected Matrix3 localTransform = new Matrix3();
        protected Matrix3 globalTransform = new Matrix3();

        public Matrix3 LocalTransform
        {
            get { return localTransform; }
        }

        public Matrix3 GlobalTransform
        {
            get { return globalTransform; }
        }

        public Vector3 Position
        {
            get
            {
                return new Vector3(globalTransform.m7, globalTransform.m8, 1);
            }
        }

        public SceneObject()
        { }

        public SceneObject Parent
        {
            get { return parent; }  //Helper Method: Another to return the parent.
        }

        public int GetChildCount()
        {
            return children.Count;  //Helper Method: One to query how many children an object has.
        }

        public SceneObject GetChild(int index)
        {
            return children[index];  //Helper Method:  One to access a certain child by an index.
        }

        public void AddChild(SceneObject child)
        {
            Debug.Assert(child.parent == null);
            child.parent = this;
            children.Add(child);
        }

        public void RemoveChild(SceneObject child)
        {
            if (children.Remove(child) == true)
            {
                child.localTransform.Set(child.globalTransform);
                child.parent = null;
            }
        }

        ~SceneObject()
        {
            if (parent != null)
            {
                parent.RemoveChild(this);
            }
            foreach (SceneObject so in children)
            {
                so.parent = null;
            }
        }

        public virtual void OnUpdate(float deltaTime)
        {

        }

        public virtual void OnDraw()
        {

        }

        public void Update(float deltaTime)
        {
            OnUpdate(deltaTime);

            foreach (SceneObject child in children)
            {
                child.Update(deltaTime);
            }

        }

        public void Draw()
        {
            OnDraw();

            foreach (SceneObject child in children)
            {
                child.Draw();
            }

        }



        public void UpdateTransform()
        {
            if (parent != null)
                globalTransform = parent.globalTransform * localTransform;

            else
                globalTransform = localTransform;

            foreach (SceneObject child in children)
                child.UpdateTransform();
        }

        public void SetPosition(float x, float y)
        {
            localTransform.SetTranslation(x, y);

            UpdateTransform();
        }

        public void SetRotate(float radians)
        {
            localTransform.SetRotateZ(radians);

            UpdateTransform();
        }

        public void SetScale(float width, float height)
        {
            localTransform.SetScaled(width, height, 1);

            UpdateTransform();
        }

        public void Translate(float x, float y)
        {
            localTransform.Translate(x, y);

            UpdateTransform();
        }

        public void Rotate(float radians)
        {
            localTransform.RotateZ(radians);

            UpdateTransform();
        }

        public void Scale(float width, float height)
        {
            localTransform.Scale(width, height, 1);

            UpdateTransform();
        }

    }
}
