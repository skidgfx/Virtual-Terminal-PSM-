/*	Virtual Terminal - Main Operations
 * This software voids most OOP and Encapsulation, however is a working product complicated 
 * of cracking because it is sub-methods to be called from the launch. 
 * 
 * 	App info:
 * Release: Jun 4, 2014 01:41 am UTC
 * Publisher: Self, Nathan Pillman aka "GTAWWEKID"
 * Platform: PlayStation®Mobile SDK 1.21.01
 * Runtime: 1.21
 * 
 * Included Libraries owned by respective owners:
 * -Microsoft CORPORATION
 * -Sony Entertainment America Inc.
 * 
 * 	Main Operations of Game Developed by Nathan Pillman(nathanwwe)
 * 	Use of this software is strictly limited to PlayStation® Hardware
 * 	Software is being licensed from PlayStation®Mobile Development program.
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.HighLevel.UI;


namespace AppData3088261ATSI
{
	public class OS
	{
		
		public static Vector4 BG;
		static GraphicsContext graphics;
		
		public OS(bool hasPASS)
		{
			
			/* Graphics Disable due to Xperia Z errors*/
			//intro Loading = new intro(); 
            graphics = new GraphicsContext(960,544,PixelFormat.Rgba,PixelFormat.Depth16,MultiSampleMode.None);
            UISystem.Initialize(graphics);

            Terminal scene = new Terminal();
            SetupListNum(scene.RootWidget);
            scene.SetWidgetLayout(LayoutOrientation.Horizontal);
            UISystem.SetScene(scene);
			if(!hasPASS)
				scene.fresh();
            for (; ; )
            {
                SystemEvents.CheckEvents();

                // update
                {
                    List<TouchData> touchDataList = Touch.GetData(0);
                    var gamePad = GamePad.GetData (0);
                    UISystem.Update(touchDataList, ref gamePad);
                }

                // draw
                {
                    graphics.SetViewport(0, 0, graphics.Screen.Width, graphics.Screen.Height);
                    graphics.SetClearColor(
                        0xFF, 
                        0xFF, 
                        0xFF, 
                        0xff);
                    graphics.Clear();

                    UISystem.Render();
                    graphics.SwapBuffers();
					if(scene.isHacking)
						scene.fakePACKETS();
					if(scene.isFlow)
						scene.addCash();
                }
            }
        }

        private static void SetupListNum(ContainerWidget container)
        {
            foreach (var child in container.Children)
            {
                if (child is ListPanel)
                {
                    (child as ListPanel).Sections = section;
                }

                if (child is GridListPanel)
                {
                    (child as GridListPanel).ItemCount = 100;
                    (child as GridListPanel).StartItemRequest();
                }

                if (child is LiveListPanel)
                {
                    (child as LiveListPanel).ItemCount = 100;
                    (child as LiveListPanel).StartItemRequest();
                }

                if (child is ContainerWidget)
                {
                    SetupListNum(child as ContainerWidget);
                }
                else if (child is PagePanel)
				{
					var pagePanel = (PagePanel)child;
					for (int i = 0; i < pagePanel.PageCount; i++) {
						SetupListNum (pagePanel.GetPage (i));
					}
				}
				else if (child is LiveFlipPanel)
				{
					var liveFlip = (LiveFlipPanel)child;
					SetupListNum(liveFlip.FrontPanel);
					SetupListNum(liveFlip.BackPanel);
				}

            }
        }

        private static ListSectionCollection section = new ListSectionCollection {
            new ListSection("Section1", 10),
            new ListSection("Section2", 3),
            new ListSection("Section3", 30),
            new ListSection("Section4", 27),
            new ListSection("Section5", 10),
            new ListSection("Section6", 20),
        };
    }

	public class __UicCustomClassBase__ : Label
	{
        public __UicCustomClassBase__()
        {
            BackgroundColor = new UIColor(0.3f, 0.3f, 0.3f, 1.0f);
            VerticalAlignment = VerticalAlignment.Middle;
            HorizontalAlignment = HorizontalAlignment.Center;
        }
        bool update;
        protected override void OnUpdate(float elapsedTime)
        {
            if (!update)
            {
                Text = this.ToString();
                update = true;
            }
            base.OnUpdate(elapsedTime);
        }
	}
		
		
	}

