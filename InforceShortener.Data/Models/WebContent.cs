namespace InforceShortener.Data.Models
{
    public class WebContent : IModel
    {
        public int Id { get; set; }

        public string TextName { get; set; }

        public string TextValue { get; set; }


        public virtual User ChangedBy { get; set; }
    }
}
