using Fall2020_CSC403_Project.code;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project {
  public partial class FrmLevel : Form {
    private Player player;

    private Enemy enemyPoisonPacket;
    private Enemy bossKoolaid;
    private Enemy enemyCheeto;
    private Character[] walls;

    private HealthPotion healthPotion;

    private Sword sword;

    private Inventory inventoryHeal;
    private Inventory inventorySword;

    private DateTime timeBegin;
    private FrmBattle frmBattle;

    static int SWITCH1 = 1;
    static int SWITCH2 = 1;

    public FrmLevel() {
      InitializeComponent();
    }

    private void FrmLevel_Load(object sender, EventArgs e) {
      const int PADDING = 7;
      const int NUM_WALLS = 13;

      player = new Player(CreatePosition(picPlayer), CreateCollider(picPlayer, PADDING));
      bossKoolaid = new Enemy(CreatePosition(picBossKoolAid), CreateCollider(picBossKoolAid, PADDING));
      enemyPoisonPacket = new Enemy(CreatePosition(picEnemyPoisonPacket), CreateCollider(picEnemyPoisonPacket, PADDING));
      enemyCheeto = new Enemy(CreatePosition(picEnemyCheeto), CreateCollider(picEnemyCheeto, PADDING));
      healthPotion = new HealthPotion("Health Potion", player);
      sword = new Sword("Sword", player, 3); 
      inventoryHeal = new Inventory(healthPotion, 3);
      inventorySword = new Inventory(sword, 1);

      bossKoolaid.Img = picBossKoolAid.BackgroundImage;
      enemyPoisonPacket.Img = picEnemyPoisonPacket.BackgroundImage;
      enemyCheeto.Img = picEnemyCheeto.BackgroundImage;

      bossKoolaid.Color = Color.Red;
      enemyPoisonPacket.Color = Color.Green;
      enemyCheeto.Color = Color.FromArgb(255, 245, 161);

      walls = new Character[NUM_WALLS];
      for (int w = 0; w < NUM_WALLS; w++) {
        PictureBox pic = Controls.Find("picWall" + w.ToString(), true)[0] as PictureBox;
        walls[w] = new Character(CreatePosition(pic), CreateCollider(pic, PADDING));
      }

      Game.player = player;
      Game.healthPotion = healthPotion;
      Game.sword = sword;
      Game.inventoryHeal = inventoryHeal;
      Game.inventorySword = inventorySword;
      timeBegin = DateTime.Now;
    }

    private Vector2 CreatePosition(PictureBox pic) {
      return new Vector2(pic.Location.X, pic.Location.Y);
    }

    private Collider CreateCollider(PictureBox pic, int padding) {
      Rectangle rect = new Rectangle(pic.Location, new Size(pic.Size.Width - padding, pic.Size.Height - padding));
      return new Collider(rect);
    }

    private void FrmLevel_KeyUp(object sender, KeyEventArgs e) {
      player.ResetMoveSpeed();
    }

    private void tmrUpdateInGameTime_Tick(object sender, EventArgs e) {
      TimeSpan span = DateTime.Now - timeBegin;
      string time = span.ToString(@"hh\:mm\:ss");
      lblInGameTime.Text = "Time: " + time.ToString();
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

    private void tmrPlayerMove_Tick(object sender, EventArgs e) {
      // move player
      player.Move();

      // check collision with walls
      if (HitAWall(player)) {
        player.MoveBack();
      }

      // check collision with enemies
      if (HitAChar(player, enemyPoisonPacket) && enemyPoisonPacket.Health > 0) {
        Fight(enemyPoisonPacket);
      }
      if (HitAChar(player, enemyCheeto) && enemyCheeto.Health > 0) {
        Fight(enemyCheeto);
      }
      if (HitAChar(player, bossKoolaid) && bossKoolaid.Health > 0) {
        Fight(bossKoolaid);
      }

      // check health of enemies and player
      if (enemyPoisonPacket.Health <= 0) { 
        picEnemyPoisonPacket.Dispose();
      }
      if (enemyCheeto.Health <= 0) { 
        picEnemyCheeto.Dispose();
      }
       if (bossKoolaid.Health <= 0) { 
        picBossKoolAid.Dispose();
      }
      if (player.Health <= 0) {
        Application.Restart();
      }

      // update player's picture box
      picPlayer.Location = new Point((int)player.Position.x, (int)player.Position.y);

        // update health bar
        float playerHealthPer = player.Health / (float)player.MaxHealth;
        const int MAX_HEALTHBAR_WIDTH = 226;
        lblPlayerHealthFull.Width = (int)(MAX_HEALTHBAR_WIDTH * playerHealthPer);
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

    private bool HitAChar(Character you, Character other) {
      return you.Collider.Intersects(other.Collider);
    }

    private bool CharWasSeen(Rectangle you, Rectangle rec) {
      return you.IntersectsWith(rec);
    }

    private void Fight(Enemy enemy) {
      player.ResetMoveSpeed();
      player.MoveBack();
      frmBattle = FrmBattle.GetInstance(enemy);
      frmBattle.Show();

      if (enemy == bossKoolaid) {
        frmBattle.SetupForBossBattle();
      }
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
  }
}
