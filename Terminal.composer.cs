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
 * */

// Official Commented Sample, Resumee for Nathan Pillman
using System;
using System.IO;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Device;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;
using Sce.PlayStation.Core.Services;
using Microsoft.Win32;
using System.Web.Services;
//^Included references
namespace AppData3088261ATSI
{
    partial class Terminal
    {
		// All Main Variables, For Use Later 
		WebData server;
		String software = "2.XX";
        Panel sceneBackgroundPanel;
        EditableText CMD_LINE;
        Panel bg;
        Label cmd;
        Button Enter;
        Button Hack;
		Button Free;
		string dummy;
		string search;
		public string view_TEXT;
		bool isSudo;
		bool Initialize;
		// PlayStation Vita SaveData Directory
		const string SAVE_PASS = "/Documents/sudopass.dat";
		string password;
		public bool isHacking = false;
		int packets;
	    private byte[] inputData;
	    private int inputDataSize;
		private int sessionPSN = 0;
		public Boolean isPSN = false;
		MessageDialog dev;
		
		/* Constructor, Kinda*/
        private void InitializeWidget()
		{
			// Server is a connection, to download an update file. Server is down most likely by the time you review it.
			server = new WebData();
            InitializeWidget(LayoutOrientation.Horizontal);
        }
        private void InitializeWidget(LayoutOrientation orientation)
        {
			// Set all initials for startup, draw and set data.
			getData();
			view_TEXT = "Welcome to Virtual Terminal\n(C)2014 Tactician Studios LLC Software Team\nVersion:" + software + "\nUse '?' for list of common commands\n";
            sceneBackgroundPanel = new Panel();
            sceneBackgroundPanel.Name = "sceneBackgroundPanel";
            CMD_LINE = new EditableText();
            CMD_LINE.Name = "CMD_LINE";
			dev = new MessageDialog();
			dev.Name = "dev";
            bg = new Panel();
            bg.Name = "bg";
            cmd = new Label();
            cmd.Name = "cmd";
            Enter = new Button();
            Enter.Name = "Enter";
			Hack = new Button();
			Hack.Name = "Hack";
			Free = new Button();
			Free.Name = "Free";
			
			
			isSudo = false;
			Initialize = false;
			packets = 0;
            // sceneBackgroundPanel
            sceneBackgroundPanel.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);

            // Terminal
            this.RootWidget.AddChildLast(sceneBackgroundPanel);
            this.RootWidget.AddChildLast(CMD_LINE);
            this.RootWidget.AddChildLast(bg);
            this.RootWidget.AddChildLast(Enter);
			this.RootWidget.AddChildLast(Hack);
			this.RootWidget.AddChildLast(Free);
			this.RootWidget.AddChildLast(dev);

            // CMD_LINE
            CMD_LINE.TextColor = new UIColor(102f / 255f, 255f / 255f, 0f / 255f, 255f / 255f);
            CMD_LINE.Font = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            CMD_LINE.LineBreak = LineBreak.Character;

            // bg
            bg.BackgroundColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            bg.Clip = true;
            bg.AddChildLast(cmd);

            // cmd
            cmd.TextColor = new UIColor(0f / 255f, 255f / 255f, 87f / 255f, 255f / 255f);
            cmd.Font = new UIFont(FontAlias.System, 18, FontStyle.Bold);
            cmd.TextTrimming = TextTrimming.Character;
            cmd.LineBreak = LineBreak.Word;
            cmd.VerticalAlignment = VerticalAlignment.Top;

            // Enter
            Enter.TextColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            Enter.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            Enter.BackgroundFilterColor = new UIColor(0f / 255f, 255f / 255f, 36f / 255f, 255f / 255f);
			Enter.TouchEventReceived += (sender, e)=> update();
			Enter.KeyEventReceived += (sender, e) => update(e);
			
			Hack.TextColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            Hack.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            Hack.BackgroundFilterColor = new UIColor(0f / 255f, 255f / 255f, 36f / 255f, 255f / 255f);
			Hack.TouchEventReceived += (sender, e)=> hackPSN();
			Hack.KeyEventReceived += (sender, e) => hackPSN(e);
			Hack.Enabled = false;
			
			Free.TextColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            Free.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            Free.BackgroundFilterColor = new UIColor(0f / 255f, 255f / 255f, 36f / 255f, 255f / 255f);
			Free.TouchEventReceived += (sender, e)=> PSN();
			Free.KeyEventReceived += (sender, e) => PSN(e);
			Free.Enabled = false;
			
			dev.Visible = false;
			dev.Enabled = false;
			
			dev.ButtonPressed += (sender, e)=> closeInfo();
			dev.CustomBackgroundColor = new UIColor(1.0f,1.0f,1.0f,0.5f);
			dev.KeyEventReceived += (sender, e)=> closeInfo(e);
			dev.TouchEventReceived += (sender, e)=> closeInfo();
            SetWidgetLayout(orientation);
            UpdateLanguage();
        }
		/* Generated Code, Most of it* */
        private LayoutOrientation _currentLayoutOrientation;
        public void SetWidgetLayout(LayoutOrientation orientation)
        {
            switch (orientation)
            {
                case LayoutOrientation.Vertical:
                    this.DesignWidth = 544;
                    this.DesignHeight = 960;

                    sceneBackgroundPanel.SetPosition(0, 0);
                    sceneBackgroundPanel.SetSize(544, 960);
                    sceneBackgroundPanel.Anchors = Anchors.Top | Anchors.Bottom | Anchors.Left | Anchors.Right;
                    sceneBackgroundPanel.Visible = true;

                    CMD_LINE.SetPosition(-52, 394);
                    CMD_LINE.SetSize(360, 56);
                    CMD_LINE.Anchors = Anchors.None;
                    CMD_LINE.Visible = true;

                    bg.SetPosition(635, 259);
                    bg.SetSize(100, 100);
                    bg.Anchors = Anchors.None;
                    bg.Visible = true;

                    cmd.SetPosition(336, 47);
                    cmd.SetSize(214, 36);
                    cmd.Anchors = Anchors.None;
                    cmd.Visible = true;

                    Enter.SetPosition(793, 488);
                    Enter.SetSize(214, 56);
                    Enter.Anchors = Anchors.None;
                    Enter.Visible = true;
					
					Hack.SetPosition(793, 488);
                    Hack.SetSize(214, 56);
                    Hack.Anchors = Anchors.None;
                    Hack.Visible = false;
				
					Free.SetPosition(793, 488);
                    Free.SetSize(214, 56);
                    Free.Anchors = Anchors.None;
                    Free.Visible = false;

                    break;

                default:
                    this.DesignWidth = 960;
                    this.DesignHeight = 544;

                    sceneBackgroundPanel.SetPosition(0, 0);
                    sceneBackgroundPanel.SetSize(960, 544);
                    sceneBackgroundPanel.Anchors = Anchors.Top | Anchors.Bottom | Anchors.Left | Anchors.Right;
                    sceneBackgroundPanel.Visible = true;

                    CMD_LINE.SetPosition(0, 490);
                    CMD_LINE.SetSize(837, 54);
                    CMD_LINE.Anchors = Anchors.None;
                    CMD_LINE.Visible = true;

                    bg.SetPosition(0, 0);
                    bg.SetSize(960, 490);
                    bg.Anchors = Anchors.None;
                    bg.Visible = true;

                    cmd.SetPosition(0, 0);
                    cmd.SetSize(950, 480);
                    cmd.Anchors = Anchors.None;
                    cmd.Visible = true;

                    Enter.SetPosition(837, 488);
                    Enter.SetSize(123, 56);
                    Enter.Anchors = Anchors.None;
                    Enter.Visible = true;
				
					Hack.SetPosition(837, 488);
                    Hack.SetSize(123, 56);
                    Hack.Anchors = Anchors.None;
                    Hack.Visible = false;
				
					Free.SetPosition(837, 488);
                    Free.SetSize(123, 56);
                    Free.Anchors = Anchors.None;
                    Free.Visible = false;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }
		/* End of Generated Code */
		
        public void UpdateLanguage()
        {
			// For multiple languages, basically useless in this project.
            CMD_LINE.DefaultText = "Enter Command: ";

            cmd.Text = view_TEXT;

            Enter.Text = "GO";
			Hack.Text = "Hack!";
			Free.Text = "Next!";
        }

        private void onShowing(object sender, EventArgs e)
        {
            switch (_currentLayoutOrientation)
            {
                case LayoutOrientation.Vertical:
                    break;

                default:
                    break;
            }
        }

        private void onShown(object sender, EventArgs e)
        {
            switch (_currentLayoutOrientation)
            {
                case LayoutOrientation.Vertical:
                    break;

                default:
                    break;
            }
        }
		/*
		 * 	Update Method: 
		 * 	Not Generated!
		 * 	ToDo: File Operations text
		 * 		-Access web server for update of commands
		 * 		-Write logs in a secured file
		 * 		-Animation Command
		 * 		-Forget Password
		 * 
		 * 	Finished:
		 * 		-Help Command
		 * 		-Fix Errors[indexOutofBounds]
		 * 		-SUDO fix
		 * 		-Fake Hack App
		*/
		public void update()
		{
			// Reads current inputed text, and converts it to string.
			String entry = CMD_LINE.Text.ToString();
			// Check if there is a value, just in-case of double enter press.	
			if(!entry.Equals(""))
			{	// Check if password was set.
				if(!Initialize)
				{
					// Check if using a Sudo Loop
					if(!isSudo)
					{
						// Update dummy string, all caps to easily test
						dummy = CMD_LINE.Text.ToUpper();
						
						
						// Check if button is being pressed willingly. 'Data is entered'
						if(!dummy.Equals(""))
						{
							//	Output command to user
							view_TEXT += "\n Command: " + dummy;
							//	Check for SUDO or another keyword. Make sure length is long
							// enough to fix indexOutOfRange
							if(dummy.Length >= 4)
								search = dummy.Substring(0,4);
							else if(dummy.Length >= 3)
								search = dummy.Substring(0,3);
							else
								search = "n/a";
							//	Test 4 Keyword
							if(search.Equals("SUDO"))
							{
								// Enter Super User Mode, for password management.
								view_TEXT += "\n\n\tPASSWORD FOR SUDO:\t";
								CMD_LINE.TextInputMode = TextInputMode.Password;
								isSudo = true;
								password = getPassword();
							}
							if(search.Equals("HACK"))
							{
								// Enter "Hack Mode", basically askes for domain or user to hack. 
								Enter.Visible = false;
								Hack.Visible = true;
								Enter.Enabled = false;
								Hack.Enabled = true;
								view_TEXT = "\nHACK MODE INITIATED:\n\n\n" +
									"THIS SOFTWARE IS NOW INITIALIZING A HACKING PROTOCOL! WE ENCOURAGE NOT\nTO HACK ANYONE WHO " +
									"DOES NOT WANT TO BE HACKED!\nTHIS SOFTWARE WILL REPORT IF ABUSE HAS OCCURED \nAND YOU WILL BE RESPONSIBLE FOR YOUR ACTIONS!\n" +
									"*********************\"HACKERS OF America\"******************************";
								
								
								view_TEXT += "\n\nPlease enter website or username to hack:\n\n";
							}
							// Reads settings
							if(search.Equals("NANO"))
								NANO("settings.dat");
							// Clears current text on screen.
							if(search.Equals("WIPE"))
								view_TEXT = "";
							if(search.Equals("INFO"))
								openInfo();
							//if(search.Equals("TERM"))
								//terms();
							//	Gets Local Information, to come!
							
							if(dummy.Equals("IPCONFIG"))
							{
								//ipconfig();
								view_TEXT += "\n\nIPCONFIG is disabled! Sorry.";
							}
							if(dummy.Equals("SYSTEM.SETTINGS.FORMAT();"))
							{
								// Help Forum, incase user lost password.
								delete();
							}
							if(search.Equals("SQL"))
							{
								// MySQL mode, same as Hack mode basically
								Enter.Visible = false;
								Free.Visible = true;
								Enter.Enabled = false;
								Free.Enabled = true;
								
								view_TEXT = "\nNetwork Mode Initiated:\n\n\n" +
									"You were logged on succesfully! We are now generating security messure forces to bypass any authorization!" +
									"What do you want to do?\n\nSamples:\n" +
									"Add #### <- Adds currency to account, replace \"#\" with a digit.\n" +
									"Free **** <- Changes a price of a product, **** = name\n"+
									"Exit <- Closes session.\n"+
									"*********************\"mySql-Network Modification\"******************************";
								
								
								view_TEXT += "\n\nPlease enter your command:\n\n";
							}
							//	Help command check, multiple ways to access
							if(dummy.Equals("?")||dummy.Equals("HELP")||dummy.Equals("/?")||dummy.Equals("/HELP"))
							{
								view_TEXT += "\n\nWorking commands:" +
									"\n\"HACK\":\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tHack USER or URL" +
									"\n\"SQL\" :\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tModifies Online Settings" +
									"\n\"WIPE\" :\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tClears Text on Screen" +
									"\n\"NANO\" :\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tReads settings file" +
									"\n\"INFO\" :\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tDeveloper Information" +
									"\n\"SUDO APT-GET UPDATE\":\t\t\t\t\tUpdates commands" +
									"\n\"SUDO APT-GET DELETE\":\t\t\t\t\tFormats Super User";
							
							}
							
							
						}
					}else{
						//	SUDO commands. 
						string pass = CMD_LINE.Text.ToUpper();
						// Check password
						if(!pass.Equals(password.ToUpper())){
							string data = dummy.Substring(5,dummy.Length - 5);
							if(data.Equals("APT-GET DELETE"))
							view_TEXT += "\n\nUse: \"SYSTEM.SETTINGS.FORMAT();\"";
								
							view_TEXT += "\n\nINVALID PASSWORD!";
							CMD_LINE.TextInputMode = TextInputMode.Normal;
						}else{
							// Get secondary data for commands
							string data = dummy.Substring(5,dummy.Length - 5);
							if(data.Equals("APT-GET UPDATE"))
							{
								updateInfo();
							}
							else if(data.Equals("APT-GET DELETE"))
							{
								delete();
							}
							else{
							view_TEXT += "\n\nInvalid command! \"" + data + "\"";
							}
							CMD_LINE.TextInputMode = TextInputMode.Normal;
						}
						isSudo = false;
					}
				}else{
					// Create pass if not exist.
					createPass();
				}
				//	Remove previous command and output new data.
				CMD_LINE.Text = "";
				cmd.Text = view_TEXT;
			}
			
    	}
		
		/* Overload Methods*/
		public void update(KeyEventArgs keys)
		{
			if(keys.KeyType == KeyType.Enter)
				update();
		}
		
		public void	hackPSN(KeyEventArgs keys)
		{
			if(keys.KeyType == KeyType.Enter)
				hackPSN();
		}
		
		public void	PSN(KeyEventArgs keys)
		{
			if(keys.KeyType == KeyType.Enter)
				PSN();
		}
		public void	closeInfo(KeyEventArgs keys)
		{
			if(keys.KeyType == KeyType.Enter)
				closeInfo();
		}
		
		
		// System IO write file(Save Password)
		public void createPass()
		{
			
			dummy = CMD_LINE.Text.ToUpper();
			inputDataSize = dummy.Length;
			CMD_LINE.Text = "";
			if ( inputDataSize > 0 ) {
				
					byte[] byteArray0 = System.Text.Encoding.Unicode.GetBytes(dummy);
                    inputData = new byte[byteArray0.Length];
                    for (int i = 0; i < byteArray0.Length; i++) {
                        inputData[i] = byteArray0[i];
                    }
				
				inputDataSize = byteArray0.Length;
                using (System.IO.FileStream hStream = System.IO.File.Open(@SAVE_PASS, FileMode.Create)) {
                    hStream.SetLength((int)inputDataSize);
                    hStream.Write(inputData, 0, (int)inputDataSize);
                    hStream.Close();
                }
				old();
            }
		}
		
		public string getPassword()
		{
			if ( true == System.IO.File.Exists(@SAVE_PASS) ) {
                using (System.IO.FileStream hStream = System.IO.File.Open(@SAVE_PASS, FileMode.Open)) {
                    if (hStream != null) {
                        long size = hStream.Length;
                        inputData = new byte[size];
                        hStream.Read(inputData, 0, (int)size);
                        inputDataSize = (int)size;
                        password = System.Text.Encoding.Unicode.GetString(inputData);
                        hStream.Close();
                    }
                }
            }
			return password;
		}
		
		//	Warm welcome to new user.
		public void fresh()
		{
			Initialize = true;
			view_TEXT = "Welcome!\n\tThis is a special virtual terminal! It is your first startup, so lets get the basics.\n\nFirst, we need a password. To keep us all safe. \n\nPASS:\t";
			CMD_LINE.TextInputMode = TextInputMode.Password;
			cmd.Text = view_TEXT;
			
		}
		
		//	Revert from fresh() method
		public void old()
		{
			view_TEXT = "Thanks! \n Now have fun with the terminal.\nIt is based off of a Linux terminal...\n";
			Initialize = false;
			CMD_LINE.TextInputMode = TextInputMode.Normal;
			cmd.Text = view_TEXT;
		}
		
		public void delete()
		{
			// Delete save files except for (settings.dat)
			File.Delete(@SAVE_PASS);
			view_TEXT = "System was formatted! Restarting Application!";
			fresh();
		}
		
		
		int dropPackets;
		public void hackPSN()
		{
			dummy = CMD_LINE.Text.ToUpper();
			if(!dummy.Equals("") && !dummy.Equals("NOW HACKING! YOUR WELCOME!"))
			{ 
				// ^ I misspelled (You're welcome) and it was allowed, I am a better speller, but eh! 
				// Sony Computer Entertainment hasn't back-handed me yet!
				view_TEXT += "\t\t\t\t" + dummy;
				cmd.Text = view_TEXT;
				if (dummy.Length >= 4)
					search = dummy.Substring(0,4);
				else
					search = "";
				if(search.Equals("HTTP"))
				{
					view_TEXT =	"Now Hacking Website! Might take a while!";
				}else{
					view_TEXT =	"Now Hacking User! Might take a while! S/He loves to hide :)";	
				}
				CMD_LINE.Text = "NOW HACKING! YOUR WELCOME!";
				CMD_LINE.Enabled = false;
				Hack.Enabled = false;
				isHacking = true;
				cmd.Text = view_TEXT;
				dropPackets = 1;
			}
    	}
		
		public void fakePACKETS()
		{
			// Math Operations to count up to 10,000
			packets += 12;
			if(search.Equals("HTTP"))
				{
					view_TEXT =	"Now Hacking Website! Might take a while!";
				}else{
					view_TEXT =	"Now Hacking User! Might take a while! S/He loves to hide :)";	
				}
				if((packets / (dropPackets * dummy.Length) > 5))
				{
					dropPackets += dummy.Length;
					packets -= 12;
				}
				view_TEXT += "\n\nPACKETS SENT:		" + packets.ToString() + "\nPACKETS DROP:		" + dropPackets.ToString() + "\n\nTrying to reach 10,000 packets!";
				cmd.Text = view_TEXT;
			if(packets >= 10000)
			{
				view_TEXT =	"Hack was completed! Great Job! Your now officially \"Kool\"\nWhat now?";	
								Enter.Visible = true;
								Hack.Visible = false;
								Enter.Enabled = true;
								Hack.Enabled = false;
				isHacking = false;
				CMD_LINE.Enabled = true;
				CMD_LINE.Text = "";
				cmd.Text = view_TEXT;
				packets = 0;
				dropPackets = 0;
			}
		}
		// Called from entering "NANO"
		public void NANO(String file)
		{
			view_TEXT += "\n\n";
			string inputFile = "/Documents/" + file;
			if ( true == System.IO.File.Exists(@inputFile) ) {
                using (System.IO.FileStream hStream = System.IO.File.Open(@inputFile, FileMode.Open)) {
                    if (hStream != null) {
                        long size = hStream.Length;
                        inputData = new byte[size];
                        hStream.Read(inputData, 0, (int)size);
                        inputDataSize = (int)size;
                        view_TEXT += System.Text.Encoding.ASCII.GetString(inputData);
                        hStream.Close();
                    }
                }
            }else{
				view_TEXT += "\nFILE DOES NOT EXIST!";	
			}
		}
		
		public string LOAD(String file)
		{
			String data = "";
			string inputFile = "/Documents/" + file;
			if ( true == System.IO.File.Exists(@inputFile) ) {
                using (System.IO.FileStream hStream = System.IO.File.Open(@inputFile, FileMode.Open)) {
                    if (hStream != null) {
                        long size = hStream.Length;
                        inputData = new byte[size];
                        hStream.Read(inputData, 0, (int)size);
                        inputDataSize = (int)size;
                        data += System.Text.Encoding.ASCII.GetString(inputData);
                        hStream.Close();
                    }
                }
            }
			return data;
		}
		
		public void getData()
		{
			// Reads Secured Update File. Searches for the First CHAR "V" to skip any copyright tag.
			String data = LOAD ("settings.dat");
			if(data.Length > 10)
			{
				String checker;
				int Start = 0;
				checker = data.Substring(Start,1);
				if(checker.Equals("V"))
					software = data.Substring(Start + 8, 4);
			}
		}
		
		public void PSN()
		{
			String entry = CMD_LINE.Text.ToString();
				
			if(!entry.Equals(""))
			{
				entry = entry.ToUpper();
				if(entry.Length >= 4)
				{
					
					search = entry.Substring(0,4);
					
					if(search.Equals("ADD "))
					{	
						if(entry.Length > 5)
						{
							string desValue = entry.Substring(4,entry.Length - 4);
							Console.Write(desValue);
							if(IsNumeric(desValue))
							{	
								view_TEXT =	"\nAdding $" + desValue + " to your account!";
								startCash(int.Parse(desValue));
							}
							else
							{
								view_TEXT +=	"\nInvalid Value! Please enter a number after the command \"ADD\"\n";
							}
						}
						
					}else if(search.Equals("FREE"))
					{
						if(entry.Length > 5)
						{
							string gameValue = entry.Substring(4,entry.Length - 4);	
							view_TEXT += "Added \"" + gameValue + "\" to your download list!";
							
						}else
							view_TEXT += "Invalid game name!";
						
					}else if(search.Equals("EXIT"))
					{
						view_TEXT = "\nExited Network mySQL mode!\n";
						Enter.Visible = true;
						Free.Visible = false;
						Enter.Enabled = true;
						Free.Enabled = false;	
					}else
					   view_TEXT += "INVALID\n";    
					
				}else{
					view_TEXT += "INVALID\n";
				}
				CMD_LINE.Text = "";
				cmd.Text = view_TEXT;
			}
    	}
		public static bool IsNumeric(object Expression)
	    {
	        bool isNum;
	        double retNum;
	
	        isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any,System.Globalization.NumberFormatInfo.InvariantInfo, out retNum );
	        return isNum;
	    }
		
		int cashAmount = 0;
		int currentCash = 0;
		public bool isFlow = false; 
		public void addCash()
		{
			if(currentCash < cashAmount)
			{
				currentCash++;	
				view_TEXT =	"Adding $" + cashAmount + " to account!\nHave added $" + currentCash + " so far!";
			}
			else
			{
				view_TEXT =	"Money was dumped into account!\n";
				Enter.Visible = true;
				Free.Visible = false;
				Enter.Enabled = true;
				Free.Enabled = false;
				isFlow = false;
				CMD_LINE.Enabled = true;
				CMD_LINE.Text = "";
				cmd.Text = view_TEXT;
			}
			cmd.Text = view_TEXT;
			
		}	
		
		public void startCash(int start)
		{
			currentCash = 0;	
			cashAmount = start;
			isFlow = true;
			Free.Enabled = false;
			CMD_LINE.Enabled = false;
		}
		
		public void updateInfo()
		{	
			server.connect("http://tsiserver.us/data/settings.dat");
			
			do
			{
				server.Update();
			}
			while(server.webData.Equals(""));
			string newData = server.webData.ToString();
			if(!newData.Equals("ERROR"))
			{
				saveFile(newData,"settings.dat");
				view_TEXT += "\n\n\tBinaries Updated!";
			}else
				view_TEXT += "\n\n\tServer is down!";
		}
		
		// System IO write file(Save Password)
		public void saveFile(string data, string location)
		{
			string SAVE_FILE = "/Documents/" + location;
			dummy = data;
			inputDataSize = dummy.Length;
			if ( inputDataSize > 0 ) {
				
					byte[] byteArray0 = System.Text.Encoding.ASCII.GetBytes(dummy);
                    inputData = new byte[byteArray0.Length];
                    for (int i = 0; i < byteArray0.Length; i++) {
                        inputData[i] = byteArray0[i];
                    }
				
				inputDataSize = byteArray0.Length;
                using (System.IO.FileStream hStream = System.IO.File.Open(@SAVE_FILE, FileMode.CreateNew)) {
                    hStream.SetLength((int)inputDataSize);
                    hStream.Write(inputData, 0, (int)inputDataSize);
                    hStream.Close();
                }
            }
			
		}
		public void openInfo()
		{
			/* Please Note:
			 * I added more people to the project, however, Nathan Pillman made this file.
			 * My 2nd Personality is "Katy", so I go by that in programming.
			 * 
			 * */
			dev.Title = "Tactician Studios LLC.";
			dev.Message = "Owners:\nNathan Pillman\nKaty Pillman\nMike Hamilton\n\nProgrammers:\nNathan Pillman\nKaty Pillman\nDenzel Richmond" +
				"\nMike Hamilton\n\nServer Management:\nConsolidated Communications, Inc.\nTactician Studios LLC.\n\nPolicy:\nUse of this " +
				"software is AS-IS and refunds will not be benefited. We offer servers at our expense and may remove any and/or all features " +
				"related   to our network. If we do decide to remove any servers or services, we willupload a tutorial how to replace them yourself." +
				"You are allowed to host    your own server, but it is your reliability if anything unfortunate happens! Other-wise, using our servers you agree " +
				"to the terms of service located  at: (\"http://tsiserver.us/terms\").";
			
			
			dev.Enabled = true;
			dev.Visible = true;
			
			dev.Style = MessageDialogStyle.Ok;
			
		}
		public void closeInfo()
		{
			dev.Enabled = false;
			dev.Visible = false;
		}
		public void terms()
		{
			
			dev.Title = "Tactician Studios LLC.";
			dev.Message = readTerms();
			dev.Style = MessageDialogStyle.Ok;
			
			dev.Enabled = true;
			dev.Visible = true;
		}
		public string readTerms()
		{
			string terms = "";
			TermsData reader = new TermsData();
			terms = reader.data;
			return terms;
		}
		
	}
}