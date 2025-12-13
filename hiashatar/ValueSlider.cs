using Godot;
using System;

public partial class ValueSlider : HSlider
{
	private void OnValueChanged(float value) 
	{
		TooltipText = value.ToString();
	}
}
