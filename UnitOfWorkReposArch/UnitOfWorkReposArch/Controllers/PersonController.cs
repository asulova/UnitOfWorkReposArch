using Microsoft.AspNetCore.Mvc;
using UoWR.Arch.Core.UnitOfWork;
using UoWR.Arch.Domain;

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
        public void Post(Person person)
        {
            _unitOfWork.Repository<Person>().Insert(person);
        }
    }
}