using Microsoft.AspNetCore.Mvc;
using UoWR.Arch.Core.UnitOfWork;
using UoWR.Arch.Domain;
using UoWR.Arch.Model.Person;

namespace UoWR.Arch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public Person Get(int id)
        {
            return _unitOfWork.Repository<Person>().GetById(id);
        }

        [HttpPost]
        public void Post(CreatePerson person)
        {
            _unitOfWork.Repository<Person>().Insert(new Person() { FirstName = person.FirstName, LastName = person.LastName });
            _unitOfWork.Save();
        }
    }
}