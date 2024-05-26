using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ReactApp.Server.Models;
using ReactApp.Server.Models.Request;
using ReactApp.Server.Models.Response;

namespace ReactApp.Server.Services
{
    public interface IPersoninfoService
    {
        Task<List<GetPersoninfoResultModel>> GetPersoninfoAsync(GetPersoninfoQueryModel query);
        Task<AddNewPersoninfoResultModel> AddNewPersoninfoAsync(AddNewPersoninfoQueryModel query);
        Task<DeletePersoninfoResultModel> DeletePersoninfoAsync(DeletePersoninfoQueryModel query);
        Task<UpdatePersoninfoResultModel> UpdatePersoninfoAsync(UpdatePersoninfoQueryModel query);
    }
    public class PersoninfoService: IPersoninfoService
    {
        private readonly ExamContext _ExamContext;

        public PersoninfoService(ExamContext examContext)
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
            var personinfos = await data.ToListAsync();

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

        #region 新增資料
        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="query"></param>
        /// <returns>
        /// cmd: 0 -> 成功, 1 -> 失敗, 2 -> 例外錯誤
        /// Message 回傳結果的訊息
        /// </returns>
        public async Task<AddNewPersoninfoResultModel> AddNewPersoninfoAsync(AddNewPersoninfoQueryModel query)
        {
            var result = new AddNewPersoninfoResultModel();
            var table = _ExamContext.Personinfos;

            // 確認 Name 是否為空
            if (string.IsNullOrEmpty(query.Name))
            {
                result.Cmd = 1;
                result.Message = "Name Can't Be Empty!";
                return result;
            }
            // 確認 Name 是否為空
            if (string.IsNullOrEmpty(query.Phone))
            {
                result.Cmd = 1;
                result.Message = "Phone Can't Be Empty!";
                return result;
            }

            var newPersoninfo = new Models.Personinfo
            {
                Name = query.Name,
                Phone = query.Phone,
                Note = query.Note
            };

            try
            {
                await table.AddAsync(newPersoninfo);
                await _ExamContext.SaveChangesAsync();

                result.Cmd = 0;
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.Cmd = 2;
                result.Message = $"Failed to add new Personinfo. Error: {ex.Message}";
            }

            return result;

        }
        #endregion

        #region 刪除資料
        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="query"></param>
        /// <returns>
        /// cmd: 0 -> 成功, 1 -> 失敗, 2 -> 例外錯誤
        /// message 回傳訊息
        /// </returns>
        public async Task<DeletePersoninfoResultModel> DeletePersoninfoAsync(DeletePersoninfoQueryModel query)
        {
            var result = new DeletePersoninfoResultModel();
            var table = _ExamContext.Personinfos;

            var personinfo = await table.FirstOrDefaultAsync(p => p.No == query.No && p.Name == query.Name);

            if (personinfo == null)
            {
                result.Cmd = 1;
                result.Message = "Personinfo not found!";
                return result;
            }

            try
            {
                table.Remove(personinfo);
                await _ExamContext.SaveChangesAsync();

                result.Cmd = 0; // 假设 2 表示成功
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.Cmd = 3;
                result.Message = $"Failed to delete Personinfo. Error: {ex.Message}";
            }
            return result;
        }
        #endregion

        #region 修改資料
        /// <summary>
        /// 修改資料
        /// </summary>
        /// <param name="query"></param>
        /// <returns>
        /// cmd: 0 -> 成功, 1 -> 失敗, 2 -> 例外錯誤
        /// message 回傳訊息
        /// </returns>
        public async Task<UpdatePersoninfoResultModel> UpdatePersoninfoAsync(UpdatePersoninfoQueryModel query)
        {
            var result = new UpdatePersoninfoResultModel();
            var table = _ExamContext.Personinfos;

            var personinfo = await table.FirstOrDefaultAsync(p => p.No == query.No);

            if (personinfo == null)
            {
                result.Cmd = 1;
                result.Message = "Personinfo not found!";
                return result;
            }

            try
            {
                if (!string.IsNullOrEmpty(query.Name))
                {
                    personinfo.Name = query.Name;
                }
                if (!string.IsNullOrEmpty(query.Phone))
                {
                    personinfo.Phone = query.Phone;
                }
                if (!string.IsNullOrEmpty(query.Note))
                {
                    personinfo.Note = query.Note;
                }

                await _ExamContext.SaveChangesAsync();

                result.Cmd = 0; // 假设 2 表示成功
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.Cmd = 3;
                result.Message = $"Failed to update Personinfo. Error: {ex.Message}";
            }

            return result;
        }
        #endregion
    }
}
