//#define FLYING

using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.ManagedSpriteGroups;
using FlatRedBall.Content.SpriteRig;

using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Graphics.Model;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using FlatRedBall.Math;

using Prototype.Entities;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using FlatRedBall.Localization;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Microsoft.Xna.Framework;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

namespace Prototype.Screens
{
	public partial class GameScreen
	{
        PositionedObjectList<Entities.Bullet> bullets = new PositionedObjectList<Bullet>(100);

		void CustomInitialize()
		{

            SpriteManager.Camera.X = PlayerInstance.X;
            SpriteManager.Camera.Y = PlayerInstance.Y;
            SceneBounds.Visible = false;

		}

		void CustomActivity(bool firstTimeCalled)
		{

            ControlActivity();
            if (true)
            {
                SpriteManager.Camera.AttachTo(PlayerInstance, true);
            }
            PlayerInstance.Collision.CollideAgainstBounce(SceneBounds, 0, 1, 0);

            foreach (Bullet bullet in bullets)
            {

                Random rand = new Random();

                bullet.Body.CollideAgainstBounce(SceneBounds, 0, 1, 0.1f);
                bullet.Body.CollideAgainst(PlayerInstance.Collision);

                bullet.Body.Drag = 20;
                bullet.Sprite.RotationZ = bullet.Body.RotationZ;

                if(Vector3.Distance(bullet.Position, PlayerInstance.Position) >= 100)
                {
                    bullet.Destroy();
                }
                if (bullets.Count >= 100)
                {
                    bullets[1].Destroy();
                }

            }

		}

		void CustomDestroy()
		{
		}

        static void CustomLoadStaticContent(string contentManagerName)
        {

        }

        private bool Grounded()
        {
            // Assumes that PlayerInstance is an Entity that has a Collision shape property and
            // that LevelCollision is a valid ShapeCollection
            if (PlayerInstance.Collision.CollideAgainstBounce(SceneBounds, 0, 1, 0))
            {
                // There has been a collision
                bool wasRepositionedUpward = PlayerInstance.Collision.LastMoveCollisionReposition.Y > 0;

                // If the PlayerInstance was repositioned upward, then that means it was on a surface
                PlayerInstance.IsOnGround = wasRepositionedUpward;
            }
            else
            {
                // Since no collision occurred, the player isn't on the ground
                PlayerInstance.IsOnGround = false;
            }
            return PlayerInstance.IsOnGround;
        }

        private bool BulletGrounded(Bullet bullet)
        {
            // Assumes that PlayerInstance is an Entity that has a Collision shape property and
            // that LevelCollision is a valid ShapeCollection
            if (bullet.Body.CollideAgainstBounce(SceneBounds, 0, 1, 0))
            {
                // There has been a collision
                bool wasRepositionedUpward = bullet.Body.LastMoveCollisionReposition.Y > 0;

                // If the PlayerInstance was repositioned upward, then that means it was on a surface
                bullet.IsOnGround = wasRepositionedUpward;
            }
            else
            {
                // Since no collision occurred, the player isn't on the ground
                bullet.IsOnGround = false;
            }
            return bullet.IsOnGround;
        }

        public void ControlActivity()
        {

#if FLYING
            if (InputManager.Keyboard.KeyDown(Keys.Space))
            {
                PlayerInstance.YVelocity = 20;

            }
#endif
            if (InputManager.Keyboard.KeyDown(Keys.Space) && Grounded())
            {
                PlayerInstance.YVelocity = 20;
                PlayerInstance.playerState = State.JUMPING;
            }   

            if (InputManager.Keyboard.KeyDown(Keys.D))
            {
                PlayerInstance.XVelocity = 10;
                PlayerInstance.playerState = State.WALKING;
            }
            else if (InputManager.Keyboard.KeyDown(Keys.A))
            {
                PlayerInstance.XVelocity = -10;
                PlayerInstance.playerState = State.WALKING;
            }
            else
            {
                PlayerInstance.XVelocity = 0;
                PlayerInstance.playerState = State.IDLE;
            }


            if (InputManager.Mouse.ButtonPushed(Mouse.MouseButtons.LeftButton) && InputManager.Mouse.IsInGameWindow())
            {
                Bullet bullet = new Bullet(this.ContentManagerName);
                Random rand = new Random();
                bullet.X = PlayerInstance.X + 2 + rand.Next(2);
                bullet.Y = PlayerInstance.Y - 0.5f;
                bullet.XVelocity = 50 + rand.Next(10);
                bullet.RotationZ = rand.Next(360);

                bullets.Add(bullet);
            }



        }
	}
}
