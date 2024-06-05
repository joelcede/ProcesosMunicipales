using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using System.Net.Mail;

namespace Commons.EmailService
{
    public class RevisarCorreo
    {
        public async Task<IEnumerable<string>> obtenerCorreosEnProceso(string email, string password, string subjectToSearch)
        {
            List<string> correos = new List<string>();
            // Check emails
            try
            {
                using (ImapClient client = new ImapClient())
                {
                    if (string.IsNullOrEmpty(email) && email.Contains("gmail"))
                        await client.ConnectAsync("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                    else
                        await client.ConnectAsync("outlook.office365.com", 993, SecureSocketOptions.SslOnConnect);

                    // Autentica con tus credenciales
                    await client.AuthenticateAsync(email, password);

                    // Selecciona la carpeta de bandeja de entrada
                    await client.Inbox.OpenAsync(FolderAccess.ReadOnly);

                    // Busca el correo con el asunto especificado
                    var query = SearchQuery.SubjectContains(subjectToSearch);
                    var uids = await client.Inbox.SearchAsync(query);

                    foreach (var uid in uids)
                    {
                        var message = await client.Inbox.GetMessageAsync(uid);
                        if (message != null && message.Subject != null && message.Subject.Contains(subjectToSearch))
                            correos.Add(email);
                    }
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return correos;
        }
    }
}
