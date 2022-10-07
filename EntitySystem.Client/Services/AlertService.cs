using System;
using System.Collections.Generic;
using System.Linq;

namespace EntitySystem.Client.Services;

public enum AlertType
{
    Error,
    Success,
    Info,
    Warning
}

public record AlertEntry(AlertType Type, string Text);

public class AlertService : IAlertService
{
    private readonly Dictionary<Guid, AlertEntry> _dictionary = new();

    public event Action OnChange;

    public IEnumerable<KeyValuePair<Guid, AlertEntry>> GetAll()
    {
        return _dictionary;
    }

    public void Add(AlertType type, string text)
    {
        if (text.FirstOrDefault() == '"' && text.LastOrDefault() == '"') text = text[1..^1];

        if (text.Length > 1000) text = $"{text[..1000]}...";

        _dictionary.Add(Guid.NewGuid(), new AlertEntry(type, text));

        OnChange?.Invoke();
    }

    public void Remove(Guid guid)
    {
        _dictionary.Remove(guid);
    }
}