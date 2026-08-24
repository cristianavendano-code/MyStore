using MyStore.Models;

namespace MyStore.Interfaces
{
    public interface IMembershipRepository
    {
        ICollection<Membership> GetMemberships();
        Membership? GetMembershipById(int id);
        bool AddMembership(Membership membership);
        bool Save();
    }
}
