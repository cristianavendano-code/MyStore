using Microsoft.EntityFrameworkCore;
using MyStore.Data;
using MyStore.Interfaces;
using MyStore.Models;

namespace MyStore.Repository
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly AppDbContext _context;

        public MembershipRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool AddMembership(Membership membership)
        {
            _context.Memberships.Add(membership);
            return Save();
        }

        public Membership? GetMembershipById(int id)
        {
            return _context.Memberships.FirstOrDefault(m => m.Id == id);
        }

        public ICollection<Membership> GetMemberships()
        {
            return _context.Memberships.OrderBy(m => m.Id).ToList();
        }

        public bool UpdateMembership(Membership membership)
        {
            _context.Update(membership);
            return Save();
        }

        public bool DeleteMembership(Membership membership)
        {
            _context.Remove(membership);
            return Save();
        }
    }
}
