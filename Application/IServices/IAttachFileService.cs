using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IAttachFileService
    {
        Task<List<AttachFile>> GetListAsync();
        Task<AttachFile> GetByIdAsync(int id);
        Task UpdateAsync(AttachFile attachFile);
        Task DeleteAsync(int id);
        Task CreateAsync(AttachFile attachFile);
        Task CreateListAsync(List<AttachFile> attachFileList);
    }
}
