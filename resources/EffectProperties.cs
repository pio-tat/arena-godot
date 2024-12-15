using Godot;
using System;

[GlobalClass]
public partial class EffectProperties : Resource
{
    [Export] private Effects effect;
    public Effects Effect {get => effect;}
    [Export] private float duration;
    public float Duration {get => duration;}
    [Export] private float strength = 1;
    public float Strength {get => strength;}

    [Export] Godot.Collections.Array<Texture2D> particles;
    public Godot.Collections.Array<Texture2D> Particles {get => particles;}
}

public enum Effects
{
	none = 0,
	poison,
	slowness,
	weakness
}