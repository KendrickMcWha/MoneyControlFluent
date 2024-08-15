using Microsoft.FluentUI.AspNetCore.Components;

namespace MoneyControl.FluentUi.Builders;

public class DialogParametersBuilder
{
    private readonly DialogParameters _dialogParameters = new() { PreventDismissOnOverlayClick = true, PreventScroll = true };

    public DialogParameters Build() => _dialogParameters;

    public DialogParametersBuilder WithHeight(int value)
    {
        _dialogParameters.Height = $"{value.ToString()}px";
        return this;
    }
    public DialogParametersBuilder WithWidth(int value)
    {
        _dialogParameters.Width = $"{value.ToString()}px";
        return this;
    }
    public DialogParametersBuilder WithTitle(string value)
    {
        _dialogParameters.Title = value;
        return this;
    }
}