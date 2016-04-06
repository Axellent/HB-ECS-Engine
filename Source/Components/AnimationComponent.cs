using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace GameEngine
{
    public class AnimationComponent : IComponent
    {
        public struct Frame
        {
            public int StartX { set; get; }
            public int StartY { set; get; }
            public int EndX { set; get; }
            public int EndY { set; get; }
        }

        public double TimePerFrame { set; get; }
        public double CurrentElapsedTime { set; get; }

        private string currentAnimation;
        public string CurrentAnimation 
        {
            get
            {
                return currentAnimation;
            }
            set
            {
                currentAnimation = value;
                CurrentXFrame = animations[value].StartX;
                CurrentYFrame = animations[value].StartY;
            }
        }
        
        private Dictionary<string, Frame> animations = new Dictionary<string, Frame>();

        public int CurrentXFrame { get; set; }
        public int CurrentYFrame { get; set; }

        public int maxXFrames;
        public int maxYFrames;

        private Rectangle sourceRect = new Rectangle();

        public AnimationComponent(double timePerFrame, int animeationRectWidth, int animeationRectHeight, int textureWidth, int textureHeight)
        {
            TimePerFrame = timePerFrame;
            sourceRect.Width = animeationRectWidth;
            sourceRect.Height = animeationRectHeight;
            maxXFrames = textureWidth / animeationRectWidth;
            maxYFrames = textureHeight / animeationRectHeight;
        }

        public void AddAnimation(string name, int startX, int startY, int endX, int endY)
        {
            Frame frame = new Frame();
            frame.StartX = startX;
            frame.StartY = startY;
            frame.EndX = endX;
            frame.EndY = endY;
            animations[name] = frame;
        }

        public void RemoveAnimation(string name)
        {
            if(animations.ContainsKey(name))
            {
                animations.Remove(name);
            }
        }

        public Frame GetCurrentAnimation()
        {
            return animations[CurrentAnimation];
        }

        public Rectangle GetSourceRect()
        {
            return new Rectangle(CurrentXFrame * sourceRect.Width, CurrentYFrame * sourceRect.Height, sourceRect.Width, sourceRect.Height);
        }

        public int GetSourceWidth()
        {
            return sourceRect.Width;
        }

        public int GetSournceHeight()
        {
            return sourceRect.Height;
        }

    }
}
