using Godot;
using System;

public partial class Tutorial : Node
{
	public void SetButtonsVisible(bool visible) 
	{
		GetNode<Button>("Introduce").SetDeferred(Button.PropertyName.Visible, visible);
		GetNode<Button>("Author").SetDeferred(Button.PropertyName.Visible, visible);
		GetNode<Button>("BasicRule").SetDeferred(Button.PropertyName.Visible, visible);
		GetNode<Button>("DrawRule").SetDeferred(Button.PropertyName.Visible, visible);
		GetNode<Button>("HoundRule").SetDeferred(Button.PropertyName.Visible, visible);
		GetNode<Button>("TergeRule").SetDeferred(Button.PropertyName.Visible, visible);
		GetNode<Button>("HorseRule").SetDeferred(Button.PropertyName.Visible, visible);
		GetNode<Button>("CamelRule").SetDeferred(Button.PropertyName.Visible, visible);
		GetNode<Button>("GuardRule").SetDeferred(Button.PropertyName.Visible, visible);
		GetNode<Button>("LionRule").SetDeferred(Button.PropertyName.Visible, visible);
		GetNode<Button>("KhanRule").SetDeferred(Button.PropertyName.Visible, visible);
		GetNode<Button>("Exit").SetDeferred(Button.PropertyName.Visible, visible);
	}
}
