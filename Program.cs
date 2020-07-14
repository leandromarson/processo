using System;
using System.Linq;
using System.Net;
using System.Net.Mail;


namespace processo
{
    class Program
    {
        static void Main(string[] args)
        {
            Cadastro cad = new Cadastro();
            Console.WriteLine("Insira seu email:");
            cad.email = Console.ReadLine();
            cad.alterarSenha();


            Console.ReadKey();
        }
    }
    public class Cadastro{
        public String email;
        
        public void alterarSenha(){
            //Geração da senha
            int tamanhoSenha = 10;            
            var letras = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var novaSenha = new string(Enumerable.Repeat(letras,tamanhoSenha).Select(s => s[random.Next(s.Length)]).ToArray());

            //Enviar email
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("emailremetente@gmail.com", "senhadoemail");
            MailMessage mail = new MailMessage();            
            // Emitente do email
            mail.From = new MailAddress("emailremetente@gmail.com", "Sistema");
            // Destinatário
            mail.To.Add(email);                     
            // Prioridade
            mail.Priority = MailPriority.High;
            //Assunto
            mail.Subject = "Nova Senha";
            // Informa que o corpo é do Tipo Html
            mail.IsBodyHtml = true;
            // Corpo da Página
            mail.Body = "<html>Sua nova senha: "+novaSenha+"</html>";            
                        
                            
            try
            {
                client.Send(mail);
                Console.WriteLine("Email enviado com sucesso!");
            }
            catch (System.Exception erro)
            {
                Console.WriteLine("Erro ao enviar o email! "+erro);
            }
            finally
            {
                mail = null;
            }      
            
        }
        

    }
    
}
