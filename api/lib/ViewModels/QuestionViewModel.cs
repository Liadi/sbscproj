namespace Interview.ViewModels
{
    public class QuestionViewModel
    {
        public string Id { get; set; }

        public string Body { get; set; }

        public string ExamId { get; set; }

        public ExamViewModel ExamViewModel { get; set; }
    }
}