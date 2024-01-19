namespace UsuariosAPI.Model
{
    public class Token
    {
        public string Valor { get; }
        public Token(string valor)
        {
            Valor = valor;
        }
    }
}
