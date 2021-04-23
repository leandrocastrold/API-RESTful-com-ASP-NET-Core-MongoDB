namespace Mk_Api.Data.Collections
{
    public class Fighter
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public string Birthplace {get; set; }
        public int Age {get; set;}


        public Fighter(string name, string cl, string birthplace, int age)
        {
            this.Name = name;
            this.Class = cl;
            this.Birthplace = birthplace;
            this.Age = age;
        }

        
    }
}