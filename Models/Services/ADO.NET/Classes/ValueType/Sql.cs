namespace Corso10157.Models.Services.ADO.NET.Classes.ValueType
{
    public class Sql
    {

        private Sql(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static explicit operator Sql(string value) => new Sql(value);

        public override string ToString()
        {
            return this.Value;
        }


    }
}