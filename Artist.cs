namespace CSLab5
{
    class Artist(int id, string name)
    {
        private int _id = id;
        private string _name = name;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }

        public override string ToString() => $"id = {_id}, name = {_name}";
    }
}
