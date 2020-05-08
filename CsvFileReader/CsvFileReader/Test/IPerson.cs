namespace CsvFileReader.Test
{
    public interface IPerson
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int Age { get; set; }

        public void SetAge(int age);
        public void SetName(string firstName, string secondName);
    }
}
