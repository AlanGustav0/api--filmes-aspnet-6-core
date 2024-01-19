using MailKit.Net.Smtp;
using MimeKit;
using UsuariosAPI.Model;

namespace UsuariosAPI.Services
{
    public class EmailService
    {
        private readonly IConfiguration? _configuration;
        public void EnviarEmail(string[] destinatario,string assunto,int usuarioId,string codigoAtivacao)
        {
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
                    client.Connect(_configuration.GetValue<string>("EmailSettings:SmtpServer"),
                    _configuration.GetValue<int>("EmailSettings:Port"),true);
                    client.AuthenticationMechanisms.Remove("XOUAUTH2");
                    client.Authenticate(_configuration.GetValue<string>("EmailSettings:From"),
                    _configuration.GetValue<string>("EmailSettings:Password"));

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
            mensagemDeEmail.From.Add(new MailboxAddress(_configuration.GetValue<string>("EmailSettings:From")));
            mensagemDeEmail.To.AddRange(mensagem.Destinatario);
            mensagemDeEmail.Subject = mensagem.Assunto;
            mensagemDeEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text){
                Text = mensagem.Conteudo
            };

            return mensagemDeEmail;
        }
    }
}