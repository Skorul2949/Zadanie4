using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YourNamespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllAnimals()
        {
            return Ok(Database.AnimalsController._animals);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetAnimalById(int id)
        {
            var animal = Database.AnimalsController._animals.FirstOrDefault(a => a.Id == id);
            return animal == null ? NotFound($"Zwierzę o identyfikatorze {id} nie zostało znalezione") : Ok(animal);
        }

        [HttpPost]
        public IActionResult AddAnimal([FromBody] Animal animal)
        {
            if (animal == null) return BadRequest("Dane zwierzęcia są wymagane");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            animal.Id = Database.AnimalsController._animals.Any() 
                ? Database.AnimalsController._animals.Max(a => a.Id) + 1 
                : 1;
                
            Database.AnimalsController._animals.Add(animal);
            
            return CreatedAtAction(nameof(GetAnimalById), new { id = animal.Id }, animal);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateAnimal(int id, [FromBody] Animal animal)
        {
            if (animal == null || animal.Id != id) return BadRequest();
            
            var existingAnimal = Database.AnimalsController._animals.FirstOrDefault(a => a.Id == id);
            if (existingAnimal == null) return NotFound();

            existingAnimal.Name = animal.Name;
            existingAnimal.Category = animal.Category;
            existingAnimal.Weight = animal.Weight;
            existingAnimal.FurColor = animal.FurColor;

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteAnimal(int id)
        {
            var animal = Database.AnimalsController._animals.FirstOrDefault(a => a.Id == id);
            if (animal == null) return NotFound();

            Database.AnimalsController._animals.Remove(animal);
            Database.AnimalsController._visits.RemoveAll(v => v.AnimalId == id);
            return NoContent();
        }

        [HttpGet("search")]
        public IActionResult SearchAnimalsByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Parametr nazwa jest wymagany");

            var results = Database.AnimalsController._animals
                .Where(a => a.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(results);
        }

        [HttpGet("{animalId:int}/visits")]
        public IActionResult GetAnimalVisits(int animalId)
        {
            if (!Database.AnimalsController._animals.Any(a => a.Id == animalId))
                return NotFound($"Zwierzę o identyfikatorze {animalId} nie zostało znalezione");

            var visits = Database.AnimalsController._visits.Where(v => v.AnimalId == animalId).ToList();
            return Ok(visits);
        }

        [HttpPost("{animalId:int}/visits")]
        public IActionResult AddAnimalVisit(int animalId, [FromBody] Visit visit)
        {
            if (!Database.AnimalsController._animals.Any(a => a.Id == animalId))
                return NotFound($"Zwierzę o identyfikatorze {animalId} nie zostało znalezione");

            if (visit == null) return BadRequest("Dane wizyty są wymagane");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            visit.Id = Database.AnimalsController._visits.Any() 
                ? Database.AnimalsController._visits.Max(v => v.Id) + 1 
                : 1;
                
            visit.AnimalId = animalId;
            Database.AnimalsController._visits.Add(visit);

            return CreatedAtAction(
                nameof(GetAnimalVisits), 
                new { animalId = animalId }, 
                visit);
        }
    }
}