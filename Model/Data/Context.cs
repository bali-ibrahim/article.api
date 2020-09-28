using Model.Data.Interface;

namespace Model.Data
{
    public class Context : IContext
    {
        public int Id { get; set; }
        public int MetaId { get; set; }
        public string Body { get; set; }
        public Meta Meta { get; set; }
    }
}