using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum;
using MonoGameGum.Forms;
using MonoGameGum.Forms.Controls;
using MonoGameGum.Forms.DefaultVisuals;
using MonoGameGum.GueDeriving;
using RenderingLibrary.Graphics;
using System;
using System.Diagnostics;


namespace wpf_to_gum
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        GumService Gum => GumService.Default;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 1000;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Gum.Initialize(this);

            // Create the main panel that everything is added to
            var mainPanel = new StackPanel();
            mainPanel.Visual.X = 10;
            mainPanel.Visual.Y = 10;
            mainPanel.Spacing = 5;
            mainPanel.AddToRoot();

            // Create a sub-panel that is stacked LEFT-TO-RIGHT
            var leftToRight = new StackPanel();
            leftToRight.Spacing = 20;
            leftToRight.Visual.ChildrenLayout = ChildrenLayout.LeftToRightStack;
            mainPanel.AddChild(leftToRight);

            // Add 3 stack panel "columns"
            var mainPanelLeft = new StackPanel();
            mainPanelLeft.Spacing = 5;
            leftToRight.AddChild(mainPanelLeft);

            var mainPanelMiddle = new StackPanel();
            mainPanelMiddle.Spacing = 5;
            leftToRight.AddChild(mainPanelMiddle);

            var mainPanelRight = new StackPanel();
            mainPanelRight.Spacing = 5;
            leftToRight.AddChild(mainPanelRight);

            // Perform different WPF like actions

            BasicXamlButton(mainPanelLeft);
            TextBlockWPFConverted(mainPanelLeft);
            TextBoxInlineFormatting(mainPanelLeft);
            TextBoxTutorial(mainPanelLeft);

            ButtonTutorial(mainPanelMiddle);
            CheckBoxTutorial(mainPanelMiddle);

            RadioButtonTutorial(mainPanelRight);
            PasswordBoxTutorial(mainPanelRight);

            MultilinTextBoxTutorial(mainPanelRight);

            Styling.Colors.Primary = new Color(255, 0, 0);

            base.Initialize();
        }

        private void BasicXamlButton(StackPanel panelToAddTo)
        {
            // https://wpf-tutorial.com/xaml/basic-xaml/

            Button button = new Button();                       // Button btn = new Button();                    
            button.Text = "";
            button.Dock(Dock.SizeToChildren);
            button.Width = 10;
            button.Height = 5;

            var stackPanel = new StackPanel();
            stackPanel.Visual.Anchor(Anchor.Center);
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Spacing = 5;

            var txt = new TextRuntime();                        // TextBlock txt = new TextBlock();
            txt.Text = "Multi";                                 // txt.Text = "Multi";
            txt.Color = Color.Blue;                             // txt.Foreground = Brushes.Blue;
            txt.IsBold = true;                                  // btn.FontWeight = FontWeights.Bold;
            txt.Width = 0;
            txt.WidthUnits = DimensionUnitType.RelativeToChildren;
            stackPanel.AddChild(txt);                           // pnl.Children.Add(txt);

            txt = new TextRuntime();                            // txt = new TextBlock();
            txt.Text = "Color";                                 // txt.Text = "Color";
            txt.Color = Color.Red;                              // txt.Foreground = Brushes.Red;
            txt.IsBold = true;                                  // btn.FontWeight = FontWeights.Bold;
            txt.Width = 0;
            txt.WidthUnits = DimensionUnitType.RelativeToChildren;
            stackPanel.AddChild(txt);                            // pnl.Children.Add(txt);

            txt = new TextRuntime();                            // txt = new TextBlock();
            txt.Text = "Button";                                // txt.Text = "Color";
            txt.IsBold = true;                                  // btn.FontWeight = FontWeights.Bold;
            txt.Width = 0;
            txt.WidthUnits = DimensionUnitType.RelativeToChildren;
            stackPanel.AddChild(txt);                           // pnl.Children.Add(txt);

            button.AddChild(stackPanel);
            panelToAddTo.AddChild(button);
        }

        private void TextBlockWPFConverted(StackPanel panelToAddTo)
        {
            // https://wpf-tutorial.com/basic-controls/the-textblock-control/

            // NOTE: COntainer is just for visual to make sure the border is showing
            var container = new ContainerRuntime();
            container.Width = 300;
            container.Height = 200;
            panelToAddTo.AddChild(container);

            var ColoredRectangleInstance = new ColoredRectangleRuntime();
            ColoredRectangleInstance.Color = Color.LightGray;
            ColoredRectangleInstance.Height = 0f;
            ColoredRectangleInstance.HeightUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToContainer;
            ColoredRectangleInstance.Width = 0f;
            ColoredRectangleInstance.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToContainer;
            container.AddChild(ColoredRectangleInstance);

            var ColoredRectangleInstance1 = new ColoredRectangleRuntime();
            ColoredRectangleInstance1.Color = Color.White;
            ColoredRectangleInstance1.Height = -20f;
            ColoredRectangleInstance1.HeightUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToContainer;
            ColoredRectangleInstance1.Width = -20f;
            ColoredRectangleInstance1.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToContainer;
            ColoredRectangleInstance1.X = 0f;
            ColoredRectangleInstance1.XOrigin = global::RenderingLibrary.Graphics.HorizontalAlignment.Center;
            ColoredRectangleInstance1.XUnits = global::Gum.Converters.GeneralUnitType.PixelsFromMiddle;
            ColoredRectangleInstance1.Y = 0f;
            ColoredRectangleInstance1.YOrigin = global::RenderingLibrary.Graphics.VerticalAlignment.Center;
            ColoredRectangleInstance1.YUnits = global::Gum.Converters.GeneralUnitType.PixelsFromMiddle;
            container.AddChild(ColoredRectangleInstance1);


            var stackpanel = new StackPanel();
            stackpanel.Visual.Height = 200;
            stackpanel.Visual.Width = 300;
            stackpanel.Visual.WidthUnits = DimensionUnitType.Absolute;
            container.AddChild(stackpanel);

            // Create a 10 pixel border control
            var innerContainer = new StackPanel();
            innerContainer.Visual.Dock(Dock.Fill);
            innerContainer.Visual.Width = -20;
            innerContainer.Visual.Height = -20;
            innerContainer.Spacing = 10; // Add the 10pixel margin between the textruntimes also
            stackpanel.AddChild(innerContainer);

            var textRuntime = new TextRuntime();
            textRuntime.Text = "This is a TextBlock control\nwith multiple lines of text.";
            textRuntime.Color = Color.Red;
            textRuntime.FontSize = -100;
            innerContainer.AddChild(textRuntime);

            // Anyway to make this better??
            var textRuntime1 = new TextRuntime();
            textRuntime1.Text = "This is a TextBlock control with text that may not be rendered completely, which will be indicated with an ellipsis.";
            textRuntime1.WidthUnits = DimensionUnitType.RelativeToParent;
            textRuntime1.TextOverflowHorizontalMode = RenderingLibrary.Graphics.TextOverflowHorizontalMode.EllipsisLetter;
            textRuntime1.TextOverflowVerticalMode = RenderingLibrary.Graphics.TextOverflowVerticalMode.TruncateLine;
            textRuntime1.HeightUnits = DimensionUnitType.Absolute;
            textRuntime1.Height = 21; // at 20 or less, the line disappears // this fudge guess is annoying
            textRuntime1.Color = Color.Green;
            innerContainer.AddChild(textRuntime1);

            var textRuntime2 = new TextRuntime();
            textRuntime2.Text = "This is a TextBlock control with automatically wrapped text, using the TextWrapping property.";
            textRuntime2.Color = Color.Blue;
            textRuntime2.WidthUnits = DimensionUnitType.RelativeToParent;
            textRuntime2.TextOverflowHorizontalMode = RenderingLibrary.Graphics.TextOverflowHorizontalMode.EllipsisLetter;
            innerContainer.AddChild(textRuntime2);
        }

        private void TextBoxInlineFormatting(StackPanel panelToAddTo)
        {
            // https://wpf-tutorial.com/basic-controls/the-textblock-control-inline-formatting/

            var inlineStackpanel = new StackPanel();
            panelToAddTo.AddChild(inlineStackpanel);

            var textInlineFormatted = new TextRuntime();
            // None of these work with CodeOnly because the FONT doesn't exist.  Also, there is apparently no underline?
            textInlineFormatted.Text = "TextBlock with [IsBold=true]bold[/IsBold], [IsItalic=true]italic[/IsItalic] and underlined text.";
            inlineStackpanel.AddChild(textInlineFormatted);

            var textInlineFormatted2 = new TextRuntime();
            // no hyperlink exists
            textInlineFormatted2.Text = "This text has a link in it.";
            inlineStackpanel.AddChild(textInlineFormatted2);

            var textInlineFormatted3 = new TextRuntime();
            // bold doesn't work, italic doesn't work, underline doesn't work.
            textInlineFormatted3.Text = "This [IsBold=true]is[/IsBold] a TextBlock with several [IsItalic=true]Span[/IsItalic] elements,\n[Color=blue]using a [IsBold=true]variety[/IsBold] of styles.[/Color]";
            inlineStackpanel.AddChild(textInlineFormatted3);

            // https://wpf-tutorial.com/basic-controls/the-label-control/
            // I'm going off the rails a bit here because this is a bit more unique.

            var labelPanelOutterFixedSize = new StackPanel();
            labelPanelOutterFixedSize.Visual.Width = 300;
            labelPanelOutterFixedSize.Visual.Height = 130;
            labelPanelOutterFixedSize.Visual.WidthUnits = DimensionUnitType.Absolute;
            panelToAddTo.AddChild(labelPanelOutterFixedSize);

            var labelPanel = new StackPanel();
            labelPanel.Visual.Dock(Dock.Fill);
            labelPanel.Visual.Width = -20;
            labelPanel.Visual.Height = -20;
            labelPanel.Spacing = 7;
            labelPanelOutterFixedSize.AddChild(labelPanel);

            var row1Panel = new StackPanel();
            row1Panel.Visual.ChildrenLayout = ChildrenLayout.LeftToRightStack;
            row1Panel.Visual.WidthUnits = DimensionUnitType.RelativeToParent;
            row1Panel.Spacing = 5;
            labelPanel.AddChild(row1Panel);

            var nameSprite = new SpriteRuntime();
            nameSprite.SourceFileName = "green_circle.png";
            row1Panel.AddChild(nameSprite);

            var nameLabel = new Label();
            nameLabel.Text = "Name:";
            row1Panel.AddChild(nameLabel);

            var nameText = new TextBox();
            nameText.Placeholder = "";
            nameText.Visual.WidthUnits = DimensionUnitType.RelativeToParent;
            nameText.Visual.Width = 0;
            ApplyStateMagicColors(nameText);  // 24 lines of code to just "color" the textbox

            labelPanel.AddChild(nameText);

            var row2Panel = new StackPanel();
            row2Panel.Visual.ChildrenLayout = ChildrenLayout.LeftToRightStack;
            row2Panel.Visual.WidthUnits = DimensionUnitType.RelativeToParent;
            row2Panel.Spacing = 5;
            labelPanel.AddChild(row2Panel);

            var mailSprite = new SpriteRuntime();
            mailSprite.SourceFileName = "blue_circle.png";
            row2Panel.AddChild(mailSprite);

            var mailLabel = new TextRuntime();
            mailLabel.Text = "Mail:";
            row2Panel.AddChild(mailLabel);

            var mailText = new TextBox();
            mailText.Placeholder = "";
            mailText.Visual.WidthUnits = DimensionUnitType.RelativeToParent;
            mailText.Visual.Width = 0;
            ApplyStateMagicColors(mailText);  // 24 lines of code to just "color" the textbox

            labelPanel.AddChild(mailText);
        }
        private void TextBoxTutorial(StackPanel panelToAddTo)
        {
            //https://wpf-tutorial.com/basic-controls/the-textbox-control/
            var textBoxPanel = new StackPanel();
            textBoxPanel.Visual.Width = 300;
            textBoxPanel.Visual.WidthUnits = DimensionUnitType.Absolute;
            textBoxPanel.Spacing = 5;
            panelToAddTo.AddChild(textBoxPanel);


            var textBoxForSelection = new TextBox();
            textBoxForSelection.Visual.WidthUnits = DimensionUnitType.Absolute;
            textBoxForSelection.Visual.Width = 300;
            ApplyStateMagicColors(textBoxForSelection);
            textBoxPanel.AddChild(textBoxForSelection);


            var textStatus = new TextBox();
            textStatus.Text = $"Selection starts are character #" +
                "\nSelection is {} characters(s) long" +
                "\nSelection text: ";
            textStatus.Visual.WidthUnits = DimensionUnitType.Absolute;
            textStatus.Visual.Width = 300;
            textStatus.Visual.Height = 75;
            textStatus.IsEnabled = false;
            ApplyStateMagicColors(textStatus);

            textBoxForSelection.SelectionChanged += (sender, args) =>
            {
                var tb = (TextBox)sender;
                var start = tb.SelectionStart;
                var length = tb.SelectionLength;

                string selectedTxt = "";
                if (length > 0)
                {
                    selectedTxt = tb.Text.Substring(start, length);
                }

                textStatus.Text =
                    $"Selection starts at character #{start}; " +
                    $"\nSelection is {length} character(s) long; " +
                    $"\nSelection text: '{selectedTxt}'";
            };

            textBoxPanel.AddChild(textStatus);
        }

        private void ButtonTutorial(StackPanel panelToAddTo)
        {
            // https://wpf-tutorial.com/basic-controls/the-button-control/

            var buttonPanel = new StackPanel();
            buttonPanel.Spacing = 5;
            panelToAddTo.AddChild(buttonPanel);

            var buttonNormal = new Button();
            buttonNormal.Text = "Hello, world!";
            buttonNormal.Visual.Width = 220;
            ApplyStateChangesToButton(buttonNormal, Color.Black, Color.LightGray);
            buttonNormal.Click += (sender, args) =>
            {
                Debug.WriteLine("Hello, world!");
            };
            buttonPanel.AddChild(buttonNormal);

            var buttonFormattedDirect = new Button();
            ApplyStateChangesToButton(buttonFormattedDirect, Color.Blue, Color.Beige);
            buttonFormattedDirect.Text = "[Color=Blue]Formatted Button[/Color]";
            buttonFormattedDirect.Visual.Width = 220;
            buttonPanel.AddChild(buttonFormattedDirect);

            var buttonAdvancedFormatting = new Button();
            buttonAdvancedFormatting.Text = "Formatted [Color=Blue][IsBold=true]Button[/IsBold][/Color] [IsItalic=true][Various][/IsItalic]";
            buttonAdvancedFormatting.Visual.Width = 220;
            ApplyStateChangesToButton(buttonAdvancedFormatting, Color.Black, Color.LightGray);
            buttonPanel.AddChild(buttonAdvancedFormatting);

            var buttonImageQuestion = new SpriteRuntime();
            buttonImageQuestion.SourceFileName = "question_mark.png";
            buttonImageQuestion.Width = 24;
            buttonImageQuestion.Height = 24;
            buttonImageQuestion.XUnits = GeneralUnitType.PixelsFromMiddle;
            buttonImageQuestion.X = -(buttonImageQuestion.Width) - 25;
            buttonImageQuestion.YUnits = GeneralUnitType.PixelsFromMiddle;
            buttonImageQuestion.Y = -(buttonImageQuestion.Height) / 2;
            buttonImageQuestion.WidthUnits = DimensionUnitType.Absolute;
            buttonImageQuestion.HeightUnits = DimensionUnitType.Absolute;

            var buttonWithImage = new Button();
            buttonWithImage.AddChild(buttonImageQuestion);
            buttonWithImage.Visual.Width = 220;
            buttonWithImage.Text = "Help";
            ApplyStateChangesToButton(buttonWithImage, Color.Black, Color.LightGray);
            buttonPanel.AddChild(buttonWithImage);
        }

        private void CheckBoxTutorial(StackPanel panelToAddTo)
        {
            // https://wpf-tutorial.com/basic-controls/the-checkbox-control/

            var checkboxPanel = new StackPanel();
            checkboxPanel.Spacing = -3;
            panelToAddTo.AddChild(checkboxPanel);

            var checkboxLabel = new Label();
            checkboxLabel.Text = "Application Options";
            var checkboxLabelTextRuntime = checkboxLabel.GetVisual<TextRuntime>();
            checkboxLabelTextRuntime.Color = Color.Black;
            checkboxPanel.AddChild(checkboxLabel);

            var checkboxOne = new CheckBox();
            checkboxOne.Text = "[Color=DarkSlateGray]Enable feature ABC[/Color]";
            checkboxOne.Visual.Width = 210;
            checkboxPanel.AddChild(checkboxOne);

            var checkboxTwo = new CheckBox();
            checkboxTwo.Text = "[Color=DarkSlateGray]Enable feature XYZ[/Color]";
            checkboxTwo.IsChecked = true;
            checkboxTwo.Visual.Width = 210;
            checkboxPanel.AddChild(checkboxTwo);

            var checkboxThree = new CheckBox();
            checkboxThree.Text = "[Color=DarkSlateGray]Enable feature WWW[/Color]";
            checkboxThree.Visual.Width = 210;
            checkboxPanel.AddChild(checkboxThree);


            var checkboxPanelStyled = new StackPanel();
            checkboxPanelStyled.Spacing = -3;
            panelToAddTo.AddChild(checkboxPanelStyled);

            var checkboxLabelStyled = new Label();
            checkboxLabelStyled.Text = "[Color=black]Application Options[/Color]";
            var checkboxLabelTextRuntimeStyled = checkboxLabel.GetVisual<TextRuntime>();
            checkboxLabelTextRuntimeStyled.Color = Color.Black;
            checkboxPanelStyled.AddChild(checkboxLabelStyled);

            var checkboxOneStyled = new CheckBox();
            checkboxOneStyled.Text = "[Color=DarkSlateGray]Enable feature [Color=green]ABC[/Color][/Color]";
            checkboxOneStyled.Visual.Width = 210;
            checkboxPanelStyled.AddChild(checkboxOneStyled);

            var checkboxTwoStyled = new CheckBox();
            checkboxTwoStyled.Text = "[Color=DarkSlateGray]Enable feature [Color=black][IsBold=true]XYZ[/IsBold][/Color][/Color]";
            checkboxTwoStyled.IsChecked = true;
            checkboxTwoStyled.Visual.Width = 210;

            var checkboxImage = new SpriteRuntime();
            checkboxImage.SourceFileName = "question_bubble.png";
            checkboxImage.X = 190;
            checkboxTwoStyled.AddChild(checkboxImage);

            checkboxPanelStyled.AddChild(checkboxTwoStyled);

            var checkboxThreeStyled = new CheckBox();
            checkboxThreeStyled.Text = "[Color=DarkSlateGray]Enable feature [Color=blue]WWW[/Color][/Color]";
            checkboxThreeStyled.Visual.Width = 210;
            checkboxPanelStyled.AddChild(checkboxThreeStyled);


            var checkboxPanelEnableAll = new StackPanel();
            checkboxPanelEnableAll.Spacing = -3;
            panelToAddTo.AddChild(checkboxPanelEnableAll);

            var checkboxLabelEnableAll = new Label();
            checkboxLabelEnableAll.Text = "Application Options";
            var checkboxLabelTextRuntimeEnableAll = checkboxLabelEnableAll.GetVisual<TextRuntime>();
            checkboxLabelTextRuntimeEnableAll.Color = Color.Black;
            checkboxPanelEnableAll.AddChild(checkboxLabelEnableAll);

            var checkboxEnableAll = new CheckBox();
            checkboxEnableAll.Text = "[Color=DarkSlateGray]Enable All[/Color]";
            checkboxEnableAll.Visual.Width = 210;
            checkboxEnableAll.IsThreeState = true;
            checkboxEnableAll.IsChecked = null;
            checkboxPanelEnableAll.AddChild(checkboxEnableAll);

            var checkboxOneEnableAll = new CheckBox();
            checkboxOneEnableAll.Text = "[Color=DarkSlateGray]Enable feature ABC[/Color]";
            checkboxOneEnableAll.Visual.Width = 210;
            checkboxOneEnableAll.Visual.X = 20;
            checkboxPanelEnableAll.AddChild(checkboxOneEnableAll);

            var checkboxTwoEnableAll = new CheckBox();
            checkboxTwoEnableAll.Text = "[Color=DarkSlateGray]Enable feature XYZ[/Color]";
            checkboxTwoEnableAll.IsChecked = true;
            checkboxTwoEnableAll.Visual.Width = 210;
            checkboxTwoEnableAll.Visual.X = 20;
            checkboxPanelEnableAll.AddChild(checkboxTwoEnableAll);

            var checkboxThreeEnableAll = new CheckBox();
            checkboxThreeEnableAll.Text = "[Color=DarkSlateGray]Enable feature WWW[/Color]";
            checkboxThreeEnableAll.Visual.Width = 210;
            checkboxThreeEnableAll.Visual.X = 20;
            checkboxPanelEnableAll.AddChild(checkboxThreeEnableAll);

            
            checkboxEnableAll.Checked += (sender, args) =>
            {
                checkboxOneEnableAll.IsChecked = (sender as CheckBox).IsChecked;
                checkboxTwoEnableAll.IsChecked = (sender as CheckBox).IsChecked;
                checkboxThreeEnableAll.IsChecked = (sender as CheckBox).IsChecked;
            };

            checkboxEnableAll.Unchecked += (sender, args) =>
            {
                checkboxOneEnableAll.IsChecked = (sender as CheckBox).IsChecked;
                checkboxTwoEnableAll.IsChecked = (sender as CheckBox).IsChecked;
                checkboxThreeEnableAll.IsChecked = (sender as CheckBox).IsChecked;
            };
        }

        private void RadioButtonTutorial(StackPanel panelToAddTo)
        {
            var radioLabel = new Label();
            radioLabel.Text = "Are you ready?";
            var checkboxLabelTextRuntimeEnableAll = radioLabel.GetVisual<TextRuntime>();
            checkboxLabelTextRuntimeEnableAll.Color = Color.Black;
            panelToAddTo.AddChild(radioLabel);

            var radioPanel = new StackPanel();
            radioPanel.Spacing = -5;
            panelToAddTo.AddChild(radioPanel);

            var radioButtonYes = new RadioButton();
            radioButtonYes.Text = "Yes";
            radioPanel.AddChild(radioButtonYes);

            var radioButtonNo = new RadioButton();
            radioButtonNo.Text = "No";
            radioButtonNo.IsChecked = true;
            radioPanel.AddChild(radioButtonNo);

            var radioButtonMaybe = new RadioButton();
            radioButtonMaybe.Text = "Maybe";
            radioPanel.AddChild(radioButtonMaybe);


            var radioLabelSex = new Label();
            radioLabelSex.Text = "Male or Female?";
            var checkboxLabelSexTextRuntimeEnableAll = radioLabelSex.GetVisual<TextRuntime>();
            checkboxLabelSexTextRuntimeEnableAll.Color = Color.Black;
            panelToAddTo.AddChild(radioLabelSex);

            var radioPanelSex = new StackPanel();
            radioPanelSex.Spacing = -5;
            panelToAddTo.AddChild(radioPanelSex);

            var radioButtonSexMale = new RadioButton();
            radioButtonSexMale.Text = "Male";
            radioButtonSexMale.IsChecked = true; // this somehow overrides the ischecked above for the other group of 3 // https://github.com/vchelaru/Gum/issues/725
            radioPanelSex.AddChild(radioButtonSexMale);

            var radioButtonSexFemale = new RadioButton();
            radioButtonSexFemale.Text = "Female";
            radioPanelSex.AddChild(radioButtonSexFemale);

            var radioButtonSexNotSure = new RadioButton();
            radioButtonSexNotSure.Text = "Not sure";
            radioPanelSex.AddChild(radioButtonSexNotSure);


            var radioLabelStyled = new Label();
            radioLabelStyled.Text = "[Color=black]Are you Ready?[/Color]";
            panelToAddTo.AddChild(radioLabelStyled);

            var radioPanelStyled = new StackPanel();
            radioPanelStyled.Spacing = -5;
            panelToAddTo.AddChild(radioPanelStyled);

            var checkboxImage = new SpriteRuntime();
            checkboxImage.SourceFileName = "green_checkmark.png";
            checkboxImage.X = 30;

            var radioStyledYes = new RadioButton();
            radioStyledYes.Text = "        [Color=green]Yes[/Color]";
            radioStyledYes.IsChecked = true; // this somehow overrides the ischecked above for the other group of 3 // https://github.com/vchelaru/Gum/issues/725
            radioStyledYes.AddChild(checkboxImage);
            radioPanelStyled.AddChild(radioStyledYes);

            checkboxImage = new SpriteRuntime();
            checkboxImage.SourceFileName = "red_x.png";
            checkboxImage.X = 30;

            var radioStyledNo = new RadioButton();
            radioStyledNo.Text = "        [Color=red]No[/Color]";
            radioStyledNo.AddChild(checkboxImage);
            radioPanelStyled.AddChild(radioStyledNo);

            checkboxImage = new SpriteRuntime();
            checkboxImage.SourceFileName = "question_bubble.png";
            checkboxImage.X = 30;

            var radioStyledMaybe = new RadioButton();
            radioStyledMaybe.Text = "        [Color=gray]Maybe[/Color]";
            radioStyledMaybe.Visual.Width = 150;
            radioStyledMaybe.AddChild(checkboxImage);
            radioPanelStyled.AddChild(radioStyledMaybe);
        }
        
        private void PasswordBoxTutorial(StackPanel panelToAddTo)
        {
            //https://wpf-tutorial.com/basic-controls/the-passwordbox-control/

            FormsUtilities.Keyboard.RepeatDelay = TimeSpan.FromMilliseconds(500);
            FormsUtilities.Keyboard.RepeatRate = TimeSpan.FromMilliseconds(70);

            var passwordBoxPanel = new StackPanel();
            passwordBoxPanel.Visual.Width = 300;
            passwordBoxPanel.Visual.Height = 0;
            passwordBoxPanel.Visual.WidthUnits = DimensionUnitType.Absolute;
            panelToAddTo.AddChild(passwordBoxPanel);

            var textRadioPanel = new StackPanel();
            textRadioPanel.Visual.ChildrenLayout = ChildrenLayout.LeftToRightStack;
            passwordBoxPanel.AddChild(textRadioPanel);

            var textLabel = new TextRuntime();
            textLabel.Text = "[Color=black]Text:[/Color]";
            textLabel.Width = 100;
            textLabel.WidthUnits = DimensionUnitType.Absolute;
            textRadioPanel.AddChild(textLabel);

            var shortLength = new RadioButton();
            shortLength.Text = "6";
            shortLength.Width = 50;
            textRadioPanel.AddChild(shortLength);
            var mediumLength = new RadioButton();
            mediumLength.Text = "14";
            mediumLength.Width = 50;
            mediumLength.IsChecked = true;
            textRadioPanel.AddChild(mediumLength);
            var longLength = new RadioButton();
            longLength.Text = "30";
            longLength.Width = 50;
            textRadioPanel.AddChild(longLength);

            var textText = new TextBox();
            textText.Placeholder = "";
            textText.Visual.WidthUnits = DimensionUnitType.RelativeToParent;
            textText.Visual.Width = 0;
            ApplyStateMagicColors(textText);  // 24 lines of code to just "color" the textbox

            passwordBoxPanel.AddChild(textText);

            var passwordCharRadioPanel = new StackPanel();
            passwordCharRadioPanel.Visual.ChildrenLayout = ChildrenLayout.LeftToRightStack;
            passwordBoxPanel.AddChild(passwordCharRadioPanel);

            var passwordLabel = new TextRuntime();
            passwordLabel.Text = "[Color=black]Password:[/Color]";
            passwordLabel.Width = 100;
            passwordLabel.WidthUnits = DimensionUnitType.Absolute;
            passwordCharRadioPanel.AddChild(passwordLabel);

            var xRadio = new RadioButton();
            xRadio.Text = "X";
            xRadio.Width = 50;
            passwordCharRadioPanel.AddChild(xRadio);
            var starRadio = new RadioButton();
            starRadio.Text = "*";
            starRadio.Width = 50;
            starRadio.IsChecked = true;
            passwordCharRadioPanel.AddChild(starRadio);
            var dashRadio = new RadioButton();
            dashRadio.Text = "-";
            dashRadio.Width = 50;
            //dashRadio.IsChecked = true;
            passwordCharRadioPanel.AddChild(dashRadio);

            var passwordText = new PasswordBox();
            passwordText.Placeholder = "";
            passwordText.Visual.WidthUnits = DimensionUnitType.RelativeToParent;
            passwordText.Visual.Width = 0;
            ApplyStateMagicColors(passwordText);  // 24 lines of code to just "color" the textbox

            passwordText.PasswordChanged += (sender, args) =>
            {
                textText.Text = passwordText.Password;
            };

            xRadio.Checked += (sender, args) =>
            {
                passwordText.PasswordChar = (sender as RadioButton).Text.ToCharArray()[0];
                passwordText.UpdateState();
            };
            starRadio.Checked += (sender, args) =>
            {
                passwordText.PasswordChar = (sender as RadioButton).Text.ToCharArray()[0];
                passwordText.UpdateState();
            };
            dashRadio.Checked += (sender, args) =>
            {
                passwordText.PasswordChar = (sender as RadioButton).Text.ToCharArray()[0];
                passwordText.UpdateState();
            };

            shortLength.Checked += (sender, args) =>
            {
                passwordText.MaxLength = int.Parse((sender as RadioButton).Text);
                passwordText.UpdateState();
            };
            mediumLength.Checked += (sender, args) =>
            {
                passwordText.MaxLength = int.Parse((sender as RadioButton).Text);
                passwordText.UpdateState();
            };
            longLength.Checked += (sender, args) =>
            {
                passwordText.MaxLength = int.Parse((sender as RadioButton).Text);
                passwordText.UpdateState();
            };


            passwordBoxPanel.AddChild(passwordText);
        }

        private void MultilinTextBoxTutorial(StackPanel panelToAddTo)
        {
            var multiLinePanel = new StackPanel();
            multiLinePanel.Spacing = 2;
            panelToAddTo.AddChild(multiLinePanel);

            var label = new Label();
            label.Width = 200;
            multiLinePanel.AddChild(label);

            var leftToRight = new StackPanel();
            leftToRight.Visual.ChildrenLayout = ChildrenLayout.LeftToRightStack;
            leftToRight.Spacing = 3;
            multiLinePanel.AddChild(leftToRight);

            var startSelection = new Button();
            startSelection.Text = "Start=5";
            leftToRight.AddChild(startSelection);

            var lengthSelection = new Button();
            lengthSelection.Text = "Length=5";
            leftToRight.AddChild(lengthSelection);

            var maxLinesButton = new Button();
            maxLinesButton.Text = "Toggle Max Lines = 3 more lines test";
            leftToRight.AddChild(maxLinesButton);

            var lettersToDisplayTextBox = new TextBox();
            lettersToDisplayTextBox.Text = null; // display all
            lettersToDisplayTextBox.Placeholder = "How many letters to display?";
            lettersToDisplayTextBox.Width = 250;
            multiLinePanel.AddChild(lettersToDisplayTextBox);

            var multiLineTextBox = new TextBox();
            multiLineTextBox.TextWrapping = TextWrapping.Wrap;
            multiLineTextBox.Height = 140;
            multiLineTextBox.Width = 250;
            multiLineTextBox.Text = "one\ntwo\nthree\nfour\nfive\nsix\nseven";
            multiLineTextBox.MaxNumberOfLines = 3;
            multiLineTextBox.AcceptsReturn = true;
            multiLinePanel.AddChild(multiLineTextBox);

            multiLineTextBox.SelectionChanged += (sender, args) =>
            {
                var start = (sender as TextBox).SelectionStart;
                var length = (sender as TextBox).SelectionLength;

                var selection = (sender as TextBox).Text.Substring(start, length);
                label.Text = $"Start [{start}] length [{length}] selection [{selection}]";
            };

            startSelection.Click += (sender, args) =>
            {
                multiLineTextBox.SelectionStart = 5;
            };

            lengthSelection.Click += (sender, args) =>
            {
                multiLineTextBox.SelectionLength = 5; // (5-=10)
            };

            maxLinesButton.Click += (sender, args) =>
            {
                if (multiLineTextBox.MaxNumberOfLines == null)
                    multiLineTextBox.MaxNumberOfLines = 3;
                else
                    multiLineTextBox.MaxNumberOfLines = null;
            };

            lettersToDisplayTextBox.TextChanged += (sender, args) =>
            {
                int howMany = 0;
                var result = int.TryParse((sender as TextBox).Text, out howMany);
                if (result)
                {
                    multiLineTextBox.MaxLettersToShow = howMany;
                }
                else
                {
                    multiLineTextBox.MaxLettersToShow = null;
                }
            };
        }


        private void ApplyStateMagicColors(FrameworkElement textbox)
        {
            TextBoxBaseVisual textboxVisual = (TextBoxBaseVisual)textbox.Visual;


            textboxVisual.States.Enabled.Clear();
            textboxVisual.States.Enabled.Apply = () =>
            {
                textboxVisual.Background.Color = Color.White;
                textboxVisual.TextInstance.Color = Color.Black;
                textboxVisual.FocusedIndicator.Visible = false;
            };

            textboxVisual.States.Highlighted.Clear();
            textboxVisual.States.Highlighted.Apply = () =>
            {
                textboxVisual.Background.Color = Color.Gray;
                textboxVisual.TextInstance.Color = Color.Black;
                textboxVisual.FocusedIndicator.Visible = false;
            };

            textboxVisual.States.Focused.Clear();
            textboxVisual.States.Focused.Apply = () =>
            {
                textboxVisual.Background.Color = Color.Tan;
                textboxVisual.TextInstance.Color = Color.Black;
                textboxVisual.FocusedIndicator.Color = Color.Red;
                textboxVisual.FocusedIndicator.Visible = true;
            };

            textbox.IsFocused = true;


            textbox.UpdateState();
        }


        private void ApplyStateChangesToButton(Button button, Color fontColor, Color enabledBackColor)
        {
            button.IsFocused = true; // Testing the visual for this, not needed

            var buttonVisual = (ButtonVisual)button.Visual;

            buttonVisual.States.Enabled.Clear();
            buttonVisual.States.Enabled.Apply = () =>
            {
                buttonVisual.Background.Color = enabledBackColor;
                buttonVisual.TextInstance.Color = fontColor;
            };

            buttonVisual.States.Highlighted.Clear();
            buttonVisual.States.Highlighted.Apply = () =>
            {
                buttonVisual.Background.Color = Color.DarkGray;
                buttonVisual.TextInstance.Color = fontColor;
            };

            buttonVisual.States.Pushed.Clear();
            buttonVisual.States.Pushed.Apply = () =>
            {
                buttonVisual.Background.Color = Color.SlateGray;
                buttonVisual.TextInstance.Color = fontColor;
            };

            button.UpdateState();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Gum.Update(gameTime);   

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Gum.Draw();

            base.Draw(gameTime);
        }
    }
}
