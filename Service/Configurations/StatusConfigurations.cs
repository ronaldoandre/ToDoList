namespace Service.Configurations
{
    public class StatusConfigurations
    {
        public StatusConfigurations(int i, string d)
        {
            Id = i;
            Descricao = d;
        }
        public int Id { get; set; }
        public string Descricao { get; set; }
    }
}