namespace ZoologicoPOO.Models
{
    public abstract class Funcionario
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Cargo { get; set; }

        public Funcionario(string nome, int idade, string cargo)
        {
            Nome = nome;
            Idade = idade;
            Cargo = cargo;
        }

        public abstract void Trabalhar();
    }
}