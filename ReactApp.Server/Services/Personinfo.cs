using ReactApp.Server.Models;

namespace ReactApp.Server.Services
{
    public interface IPersoninf
    {

    }
    public class Personinfo
    {
        private readonly ExamContext _ExamContext;

        public Personinfo(ExamContext examContext)
        {
            _ExamContext = examContext;
        }


    }
}
