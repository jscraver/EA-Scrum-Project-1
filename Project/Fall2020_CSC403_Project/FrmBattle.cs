﻿using Fall2020_CSC403_Project.code;
using Fall2020_CSC403_Project.Properties;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Media;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project {
  public partial class FrmBattle : Form {
    public static FrmBattle instance = null;
    private Enemy enemy;
    private Player player;
    private HealthPotion healthPotion;
    private Sword sword;
    private Inventory inventoryHeal;
    private Inventory inventorySword;

    private FrmBattle() {
      InitializeComponent();
      player = Game.player;
      healthPotion = Game.healthPotion;
      inventoryHeal = Game.inventoryHeal;
      inventorySword = Game.inventorySword;
      sword = Game.sword;
    }

    public void Setup() {
      // update for this enemy
      picEnemy.BackgroundImage = enemy.Img;
      picEnemy.Refresh();
      BackColor = enemy.Color;
      picBossBattle.Visible = false;

      // Observer pattern
      enemy.AttackEvent += PlayerDamage;
      player.AttackEvent += EnemyDamage;

      // Initialize correct label text for health potion quantity
      label3.Text = String.Format("Left: {0}", inventoryHeal.Quantity);
      
      // check if sword durability is 0
      if (sword.Durability <= 0)
      {
        inventorySword.SetQuantity(0);
      }

    // show health
    UpdateHealthBars();
    }

    public void SetupForBossBattle() {
      picBossBattle.Location = Point.Empty;
      picBossBattle.Size = ClientSize;
      picBossBattle.Visible = true;

      SoundPlayer simpleSound = new SoundPlayer(Resources.final_battle);
      simpleSound.Play();

      tmrFinalBattle.Enabled = true;
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

    private void btnAttack_Click(object sender, EventArgs e) {
        if (inventorySword.Quantity > 0)
        {
            sword.attackWithSwordLight();
            sword.subtractDurability(1);
        }
        else
        {
            player.OnAttack(-2);
        }

        if (enemy.Health > 0)
        {
            enemy.OnAttack(-2);
        }
       
        if (player.Health <= 0 || enemy.Health <= 0) {
        instance = null;
        Close();
        }

        UpdateHealthBars();
    }
      
    private void btnHeal_Click(object sender, EventArgs e) {
       if (player.Health <= 15 & inventoryHeal.Quantity > 0) {
            healthPotion.useHealthPotion();
            inventoryHeal.DeleteFromQuantity(1);
       }
       else if (player.Health > 15 & player.Health < 20 & inventoryHeal.Quantity > 0) {
            healthPotion.useMaxHealthPotion();
            inventoryHeal.DeleteFromQuantity(1);
       }

       label3.Text = String.Format("Left: {0}", inventoryHeal.Quantity);

       UpdateHealthBars();
    }

    private void buttonStrong_Click(object sender, EventArgs e)
    {
        if (inventorySword.Quantity > 0 & sword.Durability == 3)
        {
            sword.attackWithSwordHeavy();
            sword.subtractDurability(3);
        
            if (enemy.Health > 0)
            {
                enemy.OnAttack(-2);
            }

            if (player.Health <= 0 || enemy.Health <= 0)
            {
                instance = null;
                Close();
            }

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

    }
}
