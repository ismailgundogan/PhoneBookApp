using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Core.Dtos;
using PhoneBook.Core.Entity;
using PhoneBook.DataAccess.Interfaces;

namespace PhoneBook.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonellerController : ControllerBase
    {
        //veri erişimleri için
        public readonly IPersonelRepository _personelRepository;
        public readonly IMapper _mapper;
        public PersonellerController(IPersonelRepository personelRepository, IMapper mapper)
        {
            _personelRepository = personelRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var personeller = await _personelRepository.GetAllAsync();
            var personelDtos = _mapper.Map<List<PersonelDto>>(personeller);
            throw new Exception("Bu bir test hatasıdır."); // Hata yakalama middleware'ini test etmek için
            return Ok(personelDtos);
        }

        // POST - Yeni personel ekle
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Personel personelDto)
        {
            var personel = _mapper.Map<Personel>(personelDto);
            await _personelRepository.AddAsync(personel);

            return CreatedAtAction(nameof(GetById), new { id = personel.Id }, personelDto);
        }

        // GET by ID - Tek personel getir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var personel = await _personelRepository.GetByIdAsync(id);
            if (personel == null) return NotFound();

            var personelDto = _mapper.Map<PersonelDto>(personel);
            return Ok(personelDto);
        }

        // PUT - Personel güncelle
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PersonelDto personelDto)
        {
            var personel = await _personelRepository.GetByIdAsync(id);
            if (personel == null) return NotFound();

            _mapper.Map(personelDto, personel);
            await _personelRepository.UpdateAsync(personel);
            return NoContent();
        }

        // DELETE - Personel sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var personel = await _personelRepository.GetByIdAsync(id);
            if (personel == null) return NotFound();

            await _personelRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
