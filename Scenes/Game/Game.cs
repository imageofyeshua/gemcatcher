using Godot;
using System;

public partial class Game : Node2D
{
	[Export] private Gem _gem;
	// [Export] private NodePath _gemPath;
	// private Gem _gem;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Gem gem = GetNode<Gem>("Gem");
		// _gem = GetNode<Gem>(_gemPath);
		_gem.OnScored += OnScored;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnScored()
	{
		GD.Print("OnScored Received.");
	}
}
