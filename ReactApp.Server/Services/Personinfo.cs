using ReactApp.Server.Models;
using ReactApp.Server.Models.Request;
using ReactApp.Server.Models.Response;

namespace ReactApp.Server.Services
{
    public interface IPersoninfoService
    {
        Task<List<GetPersoninfoResultModel>> GetPersoninfoAsync(GetPersoninfoQueryModel query);
    }
    public class Personinfo
    {
        private readonly ExamContext _ExamContext;

        public Personinfo(ExamContext examContext)
        {
            _ExamContext = examContext;
        }
        #region 資料查詢
        /// <summary>
        /// 取得資料
        /// </summary>
        /// <param name="query"></param>
        /// <returns>
        /// No, Name, Phone, Note
        /// </returns>
        public async Task<List<GetPersoninfoResultModel>> GetPersoninfoAsync(GetPersoninfoQueryModel query)
        {
            var Result = new List<GetPersoninfoResultModel>();

            var data = _ExamContext.Personinfos.AsQueryable();
            if (query.No != null)
            {
                data.Where(x => x.No == query.No);
            }
            if( query.Name != null )
            {
                data.Where(x => x.Name == query.Name);
            }
            var personinfos = data.ToList();

            for ( var i = 0; i < personinfos.Count; i++ )
            {
                var personinfo = new GetPersoninfoResultModel()
                {
                    No = personinfos[i].No,
                    Name = personinfos[i].Name,
                    Phone = personinfos[i].Phone,
                    Note = personinfos[i].Note
                };
                Result.Add(personinfo);
            }

            return Result;
        }
        #endregion
    }
}
