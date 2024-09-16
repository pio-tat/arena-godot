using Godot;
using System;

public interface IHurtBox
{
    public void OnHit(int damage);
}
public interface IPlayer
{
    public PlayerStats PlayerStats {get;}
    public int PlayerId {get; set;}
}

//--------------------------------------
public partial class PlayerStats : Node2D
{   
    int health;
    public int maxHealth = 50;
    Node parent;
    ProgressBar healthBar;
    public ArenaManager arenaManager;

    public override void _Ready()
    {
        parent = GetParent();
        arenaManager = parent.GetParent() as ArenaManager;
        health = maxHealth;
        healthBar = parent.GetNode<ProgressBar>("HealthBar");
        healthBar.MaxValue = maxHealth;
    }

    public void SetHealth(int value)
    {
        if(value < maxHealth)
            health = value;
        else
            health = maxHealth;
        
        healthBar.Value = health;
    }
    public void Heal(int heal)
    {
        if(health + heal > maxHealth){
            health = maxHealth;
        }else
            health += heal;
        
        healthBar.Value = health;
    }
    public void Damage(int damage)
    {
        GD.Print("Hitted! " + health);
        if(health - damage > 0){
            health -= damage;
        }else{
            health = 0;
            Die();
        }
        healthBar.Value = health;
    }

    void Die()
    {
        arenaManager.RemovePlayerFromCamera(parent as IPlayer);
        parent.QueueFree();
    }
}