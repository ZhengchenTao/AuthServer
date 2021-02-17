namespace AuthServer.Entities.BaseEntities
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
