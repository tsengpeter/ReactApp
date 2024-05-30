using Microsoft.EntityFrameworkCore;
using ReactApp.Server.Models;
using ReactApp.Server.Models.Request;
using ReactApp.Server.Models.Response;

namespace ReactApp.Server.Services
{
    public interface IPersoninfoService
    {
        List<GetPersoninfoResultModel> GetPersoninfo(GetPersoninfoQueryModel query);
        AddNewPersoninfoResultModel AddNewPersoninfo(AddNewPersoninfoQueryModel query);
        DeletePersoninfoResultModel DeletePersoninfo(DeletePersoninfoQueryModel query);
        UpdatePersoninfoResultModel UpdatePersoninfo(UpdatePersoninfoQueryModel query);
    }

    public class PersoninfoService : IPersoninfoService
    {
        private readonly ExamContext _examContext;

        public PersoninfoService(ExamContext examContext)
        {
            _examContext = examContext;
        }

        #region 資料查詢
        public List<GetPersoninfoResultModel> GetPersoninfo(GetPersoninfoQueryModel query)
        {
            var result = new List<GetPersoninfoResultModel>();

            IQueryable<Personinfo> data = _examContext.Personinfos;
            if (query.No.HasValue)
            {
                data = data.Where(x => x.No == query.No);
            }
            if (!string.IsNullOrEmpty(query.Name))
            {
                data = data.Where(x => x.Name == query.Name);
            }
            var personinfos = data.ToList();

            foreach (var personinfo in personinfos)
            {
                result.Add(new GetPersoninfoResultModel
                {
                    No = personinfo.No,
                    Name = personinfo.Name,
                    Phone = personinfo.Phone,
                    Note = personinfo.Note
                });
            }

            return result;
        }
        #endregion

        #region 新增資料
        public AddNewPersoninfoResultModel AddNewPersoninfo(AddNewPersoninfoQueryModel query)
        {
            var result = new AddNewPersoninfoResultModel();

            if (string.IsNullOrEmpty(query.Name))
            {
                result.Cmd = 1;
                result.Message = "Name can't be empty!";
                return result;
            }

            if (string.IsNullOrEmpty(query.Phone))
            {
                result.Cmd = 1;
                result.Message = "Phone can't be empty!";
                return result;
            }

            var newPersoninfo = new Personinfo
            {
                Name = query.Name,
                Phone = query.Phone,
                Note = query.Note
            };

            try
            {
                _examContext.Personinfos.Add(newPersoninfo);
                _examContext.SaveChanges();

                result.Cmd = 0;
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.Cmd = 2;
                result.Message = $"Failed to add new personinfo. Error: {ex.Message}";
            }

            return result;
        }
        #endregion

        #region 刪除資料
        public DeletePersoninfoResultModel DeletePersoninfo(DeletePersoninfoQueryModel query)
        {
            var result = new DeletePersoninfoResultModel();

            var personinfo = _examContext.Personinfos
                .FirstOrDefault(p => p.No == query.No && p.Name == query.Name);

            if (personinfo == null)
            {
                result.Cmd = 1;
                result.Message = "Personinfo not found!";
                return result;
            }

            try
            {
                _examContext.Personinfos.Remove(personinfo);
                _examContext.SaveChanges();

                result.Cmd = 0;
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.Cmd = 2;
                result.Message = $"Failed to delete personinfo. Error: {ex.Message}";
            }

            return result;
        }
        #endregion

        #region 修改資料
        public UpdatePersoninfoResultModel UpdatePersoninfo(UpdatePersoninfoQueryModel query)
        {
            var result = new UpdatePersoninfoResultModel();

            var personinfo = _examContext.Personinfos.FirstOrDefault(p => p.No == query.No);

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

                _examContext.SaveChanges();

                result.Cmd = 0;
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.Cmd = 2;
                result.Message = $"Failed to update personinfo. Error: {ex.Message}";
            }

            return result;
        }
        #endregion
    }
}
