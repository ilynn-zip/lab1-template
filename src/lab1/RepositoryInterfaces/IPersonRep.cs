

namespace PersonRepository
{
    public interface IPersonRep : CrudRepository<Person>
    {
        Person FindUserByID(long id);
        
    }
}
