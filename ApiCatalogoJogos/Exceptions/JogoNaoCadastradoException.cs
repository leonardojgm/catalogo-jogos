namespace ApiCatalogoJogos.Exceptions
{
    public class JogoNaoCadastradoException : Exception
    {
        public JogoNaoCadastradoException() : base("Este jogo já está cadastrado") { }
    }
}