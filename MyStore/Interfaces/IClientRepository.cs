using MyStore.Models;

namespace MyStore.Interfaces
{
    public interface IClientRepository
    {
        ICollection<Client> GetClients();
        Client? GetClientById(int id);
        Client? GetClientByPhone(string phone);
        Client? GetClientByName(string name);
        bool AddClient(Client client);
        bool UpdateClient(Client client);
        bool DeleteClient(Client client);
        bool Save();

    }
}
