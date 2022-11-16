using Fall2020_CSC403_Project.code;
using Fall2020_CSC403_Project.Properties;
using System;
using System.Threading.Tasks;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Drawing.Text;
using System.Media;
using System.Windows.Forms;
using System.Xml.Serialization;
using WMPLib;

namespace Fall2020_CSC403_Project {
    public partial class FrmBattle : Form {
        public static FrmBattle instance = null;
        private Enemy enemy;
        private Player player;
        private HealthPotion healthPotion;
        private Sword sword;
        private Inventory inventoryHeal;
        private Inventory inventorySword;
        public static int volume_level = 0;
        public static int charClass = 1;
        public static int skin = 0;



        private FrmBattle() {
            InitializeComponent();
            player = Game.player;
            healthPotion = Game.healthPotion;
            inventoryHeal = Game.inventoryHeal;
            inventorySword = Game.inventorySword;
            sword = Game.sword;
            volume_level = FrmLevel.volume_level;
            charClass = FrmLevel.charClass;
            skin = FrmLevel.skin;
        }

        public void Setup() {
            // update for this enemy
            label5.Text = "";
            picEnemy.BackgroundImage = enemy.Img;
            picEnemy.Refresh();
            BackColor = enemy.Color;
            picBossBattle.Visible = false;

            // update player picture
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

            // Observer pattern
            enemy.AttackEvent += PlayerDamage;
            player.AttackEvent += EnemyDamage;

            // check if sword durability is 0
            if (sword.Durability <= 0)
            {
                inventorySword.setQuantity(0);
            }

            if (sword.Durability > 0 & inventorySword.Quantity > 0) {
                picSword.Image = Properties.Resources.sword;
            }

            if (inventoryHeal.Quantity == 4)
            {
                picHealthPot1.Image = Properties.Resources.health_potion;
                picHealthPot2.Image = Properties.Resources.health_potion;
                picHealthPot3.Image = Properties.Resources.health_potion;
                picHealthPot4.Image = Properties.Resources.health_potion;
            }

            if (inventoryHeal.Quantity == 3) {
                picHealthPot1.Image = Properties.Resources.health_potion;
                picHealthPot2.Image = Properties.Resources.health_potion;
                picHealthPot3.Image = Properties.Resources.health_potion;
            }
            else if (inventoryHeal.Quantity == 2) {

                picHealthPot1.Image = Properties.Resources.health_potion;
                picHealthPot2.Image = Properties.Resources.health_potion;

            }
            else if (inventoryHeal.Quantity == 1) {

                picHealthPot1.Image = Properties.Resources.health_potion;
            }

            // show health
            UpdateHealthBars();
            axWindowsMediaPlayer1.URL = @"Battle_music_v2.wav";
            axWindowsMediaPlayer1.settings.playCount = 9999;
            axWindowsMediaPlayer1.settings.volume = volume_level;
        }

        public static FrmBattle GetInstance(Enemy enemy) {
            if (instance == null) {
                instance = new FrmBattle();
                instance.enemy = enemy;
                instance.Setup();
            }
            return instance;
        }

        private void UpdateHealthBars() {
            float playerHealthPer = player.Health / (float)player.MaxHealth;
            float enemyHealthPer = enemy.Health / (float)enemy.MaxHealth;

            const int MAX_HEALTHBAR_WIDTH = 226;
            lblPlayerHealthFull.Width = (int)(MAX_HEALTHBAR_WIDTH * playerHealthPer);
            lblEnemyHealthFull.Width = (int)(MAX_HEALTHBAR_WIDTH * enemyHealthPer);

            lblPlayerHealthFull.Text = player.Health.ToString();
            lblEnemyHealthFull.Text = enemy.Health.ToString();
        }

        private async void btnAttack_Click(object sender, EventArgs e) {
            if (inventorySword.Quantity > 0 && sword.Durability > 0)
            {
                sword.attackWithSwordLight(player);
                sword.subtractDurability(1);
                label9.Text = "Durability: " + sword.Durability;
                label6.Text = "You strike for 6 damage.";
                btnAttack.Hide();
                await Task.Delay(2000);
                label6.Text = "";
                btnAttack.Show();
            }

            else
            {
                player.OnAttack(-2);
                label6.Text = "You strike for 4 damage.";
                btnAttack.Hide();
                await Task.Delay(2000);
                label6.Text = "";
                btnAttack.Show();

            }

            if (enemy.Health > 0)
            {
                enemy.OnAttack(-2);
            }

            if (player.Health <= 0 || enemy.Health <= 0) {
                instance = null;
                EndBattle();
            }
            // remove sword image when sword breaks
            if (sword.Durability <= 0 && picSword.Image != null)
            {
                label9.Text = "";
                label7.Text = "Your sword has broken!";
                await Task.Delay(1000);
                label7.Text = "";
                picSword.Image = null;
            }

            UpdateHealthBars();
        }

        private async void btnHeal_Click(object sender, EventArgs e)
        {
            if (player.Health <= 15 & inventoryHeal.Quantity > 0)
            {
                healthPotion.useHealthPotion(player);
                inventoryHeal.DeleteFromQuantity(1);
            }
            else if (player.Health > 15 & player.Health < 20 & inventoryHeal.Quantity > 0)
            {
                healthPotion.useMaxHealthPotion(player);
                inventoryHeal.DeleteFromQuantity(1);
            }
            // display potions on screen in inventory
            if (inventoryHeal.Quantity == 3)
            {
                label8.Text = "You have 3 potions left.";
                btnHeal.Hide();
                await Task.Delay(1000);
                btnHeal.Show();
                label8.Text = "";
                picHealthPot4.Image = null;
            }
            else if (inventoryHeal.Quantity == 2)
            {
                label8.Text = "You have 2 potions left.";
                btnHeal.Hide();
                await Task.Delay(1000);
                btnHeal.Show();
                label8.Text = "";
                picHealthPot3.Image = null;
            }

            else if (inventoryHeal.Quantity == 1) {
                label8.Text = "You have 1 potion left.";
                btnHeal.Hide();
                await Task.Delay(1000);
                btnHeal.Show();
                label8.Text = "";
                picHealthPot2.Image = null;
            }

            else if (inventoryHeal.Quantity == 0) {
                label8.Text = "You are out of potions!";
                btnHeal.Hide();
                await Task.Delay(1000);
                btnHeal.Show();
                label8.Text = "";
                picHealthPot1.Image = null;
            }
               UpdateHealthBars();
        }

            private async void buttonStrong_Click(object sender, EventArgs e)
            {
                if (inventorySword.Quantity > 0 & sword.Durability == 3)
                {
                    sword.attackWithSwordHeavy(player);
                    sword.subtractDurability(3);
                    picSword.Image = Properties.Resources.health_potion;

                    if (enemy.Health > 0)
                    {
                        enemy.OnAttack(-2);
                    }

                    if (player.Health <= 0 || enemy.Health <= 0)
                    {
                        instance = null;
                        EndBattle();
                    }

                }
                else
                {
                    label7.Text = "Sword doesn't have enough durability!";
                    await Task.Delay(800);
                    label7.Text = "";
                }

                UpdateHealthBars();
            }
            private void EnemyDamage(int amount) {
                enemy.AlterHealth(amount);
            }

            private void PlayerDamage(int amount) {
                player.AlterHealth(amount);
            }

            private void tmrFinalBattle_Tick(object sender, EventArgs e) {
                picBossBattle.Visible = false;
                tmrFinalBattle.Enabled = false;
            }

            private void EndBattle() {
                Close();
            }
    }
    }

