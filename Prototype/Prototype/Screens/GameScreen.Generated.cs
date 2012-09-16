using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math.Geometry;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Input;
using FlatRedBall.IO;
using FlatRedBall.Instructions;
using FlatRedBall.Math.Splines;
using FlatRedBall.Utilities;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if XNA4
using Color = Microsoft.Xna.Framework.Color;
#else
using Color = Microsoft.Xna.Framework.Graphics.Color;
#endif

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Microsoft.Xna.Framework.Media;
#endif

// Generated Usings
using FlatRedBall.Broadcasting;
using Prototype.Entities;
using FlatRedBall;
using FlatRedBall.Math.Geometry;

namespace Prototype.Screens
{
	public partial class GameScreen : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		private Scene Scene;
		private ShapeCollection SceneBounds;
		
		private Prototype.Entities.Player PlayerInstance;

		public GameScreen()
			: base("GameScreen")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			if (!FlatRedBallServices.IsLoaded<FlatRedBall.Scene>(@"content/screens/gamescreen/scene.scnx", ContentManagerName))
			{
			}
			Scene = FlatRedBallServices.Load<FlatRedBall.Scene>(@"content/screens/gamescreen/scene.scnx", ContentManagerName);
			if (!FlatRedBallServices.IsLoaded<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/gamescreen/scenebounds.shcx", ContentManagerName))
			{
			}
			SceneBounds = FlatRedBallServices.Load<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/gamescreen/scenebounds.shcx", ContentManagerName);
			PlayerInstance = new Prototype.Entities.Player(ContentManagerName, false);
			PlayerInstance.Name = "PlayerInstance";
			
			
			PostInitialize();
			base.Initialize(addToManagers);
			if (addToManagers)
			{
				AddToManagers();
			}

        }
        
// Generated AddToManagers
		public override void AddToManagers ()
		{
			base.AddToManagers();
			AddToManagersBottomUp();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				PlayerInstance.Activity();
			}
			else
			{
			}
			base.Activity(firstTimeCalled);
			if (!IsActivityFinished)
			{
				CustomActivity(firstTimeCalled);
			}
			Scene.ManageAll();


				// After Custom Activity
				
            
		}

		public override void Destroy()
		{
			// Generated Destroy
			if (this.UnloadsContentManagerWhenDestroyed)
			{
				Scene.RemoveFromManagers(ContentManagerName != "Global");
			}
			else
			{
				Scene.RemoveFromManagers(false);
			}
			if (this.UnloadsContentManagerWhenDestroyed)
			{
				SceneBounds.RemoveFromManagers(ContentManagerName != "Global");
			}
			else
			{
				SceneBounds.RemoveFromManagers(false);
			}
			
			if (PlayerInstance != null)
			{
				PlayerInstance.Destroy();
				PlayerInstance.Detach();
			}

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			if (PlayerInstance.Parent == null)
			{
				PlayerInstance.X = -3.885193f;
			}
			else
			{
				PlayerInstance.RelativeX = -3.885193f;
			}
			if (PlayerInstance.Parent == null)
			{
				PlayerInstance.Y = 10.43984f;
			}
			else
			{
				PlayerInstance.RelativeY = 10.43984f;
			}
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp ()
		{
			Scene.AddToManagers(mLayer);
			SceneBounds.AddToManagers(mLayer);
			PlayerInstance.AddToManagers(mLayer);
			if (PlayerInstance.Parent == null)
			{
				PlayerInstance.X = -3.885193f;
			}
			else
			{
				PlayerInstance.RelativeX = -3.885193f;
			}
			if (PlayerInstance.Parent == null)
			{
				PlayerInstance.Y = 10.43984f;
			}
			else
			{
				PlayerInstance.RelativeY = 10.43984f;
			}
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			Scene.ConvertToManuallyUpdated();
			PlayerInstance.ConvertToManuallyUpdated();
		}
		public static void LoadStaticContent (string contentManagerName)
		{
			#if DEBUG
			if (contentManagerName == FlatRedBallServices.GlobalContentManager)
			{
				HasBeenLoadedWithGlobalContentManager = true;
			}
			else if (HasBeenLoadedWithGlobalContentManager)
			{
				throw new Exception("This type has been loaded with a Global content manager, then loaded with a non-global.  This can lead to a lot of bugs");
			}
			#endif
			Prototype.Entities.Player.LoadStaticContent(contentManagerName);
			CustomLoadStaticContent(contentManagerName);
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "Scene":
					return Scene;
				case  "SceneBounds":
					return SceneBounds;
			}
			return null;
		}


	}
}
