using System;
using System.Collections.Generic;

namespace EntitySystem.Client.Services;

public interface IAlertService
{
    event Action OnChange;

    IEnumerable<KeyValuePair<Guid, AlertEntry>> GetAll();

    void Add(AlertType type, string text);

    void Remove(Guid guid);
}