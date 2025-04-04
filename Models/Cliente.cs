namespace LoginFrontEnd.Models
{
    public class Cliente
    {
        public int Fast_id { get; set; }
        public int Teste_id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public long DataExpericaoUnix { get; set; }
        public DateTime DataExpiracao { get
            {
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(DataExpericaoUnix);
                return dateTimeOffset.ToOffset(TimeSpan.FromHours(-3)).DateTime;
            } 
        }
        public string Notas { get; set; }
        public string? Celular { get; set; }
        public string? Email { get; set; }
        public bool Ativo { get {
                if (DataExpiracao > DateTime.Now)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool Teste { get; set; }

    }
}
