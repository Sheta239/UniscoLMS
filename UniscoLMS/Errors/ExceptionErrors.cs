using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace UniscoLMS.Errors
{
    public class ErrorUserEmailAlreadyExist : UniscoException
    {
        public ErrorUserEmailAlreadyExist() : base("User Email already exist", 1000) { }
    }
    public class ErrorGenerateUserToken : UniscoException
    {
        public ErrorGenerateUserToken() : base("Error Generate Usr Token", 1001) { }
    }
    public class ErrorUserEmailNotExist : UniscoException
    {
        public ErrorUserEmailNotExist() : base("User Email not exist", 1002) { }
    }
    public class ErrorIncorrectPassword : UniscoException
    {
        public ErrorIncorrectPassword() : base("Incorrect Password", 1002) { }
    }
    public class ErrorUserNotFound : UniscoException
    {
        public ErrorUserNotFound() : base("User Not Found", 1003) { }
    }
    public class ErrorNullId : UniscoException
    {
        public ErrorNullId() : base("Id Can't Be null", 1003) { }
    }
    public class ErrorWorkExperinceNotFount : UniscoException
    {
        public ErrorWorkExperinceNotFount() : base("Work Experince Not Found", 1003) { }
    }
    public class ErrorEducationHistoryNotFount : UniscoException
    {
        public ErrorEducationHistoryNotFount() : base("Education History Not Found", 1003) { }
    }
    public class ErrorSpecialityNotFound : UniscoException
    {
        public ErrorSpecialityNotFound() : base("Speciality Not Found", 1003) { }
    }
}
