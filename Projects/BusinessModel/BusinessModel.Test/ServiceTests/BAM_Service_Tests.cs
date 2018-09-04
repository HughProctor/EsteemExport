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
            Map.Init();

            IQueryBuilder queryBuilder = new QueryBuilder();
            queryBuilder.StartDateString = "04/01/2017";
            queryBuilder.EndDateString = "01/05/2017";
            queryBuilder.PageCount = 100000000;

            var service = new BAM_Service();

            var records = service.ExportDataToBAM(queryBuilder).Result;
        }
    }
}
