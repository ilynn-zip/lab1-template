using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ErrorCodes;
using Microsoft.EntityFrameworkCore;

namespace PersonRepository
{
    [Route("api/v1/persons")]
    //[Route("[controller]")]
    [ApiController]
    public class PersonController: Controller
    {
        protected IPersonRep personRepository;

        protected Person _person;
        public PersonController(IPersonRep personRep)
        {
            personRepository = personRep;
        }

        /// <summary>
        /// Возвращает информацию о человеке
        /// </summary>
        /// <param name="personId">Идентификатор человека</param>
        /// <returns>Человека, соответствующее идентификатору</returns>
        /// <response code="200" cref="Person">Человек</response>
        /// <response code="404">Не найдено соответствующего человека</response>
        [IgnoreAntiforgeryToken]
        [HttpGet("{personId}")]
        //[ValidateAntiForgeryToken]
        public ActionResult FindPersonById(int personId)
        {
            Person person = personRepository.FindUserByID(personId);
            return person != null ? Ok(person) : NotFound();
        }

        /// <summary>
        /// Возвращает информацию о всех людях
        /// </summary>
        /// <returns>Всех людей</returns>
        /// <response code="200" cref="Person">Все люди</response>
        /// <response code="404">Не найдено людей</response>
        [IgnoreAntiforgeryToken]
        [HttpGet("")]
        //[ValidateAntiForgeryToken]
        public ActionResult FindAllPerson()
        {
            List<Person> persons = personRepository.GetAll();
            return persons != null && persons.Count() > 0 ? Ok(personRepository.GetAll()) : NotFound();
        }

        /// <summary>
        /// Создает запись о человеке
        /// </summary>
        /// <param name="personData">Информация о человеке</param>
        /// <returns>Созданная запись о человеке</returns>
        /// <response code="201">Запись о человеке создана</response>
        /// <response code="404">Не удалось создать</response>
        [IgnoreAntiforgeryToken]
        [HttpPost("")]
        //[ValidateAntiForgeryToken]
        public ActionResult PostPerson(Person person)
        {
            Person checkPerson = personRepository.Add(person);
            return new CreatedResult("http://www.myapi.com/api/clients/" + checkPerson.Id, null);
            
        }

        /// <summary>
        /// Изменяет информацию о человеке
        /// </summary>
        /// <param name="person">Информация о человеке</param>
        /// <returns>Человека, соответствующее идентификатору</returns>
        /// <response code="200" cref="Person">Запись о человеке</response>
        /// <response code="404">Не найдено соответствующего человека</response>
        [IgnoreAntiforgeryToken]
        [HttpPatch("{personId}")]
        //[ValidateAntiForgeryToken]
        public ActionResult UpdatePersonById(Person person)
        {
            Person checkPerson = personRepository.Update(person);
            return checkPerson != null ? Ok(checkPerson) : NotFound();
        }

        /// <summary>
        /// Удаляет информацию о человеке
        /// </summary>
        /// <param name="personId">Идентификатор человека</param>
        /// <returns>Человека, соответствующее идентификатору</returns>
        /// <response code="200" cref="Person">Запись о человеке удалена</response>
        /// <response code="404">Не найдено соответствующего человека</response>
        [IgnoreAntiforgeryToken]
        [HttpDelete("{personId}")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeletePersonById(long personId)
        {
            ErrorCode code = personRepository.Delete(personId);
            return code == ErrorCode.OK ? Ok(null) : NotFound();
        }

    }
}
