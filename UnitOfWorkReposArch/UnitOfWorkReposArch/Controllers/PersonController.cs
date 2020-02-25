using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UoWR.Arch.Core.UnitOfWork;
using UoWR.Arch.Domain;
using UoWR.Arch.Models.PersonModel;

namespace UoWR.Arch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PersonController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public Person Get(int id)
        {
            return _unitOfWork.Repository<Person>().GetById(id);
        }

        [HttpPost]
        public void Post(PersonDTO person)
        {
            _unitOfWork.Repository<Person>().Insert(_mapper.Map<Person>(person));
            _unitOfWork.Save();
        }
    }
}