namespace ZoologicoPOO.Models
{
    public abstract class Animal
    {
        // Propriedades comuns a todos os animais
        public string Nome { get; set; }
        public int Idade { get; set; }
        public double Peso { get; set; }
        public string Especie { get; set; }

        // Construtor
        public Animal(string nome, int idade, double peso, string especie)
        {
            Nome = nome;
            Idade = idade;
            Peso = peso;
            Especie = especie;
        }

        // Métodos abstratos que serão obrigatórios nas subclasses
        public abstract void EmitirSom();
        public abstract void Movimentar();
    }
}