﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebInfrastructure;
using static WebDomain.ContantsError;
using static WebDomain.ContantsSuccess;

namespace WebDomain
{
    /// <summary>
    /// Author: Phạm Văn Đạt
    /// Created Time: 10:55 13/08/2022
    /// Service xử lý bảng xưng hô
    /// </summary>
    public class VocativeService : IVocativeService
    {
        private readonly IDapperRepository _dapper;
        public VocativeService(IDapperRepository dapper)
        {
            _dapper = dapper;
        }

        /// <summary>
        /// Author: Phạm Văn Đạt
        /// Created Time: 11:25 13/08/2022
        /// Lấy danh sách xưng hô
        /// </summary>
        /// <returns></returns>
        public async Task<ReponsitoryModel> GetAll()
        {
            try
            {
                string sql = @"SELECT VocativeId,VocativeName FROM Vocative";
                List<VocativeModel> result = await _dapper.GetAllAsync<VocativeModel>(sql);
                if (result.Count == 0)
                    return new ReponsitoryModel { Data = null, Message = MessageError.NotValue, StatusCode = CodeError.NotValue };
                return new ReponsitoryModel { Data = result, Message = MessageSuccess.GetSuccess, StatusCode = CodeSuccess.GetSuccess };
            }
            catch(Exception ex)
            {
                return new ReponsitoryModel { Data = ex.Message, Message = MessageError.ProcessError, StatusCode = CodeError.ProcessError };
            }
        }


        /// <summary>
        /// Author: Phạm Văn Đạt
        /// Created Time: 11:25 13/08/2022
        /// Lấy danh sách xưng hô theo tên
        /// </summary>
        /// <returns></returns>
        public async Task<ReponsitoryModel> GetAllByName(string name)
        {
            try
            {
                string sql = @"SELECT VocativeId,VocativeName FROM Vocative WHERE VocativeName like @search;";
                var parameters = new Dictionary<string, object>()
                {
                    ["search"] = $"%{name}%"
                };
                List<SourceModel> result = await _dapper.FindTAsync<SourceModel>(sql, parameters);
                if (result == null)
                    return new ReponsitoryModel { Data = null, Message = MessageError.NotValue, StatusCode = CodeError.NotValue };
                return new ReponsitoryModel { Data = result, Message = MessageSuccess.GetSuccess, StatusCode = CodeSuccess.GetSuccess };
            }
            catch (Exception ex)
            {
                return new ReponsitoryModel { Data = ex, Message = MessageError.ProcessError, StatusCode = CodeError.ProcessError };
            }
        }
    }
}