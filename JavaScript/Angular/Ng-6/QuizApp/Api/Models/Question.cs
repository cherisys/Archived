using Api.Global;

namespace Api.Models
{
    public class Question: EntityBase
    {
        public string QuestionText { get; set; }
        public string ImageName { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Answer { get; set; }
    }
}
