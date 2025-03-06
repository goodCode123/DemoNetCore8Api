namespace Repository.Model
{
    public class MenuItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string Icon { get; set; }

        public int? ParentId { get; set; }

        public virtual MenuItem Parent { get; set; }

        public virtual ICollection<MenuItem> Children { get; set; }
    }
}
