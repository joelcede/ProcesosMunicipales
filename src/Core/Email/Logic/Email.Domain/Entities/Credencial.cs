namespace Service.Email.Domain.Entities
{
    public class Credenciales
    {
        List<Credencial> credencial { get; set; } = new List<Credencial>();
    }
    public class Credencial
    {
        public Guid Id { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public int Estado { get; set; }
        public int IntentosSubida { get; set;}
        public int IntentosSubsanacion { get; set; }
        public int cantidadNegada { get; set; }
    }
}
