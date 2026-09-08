using Microsoft.EntityFrameworkCore;
using MyStore.Data;
using MyStore.Interfaces;
using MyStore.Models;
using System.Xml.Linq;

namespace MyStore.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;
        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool AddClient(Client client)
        {
            _context.Clients.Add(client);
            return Save();
        }

        public bool DeleteClient(Client client)
        {
            _context.Clients.Remove(client);
            return Save();
        }

        public Client? GetClientByEmail(string email)
        {
            return _context.Clients.Include(c => c.Membership).FirstOrDefault(c => c.Email == email);
        }

        public Client? GetClientById(int id)
        {
            return _context.Clients.Include(c => c.Membership).FirstOrDefault(c => c.Id == id);
        }

        public Client? GetClientByName(string name)
        {
            return _context.Clients.Include(c => c.Membership).FirstOrDefault(c => c.Name == name);
        }

        public Client? GetClientByPhone(string phone)
        {
            return _context.Clients.Include(c => c.Membership).FirstOrDefault(c => c.Phone == phone);
        }

        public ICollection<Client> GetClients()
        {
            //ERROR AQUII
            return _context.Clients.Include(c => c.Membership).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;

        }

        public bool UpdateClient(Client client)
        {
            _context.Clients.Update(client);
            return Save();
        }
    }
}
