﻿using System;
using System.Windows.Input;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using MudBlazor.Utilities;
using MudBlazor.Extensions;

namespace MudBlazor
{
    public class IconButtonBase : ComponentBase
    {
        protected string Classname =>
        new CssBuilder("mud-button-base-root mud-iconbutton-root")
          .AddClass($"mud-iconbutton-color-{Color.ToDescriptionString()}")
          .AddClass($"mud-iconbutton-size-{Size.ToDescriptionString()}", when: () => Size != Size.Medium)
          .AddClass($"mud-iconbutton-edge-{Edge.ToDescriptionString()}", when: () => !String.IsNullOrEmpty(Edge.ToDescriptionString()))
          .AddClass(Class)
        .Build();

        [Inject] public Microsoft.AspNetCore.Components.NavigationManager UriHelper { get; set; }

        [Parameter] public Color Color { get; set; } = Color.Default;

        [Parameter] public Size Size { get; set; } = Size.Medium;

        [Parameter] public Edge Edge { get; set; }

        [Parameter] public string Class { get; set; }

        [Parameter] public bool Disabled { get; set; }

        [Parameter] public string Link { get; set; }

        [Parameter] public bool ForceLoad { get; set; }

        [Parameter] public ICommand Command { get; set; }

        [Parameter] public object CommandParameter { get; set; }

        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected async Task OnClickHandler(MouseEventArgs ev)
        {
            if (Link != null)
            {
                UriHelper.NavigateTo(Link , ForceLoad);
            }
            else
            {
                await OnClick.InvokeAsync(ev);
                if (Command?.CanExecute(CommandParameter) ?? false)
                {
                    Command.Execute(CommandParameter);
                }
            }
        }
    }
}
