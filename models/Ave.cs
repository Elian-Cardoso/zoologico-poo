namespace ZoologicoPOO.Models
{
    public class Ave : Animal
    {
        public Ave(string nome, int idade, double peso, string especie) 
            : base(nome, idade, peso, especie)
        {
        }

        public override void EmitirSom()
        {
            Console.WriteLine($"{Nome} está cantando.");
        }

        public override void Movimentar()
        {
            Console.WriteLine($"{Nome} está voando ou andando.");
        }
    }
}