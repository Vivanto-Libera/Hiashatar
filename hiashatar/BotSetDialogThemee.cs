using Godot;
using System;

public partial class BotSetDialogThemee : Window
{
	[Signal]
	public delegate void SetThemeeSavedEventHandler(int depth);
	public void SetValues(int depth)
	{
		GetNode<SpinBox>("Depth").Value = depth;
	}
	private void OnSavePressed()
	{
		EmitSignal(SignalName.SetThemeeSaved, GetNode<SpinBox>("Depth").Value);
		Hide();
	}
	private void OnCancelPressed()
	{
		Hide();
	}
}
