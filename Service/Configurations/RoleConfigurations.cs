namespace Service.Configurations
{
    public class RoleConfigurations
    {
        public RoleConfigurations(int i, string d)
        {
            Id = i;
            Descricao = d;
        }
        public int Id { get; set; }
        public string Descricao { get; set; }
    }
}