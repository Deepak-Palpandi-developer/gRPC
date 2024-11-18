using Gmail.Domain.Data;
using Gmail.Helpers;

namespace Gmail.Domain;

public interface IGmailUnitOfWork : IUnitOfWork
{

}
public class GmailUnitOfWork : UnitOfWork, IGmailUnitOfWork
{
    public GmailUnitOfWork(GmailContext context) : base(context)
    {
    }
}
