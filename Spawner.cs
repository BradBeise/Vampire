using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Spawner : Node3D
{
	[Export]
	private PackedScene Enemy;

    private int EnemiesToSpawn;
	private int EnemiesKilledThisWave;

	private List<Node> Waves = new List<Node>();
	private Wave CurrentWave;
	private int CurrentWaveNum = -1;
    
	private Timer Timer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Timer = GetNode<Timer>("Timer");

        Waves.AddRange(GetNode("Waves").GetChildren().ToList());

        StartNextWave();
    }

    private void StartNextWave()
    {
        CurrentWaveNum++;
		CurrentWave = (Wave) Waves[CurrentWaveNum];

		if (CurrentWaveNum < Waves.Count)
		{
            EnemiesToSpawn = CurrentWave.NumEnemies;
            EnemiesKilledThisWave = 0;

            Timer.WaitTime = CurrentWave.SecBetweenSpawn;
            Timer.Start();
        }
    }

    public void OnTimerTimeout()
	{
		if (EnemiesToSpawn > 0)
		{
            var enemy = Enemy.Instantiate<enemy>();

			ConnectToEnemySignal(enemy);

            var sceneRoot = GetParent();
            sceneRoot.AddChild(enemy);

            EnemiesToSpawn--;
        }
		else
		{
			if (EnemiesKilledThisWave == CurrentWave.NumEnemies)
			{
				StartNextWave();
			}
		}
	}

    private void ConnectToEnemySignal(enemy enemy)
    {
		var statsNode = enemy.GetNode<Stats>("Stats");

		statsNode.Connect("CharacterDeath", new Callable(this, nameof(OnEnemyDeath)));
    }

    private void OnEnemyDeath()
    {
		EnemiesKilledThisWave++;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
