using System;
using EntitySystem.Shared.Query;
using NHibernate;

namespace EntitySystem.Server.Tree;

public delegate (ICriteria criteria, string childField) QueryJoiner(Guid guid, ICriteria criteria, string parentKey, string childProperty);

public class JoinNode : TableNode
{
    public QueryJoin QueryJoin { get; set; }
}