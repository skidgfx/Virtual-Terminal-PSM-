
using System;
using Microsoft.Win32;
using Mono.Security;
using System.Net;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;

using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Input;


using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;


using Tutorial.Utility;


namespace AppData3088261ATSI
{
	public class intro
	{
		static protected GraphicsContext graphics;
		static Texture2D texture;
		static int runTime;
		static SimpleSprite spritePlayer;
		static ImageRect rectScreen;
		static Boolean running;
		static Boolean isBig;
		static float alpha = 1.0f;
		public intro()
		{
			Initialize ();
			isBig = false;
			runTime = 0;
			running = true;
			while (running) {
				SystemEvents.CheckEvents ();
				Update ();
				Render ();
			}
			graphics.Dispose();
			texture.Dispose();
		}
		

		public static void Initialize ()
		{
			graphics = new GraphicsContext();
			rectScreen = graphics.Screen.Rectangle;
			
			texture = new Texture2D("/Application/resources/logo.jpg", false);
			spritePlayer = new SimpleSprite(graphics, texture);
			spritePlayer.Position.X = rectScreen.Width/2.0f - 180.0f;
			spritePlayer.Position.Y = rectScreen.Height/2.0f - 200.0f;
			spritePlayer.Position.Z = 0.0f;
			
		}

		
		private static int CountNow = 0;
		public static void Update ()
		{
			runTime++;
			CountNow++;
			if(runTime >= 40){
				if(isBig)
				{
					spritePlayer.SetColor(1.0F,1.0F,1.0F,1.0F);
					isBig = false;
				}else
				{
					spritePlayer.SetColor(0.0F,0.0F,0.0F,1.0F);
					isBig = true;
				}
				runTime = 0 ;
			}
			if(CountNow >= 500)
				running = false;
			
		}

		public static void Render ()
		{
			graphics.Clear();

			spritePlayer.Render();
			graphics.SwapBuffers();	
		}
	}
}

