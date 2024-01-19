using MimeKit;

namespace UsuariosAPI.Model
{
    
    public class Mensagem
    {
        public List<MailboxAddress> Destinatario { get; set; }

        public string Assunto {get;set;}

        public string Conteudo { get; set; }

        public Mensagem(IEnumerable<string> destinatarios, string assunto, int usuarioId,string codigo)
        {
            Destinatario = new List<MailboxAddress>();
            Destinatario.AddRange(destinatarios.Select(destinatario=> new MailboxAddress(destinatario)));
            Assunto = assunto;
            Conteudo = $"http://localhost:7105/ativa?UsuarioId={usuarioId}&CodigoAtivacao={codigo}";
        }




    }
}