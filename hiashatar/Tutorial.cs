using Godot;
using System;

public partial class Tutorial : Node
{
	[Signal]
	public delegate void BasicRuleEventHandler();
	[Signal]
	public delegate void HoundEventHandler();
	[Signal]
	public delegate void TergeEventHandler();
	[Signal]
	public delegate void HorseEventHandler();
	[Signal]
	public delegate void CamelEventHandler();
	[Signal]
	public delegate void GuardEventHandler();
	[Signal]
	public delegate void LionEventHandler();
	[Signal]
	public delegate void KhanEventHandler();

	private bool isInTutorial = false;

	public bool IsInTutorial()
	{
		return isInTutorial;
	}

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
	public void SetState(bool inTutorial)
	{
		isInTutorial = inTutorial;
		SetButtonsVisible(!inTutorial);
		GetNode<Button>("Exit").SetDeferred(Button.PropertyName.Visible, true);
		SetTextVisible(inTutorial);
	}
	private void SetText(string text) 
	{
		GetNode<Label>("TutorialText").Text = text;
	}
	private void SetTextVisible(bool visible) 
	{
		GetNode<Label>("TutorialText").SetDeferred(Label.PropertyName.Visible, visible);
	}
	private void OnIntroducePressed()
	{
		SetState(true);
		SetText("蒙古大象棋（Hiashatar）是蒙古象棋（Shatar）的变种，主要体现在棋盘更大，以及多了侍卫棋子。\n\n蒙古大象棋起源于10~13世纪，并成型于13世纪左右。蒙古象棋和国际象棋一般被认为同源于印度的恰图兰卡。常被视为贵族教育工具，军事推演方式和冬季娱乐活动。\n\n蒙古象棋于2011年被列入中国国家级非物质文化遗产目录，至今仍在中国内蒙古自治区和蒙古国有着一定的影响力。");
	}
	private void OnAuthorPressed()
	{
		SetState(true);
		SetText("作者是一名对棋类感兴趣的大学生，并希望通过做棋类游戏的方式让更多人接触一些不为人知的棋类，让人们对棋类这项人类的古老的娱乐活动更感兴趣。\n\n我做这个游戏也是希望更多人了解蒙古族的这项传统游戏，让大家知道象棋还有这样一个变种。\n\n我的GitHub账号为Vivanto-Libera，邮箱是vivanto@qq.com。如果大家有什么建议可以和我交流，\n\n感谢您的游玩。");
	}
	private void OnBasicPressed()
	{
		SetState(true);
		SetText("棋子摆放如左边。白先行，目标是将死对方的可汗。\n\n以白方为例，前排为犬，后排从左到右依次为车，马，驼，卫，汗，狮。");
		EmitSignal(SignalName.BasicRule);
	}
	private void OnDrawPressed()
	{
		SetState(true);
		SetText("以下情况会导致和棋:\n\n局面重复三次。\n\n50步没有吃子或移动兵。\n\n一方无法移动，即不管怎么走都将导致被将军时。\n\n子力不足。（汗vs汗或汗vs汗+驼/马/卫或汗+同色驼vs汗+同色驼）");
	}
	private void OnHoundPressed()
	{
		SetState(true);
		SetText("犬在初始状态下可以向前移动1~3格，其余情况只可向前移动1格。\n\n犬只能吃前斜方一格的棋子。\n\n当上一步对方的犬走了2~3格时，且落点在你方犬旁边，可以吃过路兵（移动到对方犬后面并吃掉犬）。\n\n当犬到达底线时，自动升变为狮。");
		EmitSignal(SignalName.Hound);
	}
	private void OnTergePressed()
	{
		SetState(true);
		SetText("车可以横竖移动任意格，但不可跨过棋子。吃子和移动一样。");
		EmitSignal(SignalName.Terge);
	}
	private void OnHorsePressed()
	{
		SetState(true);
		SetText("马可以移动至竖着两格横着一格或者横着两格竖着一格的格子，可以跨过棋子。吃子和移动一样。");
		EmitSignal(SignalName.Horse);
	}
	private void OnCamelPressed()
	{
		SetState(true);
		SetText("驼可以斜着移动任意格，但不可跨过棋子，吃子和移动一样。");
		EmitSignal(SignalName.Camel);
	}
	private void OnGuardPressed() 
	{
		SetState(true);
		SetText("卫可以八方向移动1~2格,但不可跨过棋子。\n\n卫只能吃斜向一格的棋子。\n\n卫可以保护周围的八个格子，敌方棋子在卫周围只能移动一格（无法穿过）。");
		EmitSignal(SignalName.Guard);
	}
	private void OnLionPressed() 
	{
		SetState(true);
		SetText("狮可以八方向移动任意格，但不可跨过棋子，吃子和移动一样。");
		EmitSignal(SignalName.Lion);
	}
	private void OnKhanPressed() 
	{
		SetState(true);
		SetText("汗可以八方向移动一格，吃子和移动一样。\n\n汗如被将死，将会输掉游戏。");
		EmitSignal(SignalName.Khan);
	}
}
