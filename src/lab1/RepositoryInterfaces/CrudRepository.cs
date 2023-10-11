using System.Collections.Generic;
using ErrorCodes;
using System.Threading.Tasks;

namespace PersonRepository
{
    public interface CrudRepository<T>
    {
        T Add(T element);
        List<T> GetAll();
        T Update(T element);
        ErrorCode Delete(long id);
    }
}

