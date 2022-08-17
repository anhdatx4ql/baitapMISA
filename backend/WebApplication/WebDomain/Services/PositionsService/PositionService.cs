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
    public class PositionsService : IPositionsService
    {

        private readonly IDapperRepository _dapper;
        public PositionsService(IDapperRepository dapper)
        {
            _dapper = dapper;
        }

        /// <summary>
        /// Author: Phạm Văn Đạt
        /// function: Lấy danh sách
        /// Created time: 5:55 15/08/2022
        /// </summary>
        /// <returns></returns>
        public async Task<ReponsitoryModel> GetAll()
        {
            try
            {
                string sql = @"SELECT PositionId,PositionName FROM Positions";
                List<PositionModel> result = await _dapper.GetAllAsync<PositionModel>(sql);
                if (result.Count == 0)
                    return new ReponsitoryModel { Data = null, Message = MessageError.NotValue, StatusCode = CodeError.NotValue };
                return new ReponsitoryModel { Data = result, Message = MessageSuccess.GetSuccess, StatusCode = CodeSuccess.GetSuccess };
            }
            catch (Exception ex)
            {
                return new ReponsitoryModel { Data = ex.Message, Message = MessageError.ProcessError, StatusCode = CodeError.ProcessError };
            }

        }

        /// <summary>
        /// Author: Phạm Văn Đạt
        /// function: Lấy danh sách theo tên
        /// Created time: 5:55 15/08/2022
        /// </summary>
        /// <returns></returns>
        public async Task<ReponsitoryModel> GetAllByName(string name)
        {
            try
            {
                string sql = @"SELECT PositionId,PositionName FROM Positions WHERE PositionName like @search;";
                var parameters = new Dictionary<string, object>()
                {
                    ["search"] = $"%{name}%"
                };
                List<PositionModel> result = await _dapper.FindTAsync<PositionModel>(sql, parameters);
                if (result.Count == 0)
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