using System;
using ZoologicoPOO.Interfaces;

namespace ZoologicoPOO.Models
{
    public class Zelador : Funcionario, ICuidador
    {
        public Zelador(string nome, int idade)
            : base(nome, idade, "Zelador") { }

        public override void Trabalhar()
        {
            Console.WriteLine($"{Nome} está alimentando os animais e limpando os habitats.");
        }

        public void CuidarHabitat(Animal animal)
        {
            Console.WriteLine($"Zelador {Nome} cuidou do habitat do animal {animal.Nome}.");
        }

        public void AlimentarAnimal(Animal animal)
        {
            Console.WriteLine($"Zelador {Nome} alimentou o animal {animal.Nome} com sucesso.");
        }
    }
}