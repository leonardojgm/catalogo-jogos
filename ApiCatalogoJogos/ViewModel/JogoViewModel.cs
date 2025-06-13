namespace ApiCatalogoJogos.ViewModel
{
    public class JogoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Produtora { get; set; } = string.Empty;
        public double Preco { get; set; }
    }
}