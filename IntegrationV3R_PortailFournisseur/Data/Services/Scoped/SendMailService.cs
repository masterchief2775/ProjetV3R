using System.Net.Mail;
using System.Net;

namespace IntegrationV3R_PortailFournisseur.Data.Services.Scoped
{
    public class SendMailService
    {
        private string _mailAddress = "contact@ville3r.gouv.qc.ca";

        public async Task SendMailConfirmCreate(string nomentreprise, string EmailInput, string confirmationLink)
        {
            string adress = EmailInput;

            // Set up SMTP client
            var smtpClient = new SmtpClient("sandbox.smtp.mailtrap.io", 587)
            {
                Credentials = new NetworkCredential("d03e91c65ae080", "72c31abf54bb14"),
                EnableSsl = true // Enable SSL for secure communication
            };

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Templates/ExempleCourriel.html");
            string body = File.ReadAllText(filePath);

            string lienTemporaire = "https://en.wikipedia.org/wiki/Thumb_signal#/media/File:Jempol_Ngadep_Atas_(cropped).jpg";

            var pathImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Templates/Logo_of_Trois-Rivières,_Quebec.svg");
            string svgContent = File.ReadAllText(pathImage);
            byte[] imageBytes = System.Text.Encoding.UTF8.GetBytes(svgContent);

            body = body.Replace("{NomEntreprise}", nomentreprise);
            body = body.Replace("{Image}", Convert.ToBase64String(imageBytes));
            body = body.Replace("{Link}", lienTemporaire);

            // Create the email message
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_mailAddress),
                Subject = "Confirmation de réception",
                /* Le body du courriel peut être formatter en HTML selon les normes à la ville dans le projet. Il est donc possible d'instancier 
                   par la suite un courriel avec ce body d'html avec les informations du client*/
                Body = body,
                /*"Ceci est un courriel pour vous laissez savoir que nous avons reçu votre demande d'adhésion à la liste des fournisseurs de la ville de Trois-Rivières" + '\n' +
                '\n' + "Votre dossier est en attente de traitement. Vous recevrez un autre courriel lorsque la décision sera prise." + '\n' + '\n' +
                "Merci et bonne journée !",*/
                IsBodyHtml = true // Set to true if you want to send HTML content
            };

            mailMessage.To.Add(EmailInput);
            //mailMessage.To.Add("gporlier97@gmail.com");

            // Send the email asynchronously
            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }


        }
    }
}
