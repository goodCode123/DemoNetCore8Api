namespace Repository.MergeModel
{
    public class DeleteGroupMemberModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Pwd { get; set; }
        public int GroupMemberId { get; set; }
    }
}
