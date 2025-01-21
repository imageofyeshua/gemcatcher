using Godot;
using System;

public partial class Game : Node2D
{
	const double GEM_MARGIN = 50.0;

	private static readonly AudioStream EXPLODE_SOUND =
					GD.Load<AudioStream>("res://assets/explode.wav");

	[Export] private PackedScene _gemScene;
	[Export] private Timer _spawnTimer;
	[Export] private Label _scoreLabel;
	[Export] private AudioStreamPlayer _music;
	[Export] private AudioStreamPlayer2D _effects;
	// [Export] private AudioStream _explodeSound;
	// [Export] private Gem _gem;
	// [Export] private NodePath _gemPath;
	// private Gem _gem;
	private int _score = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Gem gem = GetNode<Gem>("Gem");
		// _gem = GetNode<Gem>(_gemPath);
		// _gem.OnScored += OnScored;
		_spawnTimer.Timeout += SpawnGem;
		SpawnGem();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void SpawnGem()
	{
		Rect2 vpr = GetViewportRect();
		Gem gem = (Gem)_gemScene.Instantiate();
		AddChild(gem);

		float rX = (float)GD.RandRange(
			vpr.Position.X + GEM_MARGIN, vpr.End.X - GEM_MARGIN
		);

		gem.Position = new Vector2(rX, -100);
		gem.OnScored += OnScored;
		gem.OnGemOffSCreen += GameOver;
	}

	private void OnScored()
	{
		GD.Print("OnScored Received.");
		_score += 1;
		_scoreLabel.Text = $"{_score:0000}";
		_effects.Play();
	}

	private void GameOver()
	{
		GD.Print("GameOver");
		foreach (Node node in GetChildren())
		{
			node.SetProcess(false);
		}
		_spawnTimer.Stop();
		_music.Stop();

		_effects.Stop();
		_effects.Stream = EXPLODE_SOUND;
		_effects.Play();
	}
}
