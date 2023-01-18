using Pnk.Web.Models.Dto;
using System.Collections.Generic;
namespace Pnk.Web.Services.IServices
{
    public interface IBaseService: IDisposable
    {
        ResponseDto ResponseModel { get; set; }

        Task<T> SendRequestAysnc<T>(APIRequest aPIRequest);
    }
}
