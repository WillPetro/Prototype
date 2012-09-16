using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;
using FlatRedBall.Content.SpriteRig;
using FlatRedBall.ManagedSpriteGroups;

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;


#endif

namespace Prototype.Entities
{
    public partial class Player
    {

        SpriteRigSave spriteRigSave;
        SpriteRig spriteRig;
        string poseChainName = "WalkRight";
        float secondsFromTheStart = 0.0f;
        public State playerState;




        private void CustomInitialize()
        {
            this.YAcceleration = -40;

            spriteRigSave = SpriteRigSave.FromFile("Content/Entities/Player/ballMan.srgx");
            spriteRig = spriteRigSave.ToSpriteRig("Global");
            SpriteManager.AddSpriteRig(spriteRig);

            spriteRig.SetPose(poseChainName, secondsFromTheStart);
            spriteRig.Animate = true;        
            spriteRig.AnimationSpeed = 3.0f;
            spriteRig.ScaleBy(0.3f, true);
            spriteRig.Cycle = true;
            spriteRig.Root.RelativeRotationZ = 90;
        }

        private void CustomActivity()
        {
            spriteRig.Manage();
            spriteRig.Root.Position = this.Position;

            switch (playerState)
            {
                case State.IDLE:
                    spriteRig.Transition("Idle", 0.2f);
                    break;

                case State.WALKING:
                    spriteRig.SetPoseChain("WalkRight");
                    break;
                case State.JUMPING:
                    spriteRig.Transition("Jump", 0.0f);
                    break;
            }
        }

        private void CustomDestroy()
        {


        }

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
    }
}
