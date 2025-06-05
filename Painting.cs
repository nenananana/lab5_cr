namespace CSLab5
{
    class Painting(int id, string name, int idArtist, int ermitagePart, int year, int idStyle)
    {
        private int _id = id;
        private string _name = name;
        private int _idArtist = idArtist;
        private int _ermitagePart = ermitagePart;
        private int _year = year;
        private int _idStyle = idStyle;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public int IdArtist { get => _idArtist; set => _idArtist = value; }
        public int ErmitagePart { get => _ermitagePart; set => _ermitagePart = value; }
        public int Year { get => _year; set => _year = value; }
        public int IdStyle { get => _idStyle; set => _idStyle = value; }

        public override string ToString() =>
            $"id = {_id}, name = {_name}, idArtist = {_idArtist}, ermitagePart = {_ermitagePart}, year = {_year}, idStyle = {_idStyle}";
    }
}
