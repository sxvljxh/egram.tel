﻿using Avalonia.Markup.Xaml;
using Tel.Egram.Components.Messenger.Explorer.Messages;

namespace Tel.Egram.Gui.Views.Messenger.Explorer.Messages.Basic
{
    public class UnsupportedMessageControl : BaseControl<UnsupportedMessageModel>
    {
        public UnsupportedMessageControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}