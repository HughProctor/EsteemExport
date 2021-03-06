﻿using System;
using BusinessModel.Services;
using BusinessModel.Mappers;
using EntityModel.Repository;
using EntityModel.Repository.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessModel.Test
{
    [TestClass]
    public class BAM_Service_Tests
    {
        [TestMethod]
        public void Final_Process_All_Data()
        {
            try
            {
                Map.Init();

                IQueryBuilder queryBuilder = new QueryBuilder();
                queryBuilder.StartDateString = "01/01/2017";
                queryBuilder.EndDateString = "01/02/2017";
                queryBuilder.PageCount = 100000000;

                var service = new BAM_Service();

                var records = service.ExportDataToBAM(queryBuilder).Result;
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "ErrorMessage: " + ex.InnerException);
            }
        }
    }
}
