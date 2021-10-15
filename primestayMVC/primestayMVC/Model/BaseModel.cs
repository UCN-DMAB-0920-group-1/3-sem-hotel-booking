namespace primestayMVC.Model
{
    public class BaseModel
    {
        public string href { get; set; }

        public BaseModel(string href) => this.href = href;

        public BaseModel()
        {
        }
    }
}