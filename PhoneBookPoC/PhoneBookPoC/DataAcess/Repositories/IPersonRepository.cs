using PhoneBookPoC.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBookPoC.DataAcess.Repositories
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetPeople();
        Task<Person> GetPersonById(Guid personId);
        Task<Person> AddPerson(Person person);
        Task UpdatePerson(Person erson);
        Task DeletePerson(Guid personId);
        bool PersonExists(Guid personId);
    }
}
