using System;
using System.Collections.Generic;
using System.Linq;
using ErrorCodes;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace PersonRepository
{
    public class PersonRep : IPersonRep, IDisposable
    {
        private readonly transfersystemContext db;
        public PersonRep(transfersystemContext curDb)
        {
            db = curDb;
        }
        public Person Add(Person element)
        {
            element.Address ??= "None";
            element.Name ??= "None";
            element.Work ??= "None";
            element.Age ??= 18;
            db.ChangeTracker.Clear();
            db.Person.Attach(element);
            db.Person.Add(element);
            db.SaveChanges();
            return element;
            
        }
        public List<Person> GetAll()
        {
            IQueryable<Person> users = db.Person;
            return users == null || users.Count() == 0 ? null : users.ToList();
        }
        public Person Update(Person element)
        {
            Person person = db.Person.Find(element.Id);
            if (person != null)
            {
                person.Name = element.Name ?? person.Name;
                person.Address = element.Address ?? person.Address;
                person.Work = element.Work ?? person.Work;
                person.Age = element.Age ?? person.Age;
                db.ChangeTracker.Clear();
                db.Person.Attach(person);
                db.Person.Update(person);
                db.SaveChanges();
                return person;
            }
            else
            {
                return null;
            }
            
        }
        
        public ErrorCode Delete(long id)
        {
            Person element = db.Person.Find(id);
            if (element == null)
            {
                return ErrorCode.NotFound;
            }
            db.ChangeTracker.Clear();
            db.Person.Attach(element);
            db.Person.Remove(element);
            db.SaveChanges();
            return ErrorCode.OK;
            
        }
        
        public Person FindUserByID(long id)
        {
            
            Person person = db.Person.Find(id);
            db.ChangeTracker.Clear();
            return person;
        }

        
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
