using System;

public interface IBot
{
    BotStats BotStats { get; }
    BotState BotState { get; }
    event Action StateChanged;
    void Init(IBot bot);
}