using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using Org.BouncyCastle.Crypto;
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

                    var inbox = client.Inbox;
                    // Selecciona la carpeta de bandeja de entrada
                    await inbox.OpenAsync(FolderAccess.ReadOnly);

                    // Busca el correo con el asunto especificado
                    var uids = await inbox.SearchAsync(SearchQuery.All);

                    if(uids.Count > 0)
                    {
                        int index = Math.Max(inbox.Count - 100, 0);
                        var items = inbox.Fetch(index, -1, MessageSummaryItems.UniqueId);

                        foreach (var item in items)
                        {
                            var message = await inbox.GetMessageAsync(item.UniqueId);
                            if (message.Subject.Contains(subjectToSearch, StringComparison.OrdinalIgnoreCase))
                            {
                                correos.Add(email);
                            }
                                
                        }
                    }

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {

            }
            return correos;
        }
    }
}
