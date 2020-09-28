using Model.Interface;

namespace Model.Data.Interface
{
    public interface IContext : IEntity
    {
        public string Body { get; set; }
        public Meta Meta { get; set; }
    }
}