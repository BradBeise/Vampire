using Godot;
using System;

public partial class GunController : Node
{
	[Export]
	private PackedScene StartingWeapon;

	Gun EquippedWeapon;
	private Marker3D Hand;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hand = (Marker3D) GetParent().FindChild("Hand");

		if (StartingWeapon != null)
		{
            EquipWeapon(StartingWeapon.Instantiate<Gun>());
		}
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

	public void Shoot()
	{
		if (EquippedWeapon != null)
		{
			EquippedWeapon.Shoot();
		}
	}

    private void EquipWeapon(Gun weapon)
    {
        if (EquippedWeapon != null)
		{
			EquippedWeapon.QueueFree();
		}

		GD.Print($"Equipping gun: {weapon}");
		EquippedWeapon = weapon;
		Hand.AddChild(EquippedWeapon);
    }
}
