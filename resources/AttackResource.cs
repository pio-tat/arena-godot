using Godot;
using System;

[GlobalClass]
public partial class AttackResource : Resource
{
    [Export] int damage;
    public int Damage {get => damage;}

    [Export] float attacksPerSecond;
    public float AttacksPerSecond {get => attacksPerSecond;}

    [Export] float attackDuration;
    public float AttackDuration {get => attackDuration;}
    
    [Export] AttackTypeEnum attackType;
    public AttackTypeEnum AttackType {get => attackType;}

    [Export] float bulletSpeed;
    public float BulletSpeed {
        get{
            if(attackType == AttackTypeEnum.Bullet)
                return bulletSpeed;
            return 0;
            // throw new Exception("Can't get the bullet speed of attack type Melee");
        }
    }
    [Export] Godot.Collections.Array<Effects> effects = new();
    public Godot.Collections.Array<Effects> Effects {get => effects;}

    public enum AttackTypeEnum
    {
        Bullet,
        Melee
    }

    [Export] Texture2D attackTexture;
    public Texture2D AttackTexture {get => attackTexture;}
}
