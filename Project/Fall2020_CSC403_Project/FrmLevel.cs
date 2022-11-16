using Fall2020_CSC403_Project.code;
using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using MyGameLibrary;
using System.IO;
using System.Runtime.InteropServices;
<<<<<<< HEAD
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.Emit;
using System.Threading;
using Timer = System.Windows.Forms.Timer;
=======
using System.Media;
using System.IO;
using System.Collections.Generic;
>>>>>>> 1bc7446ed032b478595a326c46bbbe0b9ea7737c

namespace Fall2020_CSC403_Project {
  public partial class FrmLevel : Form {
    private Player player;

    private Enemy enemyPoisonPacket;
    private Enemy bossKoolaid;
    private Enemy enemyCheeto;
    private NPC npcVillager;
    private Tile[] walls;
    private Tile[] damagingtiles;
    private Tile[] slipperytiles;
    private Tile[] teleportingtiles;
    private Tile[] slowingtiles;
    private Tile[] spawningtiles;
    private Boolean InteractionPossible;
    private int TeleportIndex = 0;
    private Boolean SlipperyFlag = false;
    private Boolean SlowFlag = false;

    private DateTime timeBegin;
    private FrmBattle frmBattle;
        
    private HealthPotion healthPotion;

    private Sword sword;

    private Inventory inventoryHeal;
    private Inventory inventorySword;

    static int SWITCH1 = 1;
    static int SWITCH2 = 1;
<<<<<<< HEAD
    private string levelstring;

    public FrmLevel(string levelstring) {
      InitializeComponent();
      this.levelstring = levelstring;
    }
=======
    static int mode = 0;
    public static int charClass = 0;
    public static int skin = 0;
    public static string username = "";
    public static int volume_level = 0;

        public FrmLevel()
        {
            InitializeComponent();
            mode = FrmCharacterSelect.mode;
            volume_level = FrmCharacterSelect.volume_level;
            username = FrmCharacterSelect.username;
            if (mode == 1)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Bounds = Screen.PrimaryScreen.Bounds;
                mode = 1;
            }
            if (mode == 2)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                this.Bounds = Screen.PrimaryScreen.Bounds;
                mode = 2;
            }
            charClass = FrmCharacterSelect.charClass;
            skin = FrmCharacterSelect.skin;
            if (charClass == 1)
            {
                if (skin == 1) picPlayer.BackgroundImage = global::Fall2020_CSC403_Project.Properties.Resources.player_skin;
            }
            if (charClass == 2)
            {
                if (skin == 1) picPlayer.BackgroundImage = global::Fall2020_CSC403_Project.Properties.Resources.player_2_skin;
                else picPlayer.BackgroundImage = global::Fall2020_CSC403_Project.Properties.Resources.player_2;
            }
            if (charClass == 3)
            {
                if (skin == 1) picPlayer.BackgroundImage = global::Fall2020_CSC403_Project.Properties.Resources.player_3_skin;
                else picPlayer.BackgroundImage = global::Fall2020_CSC403_Project.Properties.Resources.player_3;
            }
        }
>>>>>>> 35db04980f4796e146402d742408ec568ab46140

    private void FrmLevel_Load(object sender, EventArgs e) {

      const int LEVEL_ROW_SIZE = 10;
      const int LEVEL_COLUMN_SIZE = 17;
      const int PADDING = 7;
      InteractionPossible = true;

      label2.Hide();

      int rownumber = 0;
      int columnnumber = 0;
      int numwalls = 0;
      int numdamagingtiles = 0;
      int numslipperytiles = 0;
      int numteleportingtiles = 0;
      int numslowingtiles = 0;
      int numspawningtiles = 0;


      healthPotion = new HealthPotion("Health Potion", player);
      sword = new Sword("Sword", player, 3); 
      inventoryHeal = new Inventory(healthPotion, 2);
      inventorySword = new Inventory(sword, 0);

      char[,][] level = new char[LEVEL_ROW_SIZE, LEVEL_COLUMN_SIZE][];
      foreach (string row in levelstring.Split('\n')) {
          for(int hexindex = 0; hexindex < row.Length - 1; hexindex+=2) {
              string mapdatabinary = ConvertCharToBinary(row[hexindex]) + ConvertCharToBinary(row[hexindex + 1]);
              char[] tilecode = mapdatabinary.ToCharArray();
              level[rownumber, hexindex/2] = tilecode;
              numwalls += tilecode[3] == '1' ? 1 : 0;
              numspawningtiles += tilecode[4] == '1' ? 1 : 0;
              numdamagingtiles += tilecode[5] == '1' ? 1 : 0;
              numslipperytiles += tilecode[6] == '1' ? 1 : 0;
              numteleportingtiles += tilecode[7] == '1' ? 1 : 0;
              numslowingtiles += tilecode[8] == '1' ? 1 : 0;
              columnnumber++;
          }   
          rownumber++;
      }

      walls = new Tile[numwalls];
      damagingtiles = new Tile[numdamagingtiles];
      slipperytiles = new Tile[numslipperytiles];
      teleportingtiles = new Tile[numteleportingtiles];
      slowingtiles = new Tile[numslowingtiles];
      spawningtiles = new Tile[numspawningtiles];

      int wallcount = 0;
      int damagingcount = 0;
      int slipperycount = 0;
      int teleportingcount = 0;
      int slowingcount = 0;
      int spawningcount = 0;

      Game.player = player;
      Game.healthPotion = healthPotion;
      Game.sword = sword;
      Game.inventoryHeal = inventoryHeal;
      Game.inventorySword = inventorySword;

      timeBegin = DateTime.Now;

      for (int index = 0; index < LEVEL_ROW_SIZE * LEVEL_COLUMN_SIZE; index++) {
          PictureBox tilepicture = Controls.Find("tile" + (index + 1).ToString(), true)[0] as PictureBox;
          char[] tilecode = level[index / LEVEL_COLUMN_SIZE, index % LEVEL_COLUMN_SIZE];
          string tiletexturecode = String.Join("",tilecode.Take(3).ToArray());
          string tiletexturecode2 = String.Join("", tilecode.Skip(9).Take(1).ToArray());
          
          switch (tiletexturecode) {
              case "000": tilepicture.BackgroundImage = Properties.Resources.Brick_Texture; break;
              case "001": tilepicture.BackgroundImage = Properties.Resources.Dirt_Texture; break;
              case "010": tilepicture.BackgroundImage = Properties.Resources.Grass_Texture; break;
              case "011": tilepicture.BackgroundImage = Properties.Resources.Ice_Texture; break;
              case "100": tilepicture.BackgroundImage = Properties.Resources.Lava_Texture; break;
              case "101": tilepicture.BackgroundImage = Properties.Resources.Portal_Texture; break;
              case "110": tilepicture.BackgroundImage = Properties.Resources.Stone_Texture; break;
              case "111": tilepicture.BackgroundImage = Properties.Resources.Water_Texture; break;
          }

          char[] tileflagcode = tilecode.Skip(3).Take(7).ToArray();

          Tile tile = new Tile(CreatePosition(tilepicture), CreateCollider(tilepicture, PADDING), tileflagcode);
          if (tile.isSolid) {
              walls[wallcount] = tile; wallcount++;
          }
          if (tile.isDamaging) {
              damagingtiles[damagingcount] = tile; damagingcount++;
          }
          if (tile.isSlippery) {
              slipperytiles[slipperycount] = tile; slipperycount++;
          }
          if (tile.isTeleporter) {
              teleportingtiles[teleportingcount] = tile; teleportingcount++;
          }
          if (tile.isSlowing) {
              slowingtiles[slowingcount] = tile; slowingcount++;
          }
          if (tile.isSpawning)
          {
            spawningtiles[spawningcount] = tile; spawningcount++;
          }

          switch (tiletexturecode2)
          {
            case "1": picSword.Location = tilepicture.Location;
                      picSword.SizeMode = PictureBoxSizeMode.StretchImage;
                      picSword.Image = Properties.Resources.GrassSwordTexture; 
                      picSword.BringToFront();  
                      break;
          }
      }

      bossKoolaid = new Enemy(CreatePosition(picBossKoolAid), CreateCollider(picBossKoolAid, PADDING));
      enemyPoisonPacket = new Enemy(CreatePosition(picEnemyPoisonPacket), CreateCollider(picEnemyPoisonPacket, PADDING));
      enemyCheeto = new Enemy(CreatePosition(picEnemyCheeto), CreateCollider(picEnemyCheeto, PADDING));
      bossKoolaid.Img = picBossKoolAid.BackgroundImage;
      enemyPoisonPacket.Img = picEnemyPoisonPacket.BackgroundImage;
      enemyCheeto.Img = picEnemyCheeto.BackgroundImage;
      bossKoolaid.Color = Color.Red;
      enemyPoisonPacket.Color = Color.Green;
      enemyCheeto.Color = Color.FromArgb(255, 245, 161);

      npcVillager = new NPC(CreatePosition(picNPCVillager), CreateCollider(picNPCVillager, PADDING));
      picNPCVillager.BringToFront();

      player = new Player(CreatePosition(picPlayer), CreateCollider(picPlayer, PADDING));
      Game.player = player;
      picPlayer.BringToFront();
      this.lblInGameTime.BringToFront();
      timeBegin = DateTime.Now;

    if (charClass == 2)
    {
        player.GO_INC = 8; 
        player.AlterHealth(-5); 
        player.strength = 4;
    }
    if (charClass == 3)
    {
        player.GO_INC = 1; 
        player.AlterHealth(-10);
        player.strength = 1;
    }

    axWindowsMediaPlayer1.URL = @"Exploration_music_v2.wav";
    axWindowsMediaPlayer1.settings.playCount = 9999;
    axWindowsMediaPlayer1.settings.volume = volume_level;
  }

    private Vector2 CreatePosition(PictureBox pic) {
      return new Vector2(pic.Location.X, pic.Location.Y);
    }
    
    private int EnemyMovement(Enemy enemy, PictureBox pb, int SWITCH) {
      int s = SWITCH;
      enemy.Move();
      if (HitAWall(enemy)) {
          enemy.MoveBack();
          s = s*(-1);
      }
      if (s == 1) {
          enemy.GoDown();
      }
      else {
          enemy.GoUp();
      }
      pb.Location = new Point((int)enemy.Position.x, (int)enemy.Position.y);
      return s;
    }

    private void EnemyVision(Enemy enemy, PictureBox pb, int SWITCH) {
      int s = SWITCH;
      Rectangle rect;
      if (s == 1) {
          rect = new Rectangle((int)enemy.Position.x, (int)enemy.Position.y, pb.Width, 150);
      } 
      else { 
          rect = new Rectangle((int)enemy.Position.x, (int)enemy.Position.y - 150 + pb.Height, pb.Width, 150);
      }
      if (CharWasSeen(player.Collider.rect, rect) && enemy.Health > 0) {
          Fight(enemy);
      }
      this.Invalidate();
    }

    private void tmrEnemyMove_Tick(object sender, EventArgs e) {
      SWITCH1 = EnemyMovement(enemyPoisonPacket, picEnemyPoisonPacket, SWITCH1);
      SWITCH2 = EnemyMovement(enemyCheeto, picEnemyCheeto, SWITCH2);
    }   

    private void tmrEnemyLook_Tick(object sender, EventArgs e) {
      EnemyVision(enemyPoisonPacket, picEnemyPoisonPacket, SWITCH1);
      EnemyVision(enemyCheeto, picEnemyCheeto, SWITCH2);
    }

    private Collider CreateCollider(PictureBox pic, int padding) {
      Rectangle rect = new Rectangle(pic.Location, new Size(pic.Size.Width - padding, pic.Size.Height - padding));
      return new Collider(rect);
    }

    private void FrmLevel_KeyUp(object sender, KeyEventArgs e) {
      if (!SlipperyFlag)
          player.ResetMoveSpeed();
      }

    private void tmrUpdateInGameTime_Tick(object sender, EventArgs e) {
      TimeSpan span = DateTime.Now - timeBegin;
      string time = span.ToString(@"hh\:mm\:ss");
      lblInGameTime.Text = "Time: " + time.ToString();
      if (InteractionPossible) {
          if (OnDamageTile(player)) {
              player.AlterHealth(-1);
              InteractionPossible = false;
          }
          if (OnTeleportTile(player)) {
              player.GoToPosition(teleportingtiles[TeleportIndex % teleportingtiles.Length].Position);
              TeleportIndex++;
              InteractionPossible = false;
          }
          if (OnSpawnTile(player))
          {
            picSword.Dispose();
            inventorySword.AddToQuantity(1);
            InteractionPossible = false;
          }
      }
    }
    
    private bool CharWasSeen(Rectangle you, Rectangle rec) {
      return you.IntersectsWith(rec);
    }

    private void Fight(Enemy enemy) {
      player.ResetMoveSpeed();
      player.MoveBack();
      axWindowsMediaPlayer1.Ctlcontrols.stop();
      frmBattle = FrmBattle.GetInstance(enemy);
      frmBattle.FormClosed += new FormClosedEventHandler(this.FrmBattle_Closed);
      frmBattle.Show();
    }
    
    private void FrmBattle_Closed(object sender, FormClosedEventArgs e)
    {
      axWindowsMediaPlayer1.Ctlcontrols.play();
    }

    private void Talk()
    {
        player.ResetMoveSpeed();
        player.MoveBack();
        Dialog();
        inventoryHeal.setQuantity(4);
        picNPCVillager.Dispose();
    }

    private void Dialog()
    {
        label2.Text = "Hi [PLAYER]! Here is a refill of Health Potions for your troubles. Good luck!";
        label2.Show();
        var t = new Timer();
        t.Interval = 4000; // it will Tick in 4 seconds
        t.Tick += (s, e) =>
        {
            label2.Hide();
            t.Stop();
        };
        t.Start();
    }

    private void tmrPlayerMove_Tick(object sender, EventArgs e) {
      // move player
      if (!SlowFlag && OnSlowingTile(player)) {
          player.SetGO_INC(2);
          SlowFlag = true;
      }
      else if(SlowFlag && !OnSlowingTile(player)) {
           player.SetGO_INC(4);
           SlowFlag = false;
      }

      player.Move();

      if (HitAChar(player, enemyPoisonPacket) && enemyPoisonPacket.Health > 0)
           Fight(enemyPoisonPacket);
      if (HitAChar(player, enemyCheeto) && enemyCheeto.Health > 0)
           Fight(enemyCheeto);
      if (HitAChar(player, bossKoolaid) && bossKoolaid.Health > 0)
           Fight(bossKoolaid);

      if (CharWasSeen(player.Collider.rect, npcVillager.Collider.rect))
            Talk();

      if (enemyPoisonPacket.Health <= 0)
           picEnemyPoisonPacket.Dispose();
      if (enemyCheeto.Health <= 0)
           picEnemyCheeto.Dispose();
      if (bossKoolaid.Health <= 0)
           picBossKoolAid.Dispose();
      if (player.Health <= 0)
           Application.Restart();

      // check collision with walls
      if (HitAWall(player))
           player.MoveBack();

      if (!OnSlipperyTile(player) && SlipperyFlag) {
           player.ResetMoveSpeed();
           SlipperyFlag = false;
      }

      // update player's picture box
      picPlayer.Location = new Point((int)player.Position.x, (int)player.Position.y);
      float playerHealthPer = player.Health / (float)player.MaxHealth;
      const int MAX_HEALTHBAR_WIDTH = 226;
      lblPlayerHealthFull.Width = (int)(MAX_HEALTHBAR_WIDTH * playerHealthPer);
    }

    private void tmrSpecialInteraction_Tick(object sender, EventArgs e) {
      InteractionPossible = true;
    }

    private bool HitAWall(Character c) {
      bool hitAWall = false;
      for (int w = 0; w < walls.Length; w++) {
           if (c.Collider.Intersects(walls[w].Collider)) {
               hitAWall = true;
               break;
           }
      }
      return hitAWall;
    }
    
    private bool OnTeleportTile(Character c) {
      bool onteleporttile = false;
      for (int w = 0; w < teleportingtiles.Length; w++) {
           if (c.Collider.Intersects(teleportingtiles[w].Collider)) {
               onteleporttile = true;
               break;
           }
      }
      if (onteleporttile)
            InteractionPossible = false;
      return onteleporttile;
    } 

    private bool OnDamageTile(Character c) {
      bool ondamagetile = false;
      for (int w = 0; w < damagingtiles.Length; w++) {
          if (c.Collider.Intersects(damagingtiles[w].Collider)) {
              ondamagetile = true;
              break;
          }
      }
      if (ondamagetile)
           InteractionPossible = false;
      return ondamagetile;
    }
    
    private bool OnSlipperyTile(Character c) {
      bool onslipperytile = false;
      for (int w = 0; w < slipperytiles.Length; w++) {
           if (c.Collider.Intersects(slipperytiles[w].Collider)) {
               onslipperytile = true;
               break;
           }
      }
      if (onslipperytile) 
           SlipperyFlag = true;
      return onslipperytile;
    }
    
    private bool OnSlowingTile(Character c) {
      bool onslowingtile = false;
      for (int w = 0; w < slowingtiles.Length; w++) {
          if (c.Collider.Intersects(slowingtiles[w].Collider)) {
              onslowingtile = true;
              break;
          }
      }
      if (onslowingtile)
          SlowFlag = true;
      return onslowingtile;
    }

    private bool OnSpawnTile(Character c)
    {
        bool onspawntile = false;
        for (int w = 0; w < spawningtiles.Length; w++)
        {
            if (c.Collider.Intersects(spawningtiles[w].Collider))
            {
                onspawntile = true;
                break;
            }
        }
        if (onspawntile)
            InteractionPossible = false;
        return onspawntile;
    }

    private bool HitAChar(Character you, Character other) {
        return you.Collider.Intersects(other.Collider);
    }

    private void FrmLevel_KeyDown(object sender, KeyEventArgs e) {
      switch (e.KeyCode) {
          case Keys.Left:
              player.GoLeft();
              break;

          case Keys.Right:
              player.GoRight();
              break;

          case Keys.Up:
              player.GoUp();
              break;

          case Keys.Down:
              player.GoDown();
              break;

          default:
              player.ResetMoveSpeed();
              break;
      }
    }

    private String ConvertCharToBinary(char c){
      switch (c) {
          case '0': return "00000";
          case '1': return "00010";
          case '2': return "00100";
          case '3': return "00110";
          case '4': return "01000"; 
          case '5': return "01001"; // does not follow any binary method (spawn block for grass texture) 
          case '6': return "01100";
          case '7': return "01110";
          case '8': return "10000";
          case '9': return "10010";
          case 'A': return "10100";
          case 'B': return "10110";
          case 'C': return "11000";
          case 'D': return "11011";
          case 'E': return "11100";
          case 'F': return "11110";
          case 'G': return "00001";
          default: return "00000";
      }
    }
  }
}