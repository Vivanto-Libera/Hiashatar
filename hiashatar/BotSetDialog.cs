using Godot;
using System;

public partial class BotSetDialog : Window
{
	[Signal]
	public delegate void SetSavedEventHandler(int sims, float tau, float cpuct);
	public void SetValues(int sims, float tau, float cpuct) 
	{
		GetNode<SpinBox>("Sims").Value = sims;
		GetNode<HSlider>("Tau").Value = tau;
		GetNode<HSlider>("Cpuct").Value = cpuct;
	}
	private void OnSavePressed() 
	{
		EmitSignal(SignalName.SetSaved, GetNode<SpinBox>("Sims").Value, GetNode<HSlider>("Tau").Value, GetNode<HSlider>("Cpuct").Value);
		Hide();
	}
	private void OnCancelPressed() 
	{
		Hide();
	}

}
