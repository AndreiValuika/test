﻿using AutoMapper;
using BulbaCourses.PracticalMaterialsTests.Data.Context;
using BulbaCourses.PracticalMaterialsTests.Logic.Services.BaseService;
using BulbaCourses.PracticalMaterialsTests.Logic.Services.Questions.Interfaсe;
using System;
using System.Data.Entity;

namespace BulbaCourses.PracticalMaterialsTests.Logic.Services.Questions.Realization
{
    public class Service_Question_ChoosingAnswerFromList : Service_Base, IService_Question
    {
        protected Service_Question_ChoosingAnswerFromList(DbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public void AddQuestion()
        {
            throw new NotImplementedException();
        }

        public void DropQuestionById(int Id)
        {
            throw new NotImplementedException();
        }

        public void GetQuestonById(int Id)
        {
            throw new NotImplementedException();
        }        
    }
}
