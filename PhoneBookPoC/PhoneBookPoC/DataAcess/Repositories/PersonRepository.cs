using Microsoft.EntityFrameworkCore;
using PhoneBookPoC.DataAcess.DbContexts;
using PhoneBookPoC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookPoC.DataAcess.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IDbConfiguration _dbConfiguration;

        public PersonRepository(IDbConfiguration dbConfiguration)
        {
            _dbConfiguration = dbConfiguration;
        }

        public async Task<Person> AddPerson(Person person)
        {
            using (var context = new PhoneBookDbContext(_dbConfiguration))
            {
                person.Id = Guid.NewGuid();

                await context.People.AddAsync(person);
                await context.SaveChangesAsync();

                return person;
            }
        }

        public async Task DeletePerson(Guid personId)
        {
            using (var context = new PhoneBookDbContext(_dbConfiguration))
            {
                var exists = PersonExists(personId);
                if (exists)
                {
                    var person = await GetPersonById(personId);
                    if (person != null)
                    {
                        context.People.Remove(person);
                        await context.SaveChangesAsync();
                    }
                }
            }
        }

        public async Task<IEnumerable<Person>> GetPeople()
        {
            using (var context = new PhoneBookDbContext(_dbConfiguration))
            {
                return await context.People.ToListAsync();
            }
        }

        public async Task<Person> GetPersonById(Guid personId)
        {
            using (var context = new PhoneBookDbContext(_dbConfiguration))
            {
                return await context.People.FirstOrDefaultAsync(p => p.Id == personId);
            }
        }

        public async Task UpdatePerson(Person person)
        {
            using (var context = new PhoneBookDbContext(_dbConfiguration))
            {
                var entity = await context.People.FirstOrDefaultAsync(p => p.Id == person.Id);
                entity.FirstName = person.FirstName;
                entity.LastName = person.LastName;
                entity.Phone = person.Phone;

                context.People.Update(entity);

                await context.SaveChangesAsync();
            }
        }

        public bool PersonExists(Guid personId)
        {
            using (var context = new PhoneBookDbContext(_dbConfiguration))
            {
                return context.People.Any(p => p.Id == personId);
            }
        }
    }
}
