using System;
using ZoologicoPOO.Interfaces;

namespace ZoologicoPOO.Models
{
    public class Veterinario : Funcionario, ITratamentoAnimal
    {
        public Veterinario(string nome, int idade)
            : base(nome, idade, "Veterinário") { }

        public override void Trabalhar()
        {
            Console.WriteLine($"{Nome} está realizando uma consulta veterinária.");
        }

        public void RealizarTratamento(Animal animal)
        {
            Console.WriteLine($"Veterinário {Nome} realizou tratamento no animal {animal.Nome}.");
        }

        public void ConsultarAnimal(Animal animal)
        {
            Console.WriteLine($"Veterinário {Nome} consultou o animal {animal.Nome} com sucesso.");
        }
    }
}