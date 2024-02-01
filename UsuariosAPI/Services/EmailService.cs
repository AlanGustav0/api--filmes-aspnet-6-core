using MailKit.Net.Smtp;
using MimeKit;
using UsuariosAPI.Model;

namespace UsuariosAPI.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        private string? _smtpServer;
        private int _port;
        private string? _from;
        private string? _password;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void EnviarEmail(string[] destinatario,string assunto,int usuarioId,string codigoAtivacao)
        {
            
            _port = int.Parse(_configuration["EmailSettings:Port"]);
            _from = _configuration["EmailSettings:From"];
            _password = _configuration["EmailSettings:Password"];
            _smtpServer = _configuration["SmtpServer"];
            Mensagem mensagem = new Mensagem(destinatario,assunto,usuarioId,codigoAtivacao);

            var mensagemDeEmail = CriarCorpoEmail(mensagem);
            Enviar(mensagemDeEmail);
            
        }

        private void Enviar(MimeMessage mensagemDeEmail)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_smtpServer,
                    _port,true);
                    client.AuthenticationMechanisms.Remove("XOUAUTH2");
                    client.Authenticate(_from,
                    _password);

                    client.Send(mensagemDeEmail);
                }catch(Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private MimeMessage CriarCorpoEmail(Mensagem mensagem)
        {
            var mensagemDeEmail = new MimeMessage();
            mensagemDeEmail.From.Add(new MailboxAddress(_from));
            mensagemDeEmail.To.AddRange(mensagem.Destinatario);
            mensagemDeEmail.Subject = mensagem.Assunto;
            mensagemDeEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text){
                Text = mensagem.Conteudo
            };

            return mensagemDeEmail;
        }
    }
}