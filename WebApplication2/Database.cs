using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Tutorial6;
using Tutorial6.Models;

namespace Tutorial6
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Weight { get; set; }
        public string FurColor { get; set; }
    }

    public class Visit
    {
        public int Id { get; set; }
        public DateTime VisitDate { get; set; }
        public int AnimalId { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
    public static class Database
    {   
    
        public class AnimalsController : ControllerBase
        {
            private static List<Animal> _animals = new List<Animal>
            {
                new Animal { Id = 1, Name = "Burek", Category = "Pies", Weight = 15.5, FurColor = "Brązowy" },
                new Animal { Id = 2, Name = "Mruczek", Category = "Kot", Weight = 4.2, FurColor = "Szary" },
                new Animal { Id = 3, Name = "Rex", Category = "Pies", Weight = 22.0, FurColor = "Czarny" }
            };

            private static List<Visit> _visits = new List<Visit>
            {
                new Visit { Id = 1, VisitDate = new DateTime(2023, 1, 10), AnimalId = 1, Description = "Szczepienie przeciwko wściekliźnie", Price = 120.0 },
                new Visit { Id = 2, VisitDate = new DateTime(2023, 2, 15), AnimalId = 1, Description = "Kontrola stanu zdrowia", Price = 80.0 },
                new Visit { Id = 3, VisitDate = new DateTime(2023, 3, 5), AnimalId = 2,
                }
}