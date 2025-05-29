namespace ZoologicoPOO.Models
{
    public class Reptil : Animal
    {
        public Reptil(string nome, int idade, double peso, string especie) 
            : base(nome, idade, peso, especie)
        {
        }

        public override void EmitirSom()
        {
            Console.WriteLine($"{Nome} está emitindo um som típico de réptil.");
        }

        public override void Movimentar()
        {
            Console.WriteLine($"{Nome} está rastejando ou andando.");
        }
    }
}