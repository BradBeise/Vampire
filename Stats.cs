using Godot;
using System;

public partial class Stats : Node
{
	public int MaxHp = 100;
	public int CurrentHp;

    [Signal]
    public delegate void CharacterDeathEventHandler();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		CurrentHp = MaxHp;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void TakeDamage(int damage)
	{
		CurrentHp -= damage;
		GD.Print($"HP: {CurrentHp}");

		if (CurrentHp <= 0)
		{
            EmitSignal(SignalName.CharacterDeath);
        }
    }
}
