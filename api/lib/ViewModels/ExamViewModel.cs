using System.Collections.Generic;

namespace Interview.ViewModels {
    public class ExamViewModel {
        public string Id { get; set; }

        public string Description { get; set; }

        public int PassScorePercentage { get; set; }
        
        public List<QuestionViewModel> Questions { get; set; }

    }
}