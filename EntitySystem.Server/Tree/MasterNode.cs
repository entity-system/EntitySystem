using NHibernate;

namespace EntitySystem.Server.Tree;

public delegate (ICriteria criteria, string field) QueryCreator(ISession session);

public class MasterNode : TableNode
{
    public QueryCreator Creator { get; set; }
}